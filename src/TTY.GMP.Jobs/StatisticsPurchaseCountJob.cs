using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using TTY.GMP.Entity.Database;
using TTY.GMP.IBusiness;
using TTY.GMP.IOC;
using TTY.GMP.LOG;
using TTY.GMP.Utility;

namespace TTY.GMP.Jobs
{
    /// <summary>
    /// 定时统计采购单数Job
    /// </summary>
    public class StatisticsPurchaseCountJob : IJob
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
        /// 订单统计业务
        /// </summary>
        private IStatisticsBillCountBLL _statisticsBillCountBll;

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
            _statisticsBillCountBll = CustomServiceLocator.GetInstance<IStatisticsBillCountBLL>();
            _reportBll = CustomServiceLocator.GetInstance<IReportBLL>();
        }

        /// <summary>
        /// 执行采购单据数统计
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Execute(IJobExecutionContext context)
        {
            Log.Info("定时统计采购单数Job》》开始执行", this.GetType());
            Initialize();
            try
            {
                await Process();
                Log.Info("定时统计采购单数Job》》执行完成", this.GetType());
                Console.WriteLine($"{DateTime.Now}定时统计采购单数Job》》执行完成");
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex, this.GetType());
            }
        }

        /// <summary>
        /// 统计一天的采购单据数
        /// </summary>
        private async Task Process()
        {
            var maxTime = (await _reportBll.GetStatisticsPurchaseCountMaxTime()).Date;
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
            var retailBillCount = await _statisticsBillCountBll.GetAllPurchaseBillCount(_startTime.ToTimestamp(), _endTime.ToTimestamp());
            if (retailBillCount == null || !retailBillCount.Any())
            {
                return;
            }
            foreach (var record in retailBillCount)
            {
                _reportBll.AddStatisticsPurchaseCount(new StatisticsPurchaseCount()
                {
                    BillCount = record.BillCount,
                    City = record.City,
                    District = record.District,
                    EndTime = _endTime,
                    OrgId = record.OrgId,
                    Province = record.Province,
                    ShopId = record.ShopId,
                    ShopName = record.ShopName,
                    StartTime = _startTime,
                    Street = record.Street,
                    StatisticsId = PrimaryKeyHelper.Instance.CreateID()
                }).Wait();
            }
        }
    }
}
