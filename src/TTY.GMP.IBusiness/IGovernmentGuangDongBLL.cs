using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TTY.GMP.Entity.Government.GuangDong.Request;
using TTY.GMP.Entity.Government.GuangDong.Response;

namespace TTY.GMP.IBusiness
{
    /// <summary>
    /// 广东省农药经营管理系统平台通讯接口
    /// </summary>
    public interface IGovernmentGuangDongBLL
    {
        /// <summary>
        /// 获取地区信息
        /// </summary>
        /// <param name="address"></param>
        /// <param name="qyCode"></param>
        /// <returns></returns>
        Task<GetAreasResponse> GetAreas(string address, string qyCode);

        /// <summary>
        /// 经销商数据上传
        /// </summary>
        /// <param name="address"></param>
        /// <param name="qyCode"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<GdAddShopResponse> AddShopInfo(string address, string qyCode, GdAddShopRequest request);

        /// <summary>
        /// 销售记录上传
        /// </summary>
        /// <param name="address"></param>
        /// <param name="qyCode"></param>
        /// <param name="comRequest"></param>
        /// <param name="xsdRequest"></param>
        /// <returns></returns>
        Task<GdCommonResponse> UploadXsd(string address, string qyCode, GdCommonRequest comRequest, UploadXsdRequest xsdRequest);

        /// <summary>
        /// 采购记录上传
        /// </summary>
        /// <param name="address"></param>
        /// <param name="qyCode"></param>
        /// <param name="comRequest"></param>
        /// <param name="sgdRequest"></param>
        /// <returns></returns>
        Task<GdCommonResponse> UploadSgd(string address, string qyCode, GdCommonRequest comRequest, UploadSgdRequest sgdRequest);

        /// <summary>
        /// 更改销售单状态
        /// </summary>
        /// <param name="address"></param>
        /// <param name="qyCode"></param>
        /// <param name="nos"></param>
        /// <param name="detailNos"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        Task<GdCommonResponse> UpdXsdStatus(string address, string qyCode, string nos, string detailNos, string type);

        /// <summary>
        /// 更改采购单状态
        /// </summary>
        /// <param name="address"></param>
        /// <param name="qyCode"></param>
        /// <param name="nos"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        Task<GdCommonResponse> UpdSgdStatus(string address, string qyCode, string nos, string type);
    }
}
