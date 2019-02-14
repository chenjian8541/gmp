using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TTY.GMP.Entity.View;
using TTY.GMP.IBusiness;
using TTY.GMP.IOC;
using TTY.GMP.Utility;
using TTY.GMP.LOG;
using TTY.GMP.Entity.Config;
using TTY.GMP.Entity.Database;
using System.Linq;
using TTY.GMP.Entity.Enum;
using System.Data;

namespace TTY.GMP.Jobs
{
    /// <summary>
    /// 定时生成门店零售单排名
    /// </summary>
    [DisallowConcurrentExecution]
    public class GenerateShopRetailRankJob : IJob
    {
        /// <summary>
        /// 门店业务访问
        /// </summary>
        private IShopBLL _shopBll;

        /// <summary>
        /// 门店销售单排名业务访问
        /// </summary>
        private IShopRetailRankBLL _shopRetailRankBll;

        /// <summary>
        /// 门店销售单排名获奖者业务访问
        /// </summary>
        private IShopRetailRankLimitBLL _shopRetailRankWinnerBll;

        /// <summary>
        /// 定时生成门店零售单排名功能配置
        /// </summary>
        private ShopRetailRankConfig _config;

        /// <summary>
        /// 系统配置
        /// </summary>
        private AppSettings _appSettings;

        /// <summary>
        /// 平台访问业务
        /// </summary>
        private IPlatformBLL _platformBll;

        /// <summary>
        /// 开始时间
        /// </summary>
        private DateTime _now;

        /// <summary>
        /// 成长之星(开始时间)
        /// </summary>
        private DateTime _startGrowth;

        /// <summary>
        /// 成长之星(结束时间)
        /// </summary>
        private DateTime _endGrowth;

        /// <summary>
        /// 订单王(开始时间)
        /// </summary>
        private DateTime _startKing;

        /// <summary>
        /// 订单王(结束时间)
        /// </summary>
        private DateTime _endKing;

        /// <summary>
        /// 生成门店零售单排名
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task Execute(IJobExecutionContext context)
        {
            Log.Info("定时生成门店零售单排名Job》》开始执行", this.GetType());
            Initialize();
            try
            {
                Process();
                Log.Info("定时生成门店零售单排名Job》》执行完成", this.GetType());
                Console.WriteLine($"{DateTime.Now}定时生成门店零售单排名Job》》执行完成");
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex, this.GetType());
            }
            return Task.CompletedTask;
        }

        /// <summary>
        /// 初始化类公共属性
        /// </summary>
        private void Initialize()
        {
            _shopBll = CustomServiceLocator.GetInstance<IShopBLL>();
            _shopRetailRankBll = CustomServiceLocator.GetInstance<IShopRetailRankBLL>();
            _shopRetailRankWinnerBll = CustomServiceLocator.GetInstance<IShopRetailRankLimitBLL>();
            _platformBll = CustomServiceLocator.GetInstance<IPlatformBLL>();
            _appSettings = CustomServiceLocator.GetInstance<IAppConfigurtaionServices>().AppSettings;
            _config = _appSettings.ShopRetailRankConfig;
            _now = DateTime.Now;
            _startGrowth = _now.GetStartOfTheWeek();
            _endGrowth = _now;
            _startKing = _config.KingStartTime;
            _endKing = _now;
        }

        /// <summary>
        /// 执行生成门店零售单排名
        /// </summary>
        private void Process()
        {
            var winners = _shopRetailRankWinnerBll.GetShopRetailRankWinner().Result;
            var growthData = GetShopRetailRankedOfGrowth(winners, out var limitGrowthShops);
            var kingData = GetShopRetailRankedOfKing(winners, out var limitKingShops);
            var excelPath = SaveExcel(growthData, kingData);
            SendMailAboutRank(excelPath);
            _shopRetailRankBll.DelShopRetailRank(DateTime.Now.AddMonths(-1)).Wait();
            _shopRetailRankBll.MarkShopRetailRankOld();
            _shopRetailRankBll.AddShopRetailRank(growthData).Wait();
            _shopRetailRankBll.AddShopRetailRank(kingData).Wait();
            _shopRetailRankBll.AddShopRetailRank(limitGrowthShops).Wait();
            _shopRetailRankBll.AddShopRetailRank(limitKingShops).Wait();
            HandleWinner(growthData);
        }

