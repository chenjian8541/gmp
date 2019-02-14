using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using TTY.GMP.Entity.Enum;
using TTY.GMP.IBusiness;
using TTY.GMP.IOC;
using TTY.GMP.LOG;

namespace TTY.GMP.Jobs
{
    /// <summary>
    /// 定时统计各地区的门店总数
    /// </summary>
    [DisallowConcurrentExecution]
    public class UpdateShopStatisticsJob : IJob
    {
        /// <summary>
        /// 执行统计
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task Execute(IJobExecutionContext context)
        {
            Log.Info("定时统计各地区的门店总数Job》》开始执行", this.GetType());
            try
            {
                Process();
                Log.Info("定时统计各地区的门店总数Job》》执行完成", this.GetType());
                Console.WriteLine($"{DateTime.Now}定时统计各地区的门店总数Job》》执行完成");
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex, this.GetType());
            }
            return Task.CompletedTask;
        }

        /// <summary>
        /// 执行更新门店统计信息
        /// </summary>
        private void Process()
        {
            var shopStatisticsBll = CustomServiceLocator.GetInstance<IShopStatisticsBLL>();
            shopStatisticsBll.UpdateShopStatistics(ShopStatisticsTypeEnum.Province);
            shopStatisticsBll.UpdateShopStatistics(ShopStatisticsTypeEnum.City);
        }
    }
}
