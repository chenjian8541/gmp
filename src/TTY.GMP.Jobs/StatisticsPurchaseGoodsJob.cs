using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTY.GMP.Entity.Enum;
using TTY.GMP.IBusiness;
using TTY.GMP.IOC;
using TTY.GMP.LOG;
using TTY.GMP.Utility;

namespace TTY.GMP.Jobs
{
    /// <summary>
    /// 商品采购统计
    /// </summary>
    public class StatisticsPurchaseGoodsJob : IJob
    {
        /// <summary>
        /// 开始统计时间
        /// </summary>
        private DateTime _startTime;

        /// <summary>
        /// 结束时间
        /// </summary>
        private DateTime _endTime;

        /// <summary>
        /// 出入库业务访问
        /// </summary>
        private IStockInOutRecordBLL _stockInOutRecordBll;

        /// <summary>
        /// 报表访问业务
        /// </summary>
        private IReportBLL _reportBll;

        /// <summary>
        /// 初始化成员
        /// </summary>
        private void Initialize()
        {
            var now = DateTime.Now;
            _startTime = now.Date;
            _endTime = _startTime.AddDays(1).AddSeconds(-1);
            _stockInOutRecordBll = CustomServiceLocator.GetInstance<IStockInOutRecordBLL>();
            _reportBll = CustomServiceLocator.GetInstance<IReportBLL>();
        }

        /// <summary>
        /// 执行商品采购统计
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Execute(IJobExecutionContext context)
        {
            Log.Info("定时商品采购统计Job》》开始执行", this.GetType());
            Initialize();
            try
            {
                await Process();
                Log.Info("定时商品采购统计Job》》执行完成", this.GetType());
                Console.WriteLine($"{DateTime.Now}定时商品采购统计Job》》执行完成");
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex, this.GetType());
            }
        }

        /// <summary>
        /// 统计一天的零售商品
        /// </summary>
        private async Task Process()
        {
            var maxTime = (await _reportBll.GetStatisticsPurchaseGoodsMaxTime()).Date;
            while (maxTime < _startTime)
            {
                Execution().Wait();
                _endTime = _startTime.AddSeconds(-1);
                _startTime = _startTime.AddDays(-1);
            }
        }

        /// <summary>
        /// 执行
        /// </summary>
        /// <returns></returns>
        private async Task Execution()
        {
            var stockInOutRecord = await _stockInOutRecordBll.GetStockInOut(_startTime.ToTimestamp(), _endTime.ToTimestamp(),
                $"{(int)BillTypeEnum.ReceiptIn},{(int)BillTypeEnum.ReceiptBack}");
            if (stockInOutRecord == null || !stockInOutRecord.Any())
            {
                return;
            }
            foreach (var record in stockInOutRecord)
            {
                var goodsSpecAnalysis = Common.GoodsSpecAnalysis(record.GoodsSpec);
                var goodsContentsAnalysis = Common.GoodsContentsAnalysis(record.GoodsContents);
                var totalCount = record.TotalCount;
                var totalWeight = goodsSpecAnalysis * totalCount;
                var totalContentsWeight = totalWeight * goodsContentsAnalysis;
                _reportBll.AddStatisticsPurchaseGoods(new Entity.Database.StatisticsPurchaseGoods()
                {
                    GoodsId = record.GoodsId,
                    GoodsName = record.GoodsName,
                    GoodsContents = record.GoodsContents,
                    GoodsSpec = record.GoodsSpec,
                    GoodsCategoryId = record.GoodsCategoryId,
                    GoodsCategoryName = record.GoodsCategoryName,
                    ShopId = record.ShopId,
                    ShopName = record.ShopName,
                    Province = record.Province,
                    City = record.City,
                    District = record.District,
                    Street = record.Street,
                    OrgId = record.OrgId,
                    EndTime = _endTime,
                    StartTime = _startTime,
                    TotalCount = totalCount,
                    TotalWeight = totalWeight,
                    TotalContentsWeight = totalContentsWeight,
                    StatisticsId = PrimaryKeyHelper.Instance.CreateID()
                }).Wait();
            }
        }
    }
}