        /// <summary>
        /// 获取成长之星数据
        /// </summary>
        /// <param name="winners"></param>
        /// <param name="limitShop"></param>
        /// <returns></returns>
        private List<ShopRetailRank> GetShopRetailRankedOfGrowth(List<ShopRetailRankLimit> winners, out List<ShopRetailRank> limitShop)
        {
            Log.Info($"【成长之星】定时获取门店零售单排名数据:开始时间:{_startGrowth},结束时间:{_endGrowth}", this.GetType());
            var rankView = _shopBll.GetShopRetailRank(_startGrowth.ToTimestamp(), _endGrowth.ToTimestamp()).Result;
            rankView = ClearAwayWinnerOfGrowth(rankView, winners, out var limitShopView);
            //被排除的门店
            limitShop = limitShopView.Select(p =>
                 ConvertToShopRetailRank(p, _startGrowth, _endGrowth, 0, (int)ShopRetailRankTypeEnum.Growth, true)
            ).ToList();
            var rank = 0;
            return rankView.Select(p =>
                ConvertToShopRetailRank(p, _startGrowth, _endGrowth, ++rank, (int)ShopRetailRankTypeEnum.Growth)
            ).ToList();
        }

        /// <summary>
        /// 排除已获奖者名单(成长之星)
        /// </summary>
        /// <param name="oldRankView"></param>
        /// <param name="winners"></param>
        /// <param name="limitShop"></param>
        /// <returns></returns>
        private List<ShopRetailRankView> ClearAwayWinnerOfGrowth(List<ShopRetailRankView> oldRankView, List<ShopRetailRankLimit> winners, out List<ShopRetailRankView> limitShop)
        {
            if (winners == null || !winners.Any())
            {
                limitShop = new List<ShopRetailRankView>();
                return oldRankView;
            }
            limitShop = oldRankView.Where(p => winners.Exists(w => p.ShopId == w.ShopId)).ToList();
            return oldRankView.Where(p => !winners.Exists(w => p.ShopId == w.ShopId)).OrderByDescending(p => p.BillCount).ToList();
        }

        /// <summary>
        /// 获取订单王数据
        /// </summary>
        /// <param name="winners"></param>
        /// <param name="limitShops"></param>
        /// <returns></returns>
        private List<ShopRetailRank> GetShopRetailRankedOfKing(List<ShopRetailRankLimit> winners, out List<ShopRetailRank> limitShops)
        {
            Log.Info($"【订单王】定时获取门店零售单排名数据:开始时间:{_startKing},结束时间:{_endKing}", this.GetType());
            var rankView = _shopBll.GetShopRetailRank(_startKing.ToTimestamp(), _endKing.ToTimestamp()).Result;
            rankView = ClearAwayWinnerOfKing(rankView, winners, out var limitShopsView);
            //被排除的门店
            limitShops = limitShopsView.Select(p =>
                ConvertToShopRetailRank(p, _startKing, _endKing, 0, (int)ShopRetailRankTypeEnum.King, true)
            ).ToList();
            var rank = 0;
            return rankView.Select(p =>
                ConvertToShopRetailRank(p, _startKing, _endKing, ++rank, (int)ShopRetailRankTypeEnum.King)
            ).ToList();
        }

        /// <summary>
        /// 排除已获奖者名单(订单王)
        /// </summary>
        /// <param name="oldRankView"></param>
        /// <param name="winners"></param>
        /// <param name="limitShops"></param>
        /// <returns></returns>
        private List<ShopRetailRankView> ClearAwayWinnerOfKing(List<ShopRetailRankView> oldRankView, List<ShopRetailRankLimit> winners, out List<ShopRetailRankView> limitShops)
        {
            limitShops = new List<ShopRetailRankView>();
            if (winners == null || !winners.Any())
            {
                return oldRankView;
            }
            var testShops = winners.Where(p => p.Type == (int)ShopRetailRankLimitTypeEnum.TestShop).ToList();
            if (!testShops.Any())
            {
                return oldRankView;
            }
            limitShops = oldRankView.Where(p => testShops.Exists(w => p.ShopId == w.ShopId)).ToList();
            return oldRankView.Where(p => !testShops.Exists(w => p.ShopId == w.ShopId)).OrderByDescending(p => p.BillCount).ToList();
        }

