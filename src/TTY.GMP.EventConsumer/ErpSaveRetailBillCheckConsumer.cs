using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TTY.GMP.Entity.Config;
using TTY.Event.GMP;
using TTY.GMP.Http;
using TTY.GMP.IBusiness;
using TTY.GMP.IOC;
using TTY.GMP.LOG;
using System.Linq;
using TTY.GMP.EventConsumer.GovernmentBusiness;

namespace TTY.GMP.EventConsumer
{
    /// <summary>
    /// 零售下单
    /// </summary>
    [QueueConsumerAttribution("ErpSaveRetailBillCheckQueue")]
    public class ErpSaveRetailBillCheckConsumer : ConsumerBase<ErpSaveRetailBillCheckEvent>
    {
        /// <summary>
        /// 零售下单
        /// </summary>
        /// <param name="eEvent"></param>
        /// <returns></returns>
        protected override async Task Receive(ErpSaveRetailBillCheckEvent eEvent)
        {
            var retail = await CustomServiceLocator.GetInstance<IRetailBLL>().GetSoRetail(eEvent.RetailId);
            var shop = await CustomServiceLocator.GetInstance<IShopBLL>().GetShop(retail.ShopId);
            if (shop.Province == null)
            {
                Log.Warn($"【零售下单消费者】门店未设置省份信息,参数:{JsonConvert.SerializeObject(eEvent)}", this.GetType());
                return;
            }
            var areas = await CustomServiceLocator.GetInstance<IAreaBLL>().GetArea();
            var province = areas.FirstOrDefault(p => p.AreaId == shop.Province.Value);
            var executor = SaveRetailBillCheckFactory.CreateExecutor(province?.AreaName);
            if (executor == null)
            {
                Log.Warn($"【零售下单消费者】未找到合适的处理者,参数:{JsonConvert.SerializeObject(eEvent)}", this.GetType());
                return;
            }
            try
            {
                await executor.Process(shop, retail, areas);
                Log.Info($"【零售下单消费者】消费成功,参数:{JsonConvert.SerializeObject(eEvent)}", this.GetType());
            }
            catch (Exception ex)
            {
                Log.Error($"【零售下单消费者】发生异常,参数:{JsonConvert.SerializeObject(eEvent)}", ex, this.GetType());
            }
        }
    }
}
