using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using TTY.GMP.Entity.Government.GuangDong.Request;
using TTY.GMP.Entity.Government.GuangDong.Response;
using TTY.GMP.Http;
using TTY.GMP.IBusiness;
using TTY.GMP.Utility;

namespace TTY.GMP.Business
{
    /// <summary>
    /// 广东省农药经营管理系统平台通讯接口
    /// </summary>
    public class GovernmentGuangDongBLL : IGovernmentGuangDongBLL
    {
        /// <summary>
        /// http请求帮助类
        /// </summary>
        private readonly IHttpClient _httpClient;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="httpClient"></param>
        public GovernmentGuangDongBLL(IHttpClient httpClient)
        {
            this._httpClient = httpClient;
        }

        /// <summary>
        /// 获取地区信息
        /// </summary>
        /// <param name="address"></param>
        /// <param name="qyCode"></param>
        /// <returns></returns>
        public async Task<GetAreasResponse> GetAreas(string address, string qyCode)
        {
            var url = $"{address}baseService/getAreas?wbqyCode={qyCode}";
            var result = await _httpClient.GetStringAsync(url);
            return SerializerHelper.DeserializeXml<GetAreasResponse>(FilteringData(result));
        }

        /// <summary>
        /// 经销商数据上传
        /// </summary>
        /// <param name="address"></param>
        /// <param name="qyCode"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<GdAddShopResponse> AddShopInfo(string address, string qyCode, GdAddShopRequest request)
        {
            var url = $"{address}baseService/addShopInfo?data=<gldxshop>{PropertyInfoToXml(request)}</gldxshop>&wbqyCode={qyCode}";
            var result = await _httpClient.GetStringAsync(url);
            return SerializerHelper.DeserializeXml<GdAddShopResponse>(FilteringData(result));
        }

        /// <summary>
        /// 销售记录上传
        /// </summary>
        /// <param name="address"></param>
        /// <param name="qyCode"></param>
        /// <param name="comRequest"></param>
        /// <param name="xsdRequest"></param>
        /// <returns></returns>
        public async Task<GdCommonResponse> UploadXsd(string address, string qyCode, GdCommonRequest comRequest, UploadXsdRequest xsdRequest)
        {
            var url = $"{address}baseService/uploadXsd?data=<data><agency>{PropertyInfoToXml(comRequest)}</agency><dataInfo>{PropertyInfoToXml(xsdRequest)}</dataInfo></data>&wbqyCode={qyCode}";
            var result = await _httpClient.GetStringAsync(url);
            return SerializerHelper.DeserializeXml<GdCommonResponse>(FilteringData(result));
        }

        /// <summary>
        /// 采购记录上传
        /// </summary>
        /// <param name="address"></param>
        /// <param name="qyCode"></param>
        /// <param name="comRequest"></param>
        /// <param name="sgdRequest"></param>
        /// <returns></returns>
        public async Task<GdCommonResponse> UploadSgd(string address, string qyCode, GdCommonRequest comRequest, UploadSgdRequest sgdRequest)
        {
            var url = $"{address}baseService/uploadSgd?data=<data><agency>{PropertyInfoToXml(comRequest)}</agency><dataInfo>{PropertyInfoToXml(sgdRequest)}</dataInfo></data>&wbqyCode={qyCode}";
            var result = await _httpClient.GetStringAsync(url);
            return SerializerHelper.DeserializeXml<GdCommonResponse>(FilteringData(result));
        }

        /// <summary>
        /// 更改销售单状态
        /// </summary>
        /// <param name="address"></param>
        /// <param name="qyCode"></param>
        /// <param name="nos"></param>
        /// <param name="detailNos"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public async Task<GdCommonResponse> UpdXsdStatus(string address, string qyCode, string nos, string detailNos, string type)
        {
            var url = $"{address}baseService/updXsdStatus?nos={nos}&detailNos={detailNos}&type={type}&wbqyCode={qyCode}";
            var result = await _httpClient.GetStringAsync(url);
            return SerializerHelper.DeserializeXml<GdCommonResponse>(FilteringData(result));
        }

        /// <summary>
        /// 更改采购单状态
        /// </summary>
        /// <param name="address"></param>
        /// <param name="qyCode"></param>
        /// <param name="nos"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public async Task<GdCommonResponse> UpdSgdStatus(string address, string qyCode, string nos, string type)
        {
            var url = $"{address}baseService/updSgdStatus?nos={nos}&type={type}&wbqyCode={qyCode}";
            var result = await _httpClient.GetStringAsync(url);
            return SerializerHelper.DeserializeXml<GdCommonResponse>(FilteringData(result));
        }

        #region 公共私有方法

        /// <summary>
        /// 对返回的数据进行过滤
        /// </summary>
        /// <param name="apiData"></param>
        /// <returns></returns>
        private string FilteringData(string apiData)
        {
            return HttpUtility.HtmlDecode(apiData).Substring("<return>", "</return>");
        }

        /// <summary>
        /// 实体转XML
        /// </summary>
        /// <param name="entity">实体类</param>
        public string PropertyInfoToXml(object entity)
        {
            var sb = new StringBuilder();
            var propinfos = entity.GetType().GetProperties();
            if (propinfos.Length == 0)
            {
                return string.Empty;
            }
            foreach (var propinfo in propinfos)
            {
                var value = propinfo.GetValue(entity);
                if (value == null)
                {
                    value = string.Empty;
                }
                sb.AppendFormat("<{0}>{1}</{0}>", propinfo.Name, value);
            }
            return sb.ToString();
        }

        #endregion
    }
}
