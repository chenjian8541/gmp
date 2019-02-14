using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using TTY.GMP.IBusiness;
using TTY.GMP.IOC;
using TTY.GMP.LOG;
using TTY.GMP.Utility;
using System.Linq;
using TTY.GMP.Entity.Database;

namespace TTY.GMP.Jobs
{
    /// <summary>
    /// 定时更新商品库存
    /// </summary>
    public class StatisticsStockGoodsJob : IJob
    {
        /// <summary>
        /// 库存业务访问
        /// </summary>
        private IStockQtyBLL _stockQtyBll;

        /// <summary>
        /// 报表访问
        /// </summary>
        private IReportBLL _reportBll;

        /// <summary>
        /// 初始化成员
        /// </summary>
        private void Initialize()
        {
            _stockQtyBll = CustomServiceLocator.GetInstance<IStockQtyBLL>();
            _reportBll = CustomServiceLocator.GetInstance<IReportBLL>();
        }

        /// <summary>
        /// 商品库存更新
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Execute(IJobExecutionContext context)
        {
            Log.Info("定时更新商品库存Job》》开始执行", this.GetType());
            Initialize();
            try
            {
                await Process();
                Log.Info("定时更新商品库存Job》》执行完成", this.GetType());
                Console.WriteLine($"{DateTime.Now}定时更新商品库存Job》》执行完成");
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex, this.GetType());
            }
        }

        /// <summary>
        /// 执行更新商品库存
        /// </summary>
        /// <returns></returns>
        private async Task Process()
        {
            var lastUpdateTime = await _reportBll.GetStockGoodsLastUpdateTime();
            var recentModifyStockGoods = await _stockQtyBll.GetRecentModifyStockGoods(lastUpdateTime.ToTimestamp());
            if (recentModifyStockGoods == null || !recentModifyStockGoods.Any())
            {
                return;
            }
            foreach (var stockGoods in recentModifyStockGoods)
            {
                var goodsSpecAnalysis = Common.GoodsSpecAnalysis(stockGoods.GoodsSpec);
                var goodsContentsAnalysis = Common.GoodsContentsAnalysis(stockGoods.GoodsContents);
                var totalCount = stockGoods.TotalCount;
                var totalWeight = goodsSpecAnalysis * totalCount;
                var totalContentsWeight = totalWeight * goodsContentsAnalysis;
                _reportBll.SaveStatisticsStockGoods(new StatisticsStockGoods()
                {
                    City = stockGoods.City,
                    District = stockGoods.District,
                    GoodsId = stockGoods.GoodsId,
                    GoodsCategoryId = stockGoods.GoodsCategoryId,
                    GoodsCategoryName = stockGoods.GoodsCategoryName,
                    GoodsContents = stockGoods.GoodsContents,
                    GoodsName = stockGoods.GoodsName,
                    GoodsSpec = stockGoods.GoodsSpec,
                    OrgId = stockGoods.OrgId,
                    Province = stockGoods.Province,
                    ShopId = stockGoods.ShopId,
                    ShopName = stockGoods.ShopName,
                    Street = stockGoods.Street,
                    TotalContentsWeight = totalContentsWeight,
                    TotalCount = totalCount,
                    TotalWeight = totalWeight,
                    UpdateTime = DateTime.Now,
                    StatisticsId = PrimaryKeyHelper.Instance.CreateID()
                }).Wait();
            }
        }
    }
}