        /// <summary>
        /// ShopRetailRankView转换成ShopRetailRank
        /// </summary>
        /// <param name="view"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="rank"></param>
        /// <param name="type"></param>
        /// <param name="isLimit"></param>
        /// <returns></returns>
        private ShopRetailRank ConvertToShopRetailRank(ShopRetailRankView view, DateTime startTime, DateTime endTime,
            int rank, int type, bool isLimit = false)
        {
            var shopRetailRank = new ShopRetailRank()
            {
                ShopId = view.ShopId,
                ShopLinkMan = view.ShopLinkMan,
                ShopName = view.ShopName,
                ShopTelphone = view.ShopTelphone,
                ShopAddress = view.ShopAddress,
                OrgId = view.OrgId,
                OrgNo = string.Empty,
                BillCount = view.BillCount,
                Rank = rank,
                StartTime = startTime,
                EndTime = endTime,
                Type = type,
                IsLast = true,
                IsLimit = isLimit,
                ProvinceName = view.ProvinceName,
                CityName = view.CityName,
                DistrictName = view.DistrictName,
                RecommendName = GetOrgRecommendName(view.OrgId)
            };
            HandleShopRetailRank(shopRetailRank);
            return shopRetailRank;
        }

        /// <summary>
        /// 通过机构ID获取组织编码
        /// </summary>
        /// <param name="shopRetailRank"></param>
        /// <returns></returns>
        private void HandleShopRetailRank(ShopRetailRank shopRetailRank)
        {
            var org = _platformBll.GetOrgInfo(shopRetailRank.OrgId);
            if (org != null)
            {
                shopRetailRank.OrgNo = org.org_number;
                if (string.IsNullOrEmpty(shopRetailRank.ShopLinkMan))
                {
                    shopRetailRank.ShopLinkMan = org.org_legal_person;
                }
            }
        }

        /// <summary>
        /// 获取机构推荐人信息
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        private string GetOrgRecommendName(long orgId)
        {
            var recommend = _platformBll.GetOrgRecommend(orgId);
            return recommend == null ? string.Empty : recommend.reference_name;
        }

        /// <summary>
        /// 保存数据到本地excel中
        /// </summary>
        /// <param name="growthData"></param>
        /// <param name="kingData"></param>
        /// <returns></returns>
        private string SaveExcel(List<ShopRetailRank> growthData, List<ShopRetailRank> kingData)
        {
            var dir = _config.MailAttachmentAddress;
            FolderHelper.ClearFolder(dir);
            var tableGrowth = ConvertToDataTable(growthData, "成长之星");
            var tableking = ConvertToDataTable(kingData, "订单王");
            var attachment = $"{dir}{_now.ToString("yyyyMMddHHmmssffff")}.xls";
            ExcelHelper.ExportExcel(new List<ExcelData>()
            {
                new ExcelData()
                {
                    Dt = tableGrowth,
                    ColumnHeader = new string[] {"门店ID","店长姓名", "门店账号", "周新增有商品销售销售单据数", "门店名称", "组织编码","省","市","区","推荐人"}
                },
                new ExcelData()
                {
                    Dt = tableking,
                    ColumnHeader = new string[] { "门店ID", "店长姓名", "门店账号", "活动累计有商品销售单据数", "门店名称", "组织编码", "省", "市", "区", "推荐人" }
                }
            }, attachment);
            return attachment;
        }

