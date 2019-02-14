using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TTY.GMP.Entity.Config;
using TTY.GMP.Entity.Database;
using TTY.GMP.IBusiness;
using TTY.GMP.IOC;
using TTY.GMP.LOG;

namespace TTY.GMP.EventConsumer.GovernmentBusiness.GuangDong
{
    /// <summary>
    /// 采购入库撤销
    /// </summary>
    public class GdErpReceiptInRed : IErpReceiptInRed
    {
        /// <summary>
        /// 处理采购入库撤销
        /// </summary>
        /// <param name="shop"></param>
        /// <param name="inReceipt"></param>
        /// <param name="areas"></param>
        /// <returns></returns>
        public async Task Process(Shop shop, PoInReceipt inReceipt, List<Area> areas)
        {
            var appSettings = CustomServiceLocator.GetInstance<IAppConfigurtaionServices>().AppSettings;
            var governmentGuangDongBll = CustomServiceLocator.GetInstance<IGovernmentGuangDongBLL>();
            var result = await governmentGuangDongBll.UpdSgdStatus(appSettings.GovernmentApiConfig.GuangDong.ApiAddress,
                appSettings.GovernmentApiConfig.GuangDong.QyCode, inReceipt.in_id.ToString(), "QX");
            if (result.result.status != "0")
            {
                Log.Warn($"【广东省农药经营管理系统平台】采购撤销失败，in_id:{inReceipt.in_id}", this.GetType());
            }
        }
    }
}
