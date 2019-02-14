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
    /// 采购退货
    /// </summary>
    [QueueConsumerAttribution("ErpSaveReceilBackBillCheckQueue")]
    public class ErpSaveReceilBackBillCheckConsumer : ConsumerBase<ErpSaveReceilBackBillCheckEvent>
    {
        /// <summary>
        /// 采购退货
        /// </summary>
        /// <param name="eEvent"></param>
        /// <returns></returns>
        protected override async Task Receive(ErpSaveReceilBackBillCheckEvent eEvent)
        {
            var receiptBack = await CustomServiceLocator.GetInstance<IReceiptBackBLL>().GetPoBackReceipt(eEvent.BackId);
            var shop = await CustomServiceLocator.GetInstance<IShopBLL>().GetShop(receiptBack.user_shop_id);
            if (shop.Province == null)
            {
                Log.Warn($"【采购退货消费者】门店未设置省份信息,参数:{JsonConvert.SerializeObject(eEvent)}", this.GetType());
                return;
            }
            var areas = await CustomServiceLocator.GetInstance<IAreaBLL>().GetArea();
            var province = areas.FirstOrDefault(p => p.AreaId == shop.Province.Value);
            var executor = SaveReceilBackBillCheckFactory.CreateExecutor(province?.AreaName);
            if (executor == null)
            {
                Log.Warn($"【采购退货消费者】未找到合适的处理者,参数:{JsonConvert.SerializeObject(eEvent)}", this.GetType());
                return;
            }
            try
            {
                await executor.Process(shop, receiptBack, areas);
                Log.Info($"【采购退货消费者】消费成功,参数:{JsonConvert.SerializeObject(eEvent)}", this.GetType());
            }
            catch (Exception ex)
            {
                Log.Error($"【采购退货消费者】发生异常,参数:{JsonConvert.SerializeObject(eEvent)}", ex, this.GetType());
            }
        }
    }
}
