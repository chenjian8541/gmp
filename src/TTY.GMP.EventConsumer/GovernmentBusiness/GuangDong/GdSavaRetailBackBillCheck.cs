using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TTY.GMP.Entity.Config;
using TTY.GMP.Entity.Database;
using TTY.GMP.IBusiness;
using TTY.GMP.IOC;
using System.Linq;
using TTY.GMP.LOG;

namespace TTY.GMP.EventConsumer.GovernmentBusiness.GuangDong
{
    /// <summary>
    /// 零售退货
    /// </summary>
    public class GdSavaRetailBackBillCheck : ISavaRetailBackBillCheck
    {
        /// <summary>
        /// 零售退货处理
        /// </summary>
        /// <param name="shop"></param>
        /// <param name="soRetailBack"></param>
        /// <param name="areas"></param>
        /// <returns></returns>
        public async Task Process(Shop shop, SoRetailBack soRetailBack, List<Area> areas)
        {
            var appSettings = CustomServiceLocator.GetInstance<IAppConfigurtaionServices>().AppSettings;
            var governmentGuangDongBll = CustomServiceLocator.GetInstance<IGovernmentGuangDongBLL>();
            var soRetailBackDetails = await CustomServiceLocator.GetInstance<IRetailBackBLL>().GetSoRetailBackDetail(soRetailBack.retail_back_id);
            var detailNos = string.Join(";", soRetailBackDetails.Select(p => p.retail_detail_id));
            var result = await governmentGuangDongBll.UpdXsdStatus(appSettings.GovernmentApiConfig.GuangDong.ApiAddress,
                  appSettings.GovernmentApiConfig.GuangDong.QyCode, soRetailBack.retail_bill_id.ToString(), detailNos,
                  "QX");
            if (result.result.status != "0")
            {
                Log.Warn($"【广东省农药经营管理系统平台】零售退货失败，retail_back_id:{soRetailBack.retail_back_id}", this.GetType());
            }
        }
    }
}
