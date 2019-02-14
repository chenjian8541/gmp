using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TTY.Event.GMP;
using TTY.GMP.IBusiness;
using TTY.GMP.IOC;
using TTY.GMP.LOG;
using System.Linq;
using TTY.GMP.EventConsumer.GovernmentBusiness;

namespace TTY.GMP.EventConsumer
{
    /// <summary>
    /// 零售退货
    /// </summary>
    [QueueConsumerAttribution("ErpSavaRetailBackBillCheckQueue")]
    public class ErpSavaRetailBackBillCheckConsumer : ConsumerBase<ErpSavaRetailBackBillCheckEvent>
    {
        /// <summary>
        /// 零售退货 
        /// </summary>
        /// <param name="eEvent"></param>
        /// <returns></returns>
        protected override async Task Receive(ErpSavaRetailBackBillCheckEvent eEvent)
        {
            var retailBack = await CustomServiceLocator.GetInstance<IRetailBackBLL>().GetSoRetailBack(eEvent.RetailBackId);
            var shop = await CustomServiceLocator.GetInstance<IShopBLL>().GetShop(retailBack.shop_id);
            if (shop.Province == null)
            {
                Log.Warn($"【零售退货消费者】门店未设置省份信息,参数:{JsonConvert.SerializeObject(eEvent)}", this.GetType());
                return;
            }
            var areas = await CustomServiceLocator.GetInstance<IAreaBLL>().GetArea();
            var province = areas.FirstOrDefault(p => p.AreaId == shop.Province.Value);
            var executor = SavaRetailBackBillCheckFactory.CreateExecutor(province?.AreaName);
            if (executor == null)
            {
                Log.Warn($"【零售退货消费者】未找到合适的处理者,参数:{JsonConvert.SerializeObject(eEvent)}", this.GetType());
                return;
            }
            try
            {
                await executor.Process(shop, retailBack, areas);
                Log.Info($"【零售退货消费者】消费成功,参数:{JsonConvert.SerializeObject(eEvent)}", this.GetType());
            }
            catch (Exception ex)
            {
                Log.Error($"【零售退货消费者】发生异常,参数:{JsonConvert.SerializeObject(eEvent)}", ex, this.GetType());
            }
        }
    }
}
