using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTY.GMP.Entity.Config;
using TTY.GMP.Entity.Database;
using TTY.GMP.Entity.Government.GuangDong.Request;
using TTY.GMP.IBusiness;
using TTY.GMP.IOC;
using TTY.GMP.LOG;
using TTY.GMP.Utility;

namespace TTY.GMP.EventConsumer.GovernmentBusiness.GuangDong
{
    /// <summary>
    /// 采购入库（广东省农药经营管理业务处理）
    /// </summary>
    public class GdErpAddReceipt : IErpAddReceipt
    {
        /// <summary>
        /// 处理逻辑
        /// </summary>
        /// <param name="shop"></param>
        /// <param name="inReceipt"></param>
        /// <param name="areas"></param>
        /// <returns></returns>
        public async Task Process(Shop shop, PoInReceipt inReceipt, List<Area> areas)
        {
            var inReceiptBll = CustomServiceLocator.GetInstance<IInReceiptBLL>();
            var baseSupplierBll = CustomServiceLocator.GetInstance<IBaseSupplierBLL>();
            var agencyBll = CustomServiceLocator.GetInstance<IAgencyBLL>();
            var governmentGuangDongBll = CustomServiceLocator.GetInstance<IGovernmentGuangDongBLL>();
            var appSettings = CustomServiceLocator.GetInstance<IAppConfigurtaionServices>().AppSettings;
            var agency = await Common.CreateAgency(appSettings.GovernmentApiConfig, agencyBll, governmentGuangDongBll, shop, areas);
            var poInReceiptDetails = await inReceiptBll.GetPoInReceiptDetailView(inReceipt.in_id);
            var supplier = await baseSupplierBll.GetBaseSupplier(inReceipt.in_offer_id);
            var inDate = inReceipt.in_date.ToDateTimeFromTimeStamp().ToString("yyyy-MM-dd");
            foreach (var inReceiptDetail in poInReceiptDetails)
            {
                var sgdRequest = new UploadSgdRequest()
                {
                    no = inReceipt.in_id.ToString(),
                    detailno = inReceiptDetail.in_detail_id.ToString(),
                    product_info_name = inReceiptDetail.goods_name,
                    product_info_id = string.Empty,
                    product_info_code = inReceiptDetail.goods_code,
                    nyjx = inReceiptDetail.dosage_forms,
                    nydx = inReceiptDetail.toxicity_grade_name,
                    yxq = string.Empty,
                    pzwh = string.Empty,
                    specification = inReceiptDetail.goods_spec,
                    zsm = string.Empty,
                    fromId = supplier.supplier_id.ToString(),
                    fromCode = supplier.supplier_code,
                    fromName = supplier.supplier_name,
                    gldx_company_code = string.Empty,
                    gldx_company_name = inReceiptDetail.goods_product,
                    gys = supplier.supplier_name,
                    djh = inReceiptDetail.registration_number,
                    batchno = string.Empty,
                    date = inDate,
                    total = inReceiptDetail.qty.ToString(),
                    price = "0",
                    totalprice = "0",
                    totalunit = inReceiptDetail.unit_name,
                    scrq = string.Empty,
                    isxzshop = 0,
                    isxz = inReceiptDetail.goods_restrictive,
                    info = string.Empty
                };
                var result = await governmentGuangDongBll.UploadSgd(appSettings.GovernmentApiConfig.GuangDong.ApiAddress,
                         appSettings.GovernmentApiConfig.GuangDong.QyCode, agency, sgdRequest);
                if (result.result.status != "0")
                {
                    Log.Warn($"【广东省农药经营管理系统平台】上传采购记录失败，参数in_id:{inReceiptDetail.in_id}", this.GetType());
                }
            }
        }
    }
}
