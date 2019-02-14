using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TTY.Event.GMP;
using TTY.GMP.EventConsumer.GovernmentBusiness;
using TTY.GMP.IBusiness;
using TTY.GMP.IOC;
using TTY.GMP.LOG;

namespace TTY.GMP.EventConsumer
{
    /// <summary>
    /// 采购撤销
    /// </summary>
    [QueueConsumerAttribution("ErpReceiptInRedQueue")]
    public class ErpReceiptInRedConsumer : ConsumerBase<ErpReceiptInRedEvent>
    {
        /// <summary>
        /// 处理采购撤销
        /// </summary>
        /// <param name="eEvent"></param>
        /// <returns></returns>
        protected override async Task Receive(ErpReceiptInRedEvent eEvent)
        {
            var inReceipt = await CustomServiceLocator.GetInstance<IInReceiptBLL>().GetPoInReceipt(eEvent.InId);
            var shop = await CustomServiceLocator.GetInstance<IShopBLL>().GetShop(inReceipt.user_shop_id);
            if (shop.Province == null)
            {
                Log.Warn($"【采购撤销消费者】门店未设置省份信息,参数:{JsonConvert.SerializeObject(eEvent)}", this.GetType());
                return;
            }
            var areas = await CustomServiceLocator.GetInstance<IAreaBLL>().GetArea();
            var province = areas.FirstOrDefault(p => p.AreaId == shop.Province.Value);
            var executor = ReceiptInRedFactory.CreateExecutor(province?.AreaName);
            if (executor == null)
            {
                Log.Warn($"【采购撤销消费者】未找到合适的处理者,参数:{JsonConvert.SerializeObject(eEvent)}", this.GetType());
                return;
            }
            try
            {
                await executor.Process(shop, inReceipt, areas);
                Log.Info($"【采购撤销消费者】消费成功,参数:{JsonConvert.SerializeObject(eEvent)}", this.GetType());
            }
            catch (Exception ex)
            {
                Log.Error($"【采购撤销消费者】发生异常,参数:{JsonConvert.SerializeObject(eEvent)}", ex, this.GetType());
            }
        }
    }
}
