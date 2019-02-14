using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TTY.GMP.Entity.Config;
using TTY.GMP.Entity.Database;
using TTY.GMP.Entity.Government.GuangDong.Request;
using TTY.GMP.IBusiness;
using TTY.GMP.IOC;
using System.Linq;
using TTY.GMP.Utility;
using TTY.GMP.LOG;

namespace TTY.GMP.EventConsumer.GovernmentBusiness.GuangDong
{
    /// <summary>
    /// 零售下单（广东省农药经营管理业务处理）
    /// </summary>
    public class GdSaveRetailBillCheck : ISaveRetailBillCheck
    {
        /// <summary>
        /// 处理逻辑
        /// </summary>
        /// <param name="shop"></param>
        /// <param name="soRetail"></param>
        /// <param name="areas"></param>
        public async Task Process(Shop shop, SoRetail soRetail, List<Area> areas)
        {
            var retailBll = CustomServiceLocator.GetInstance<IRetailBLL>();
            var agencyBll = CustomServiceLocator.GetInstance<IAgencyBLL>();
            var governmentGuangDongBll = CustomServiceLocator.GetInstance<IGovernmentGuangDongBLL>();
            var appSettings = CustomServiceLocator.GetInstance<IAppConfigurtaionServices>().AppSettings;
            var agency = await Common.CreateAgency(appSettings.GovernmentApiConfig, agencyBll, governmentGuangDongBll, shop, areas);
            var soRetailDetails = await retailBll.GetSoRetailDetail(soRetail.RetailId);
            var cmRetailCustomer = await retailBll.GetCmRetailCustomer(soRetail.RetailId);
            var billDate = soRetail.BillDate.ToDateTimeFromTimeStamp().ToString("yyyy-MM-dd");
            foreach (var retailDetail in soRetailDetails)
            {
                Area toProvince = null;
                if (cmRetailCustomer != null && cmRetailCustomer.Province != null)
                {
                    toProvince = areas.FirstOrDefault(p => p.AreaId == cmRetailCustomer.Province.Value);
                }
                var xsdRequest = new UploadXsdRequest()
                {
                    no = soRetail.RetailId.ToString(),
                    detailno = retailDetail.retail_detail_id.ToString(),
                    product_info_name = retailDetail.goods_name,
                    product_info_id = string.Empty,
                    product_info_code = retailDetail.goods_code,
                    nyjx = retailDetail.dosage_forms,
                    nydx = retailDetail.toxicity_grade_name,
                    scrq = string.Empty,
                    yxq = string.Empty,
                    pzwh = string.Empty,
                    specification = retailDetail.goods_spec,
                    zsm = string.Empty,
                    toaddress = cmRetailCustomer?.AddressDetail,
                    topost = string.Empty,
                    touser = cmRetailCustomer?.RetailCustomerName,
                    toheadmen = cmRetailCustomer?.RetailCustomerName,
                    toidcard = cmRetailCustomer?.Identification,
                    toprovince = toProvince?.AreaName,
                    tophone = cmRetailCustomer?.RetailCustomerTel,
                    gldx_company_code = string.Empty,
                    gldx_company_name = retailDetail.goods_product,
                    djh = retailDetail.registration_number,
                    batchno = string.Empty,
                    date = billDate,
                    total = retailDetail.qty.ToString(),
                    price = "0",
                    totalprice = "0",
                    totalunit = retailDetail.unit_name,
                    isxzshop = 0,
                    isxz = retailDetail.goods_restrictive,
                    info = soRetail.Explain
                };
                var result = await governmentGuangDongBll.UploadXsd(appSettings.GovernmentApiConfig.GuangDong.ApiAddress,
                         appSettings.GovernmentApiConfig.GuangDong.QyCode, agency, xsdRequest);
                if (result.result.status != "0")
                {
                    Log.Warn($"【广东省农药经营管理系统平台】上传销售记录失败，参数retail_detail_id:{retailDetail.retail_detail_id}", this.GetType());
                }
            }
        }
    }
}