        /// <summary>
        /// 转换成DataTable
        /// </summary>
        /// <param name="data"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        private DataTable ConvertToDataTable(List<ShopRetailRank> data, string tableName)
        {
            var table = new DataTable(tableName);
            table.Columns.Add("ShopId", typeof(string));
            table.Columns.Add("ShopLinkMan", typeof(string));
            table.Columns.Add("ShopTelphone", typeof(string));
            table.Columns.Add("BillCount", typeof(string));
            table.Columns.Add("ShopName", typeof(string));
            table.Columns.Add("OrgNo", typeof(string));
            table.Columns.Add("ProvinceName", typeof(string));
            table.Columns.Add("CityName", typeof(string));
            table.Columns.Add("DistrictName", typeof(string));
            table.Columns.Add("RecommendName", typeof(string));
            foreach (var rank in data)
            {
                table.Rows.Add(rank.ShopId, rank.ShopLinkMan, rank.ShopTelphone, rank.BillCount, rank.ShopName, rank.OrgNo,
                    rank.ProvinceName, rank.CityName, rank.DistrictName, rank.RecommendName);
            }
            return table;
        }

        /// <summary>
        /// 发送附件
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private void SendMailAboutRank(string path)
        {
            var body = new StringBuilder();
            body.Append($"成长之星：{_startGrowth}—{_endGrowth}<br />");
            body.Append($"订单王：{_startKing}—{_endKing}");
            MailHelper.Send(_appSettings.MailConfig, new MailInfo()
            {
                AttachmentAddress = path,
                Body = body.ToString(),
                Recipients = _config.MailRecipients.Split(';'),
                Subject = "门店销售单排名"
            });
        }

        /// <summary>
        /// 处理获奖者
        /// 如果为星期天，则统计获奖者,并以邮件的形式发送获奖者名单，并将获奖者记录数据库
        /// </summary>
        /// <param name="growthData"></param>
        private void HandleWinner(List<ShopRetailRank> growthData)
        {
            if (_now.DayOfWeek != DayOfWeek.Sunday)
            {
                return;
            }
            var reachMinBillCountLimit = growthData.Where(p => p.BillCount >= _config.MinBillCountGrowth).OrderByDescending(p => p.BillCount).ToList();
            if (reachMinBillCountLimit.Count > _config.RankLimitCount)
            {
                reachMinBillCountLimit = reachMinBillCountLimit.GetRange(0, _config.RankLimitCount);
            }
            var path = SaveExcelAboutWinner(reachMinBillCountLimit);
            SendMailAboutWinner(path);
            if (reachMinBillCountLimit.Any())
            {
                _shopRetailRankWinnerBll.AddShopRetailRankWinner(reachMinBillCountLimit.Select(p =>
                    new ShopRetailRankLimit()
                    {
                        ShopId = p.ShopId,
                        Type = (int)ShopRetailRankLimitTypeEnum.Winner
                    }).ToList());
            }
        }

        /// <summary>
        /// 保存获奖者
        /// </summary>
        /// <param name="winnerData"></param>
        /// <returns></returns>
        private string SaveExcelAboutWinner(List<ShopRetailRank> winnerData)
        {
            var dir = _config.MailAttachmentAddress;
            var tableWinner = ConvertToDataTable(winnerData, "获奖者名单");
            var attachment = $"{dir}{"w" + _now.ToString("yyyyMMddHHmmssffff")}.xls";
            ExcelHelper.ExportExcel(new List<ExcelData>()
            {
                new ExcelData()
                {
                    Dt = tableWinner,
                    ColumnHeader = new string[] {"门店ID","店长姓名", "门店账号", "周新增有商品销售销售单据数", "门店名称", "组织编码", "省", "市", "区", "推荐人" }
                }
            }, attachment);
            return attachment;
        }

        /// <summary>
        /// 发送获奖者名单
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private void SendMailAboutWinner(string path)
        {
            var body = new StringBuilder();
            body.Append("获奖者名单:<br />");
            body.Append($"成长之星：{_startGrowth}—{_endGrowth}");
            MailHelper.Send(_appSettings.MailConfig, new MailInfo()
            {
                AttachmentAddress = path,
                Body = body.ToString(),
                Recipients = _config.MailRecipients.Split(';'),
                Subject = "获奖者名单"
            });
        }
    }
}
