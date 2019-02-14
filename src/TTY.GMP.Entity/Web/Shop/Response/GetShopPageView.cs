using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using TTY.GMP.Entity.Common;

namespace TTY.GMP.Entity.Web.Shop.Response
{
    /// <summary>
    /// 通过地区获取店面信息
    /// </summary>
    public class GetShopPageView
    {
        /// <summary>
        /// 门店ID
        /// </summary>
        public long ShopId { get; set; }

        /// <summary>
        /// 门店名称
        /// </summary>
        public string ShopName { get; set; }

        /// <summary>
        /// 联系号码
        /// </summary>
        public string ShopTelphone { get; set; }

        /// <summary>
        /// 省
        /// </summary>
        public string ProvinceName { get; set; }

        /// <summary>
        /// 市
        /// </summary>
        public string CityName { get; set; }

        /// <summary>
        /// 区
        /// </summary>
        public string DistrictName { get; set; }

        /// <summary>
        /// 经营地址
        /// </summary>
        public string ShopAddress { get; set; }

        /// <summary>
        /// 许可证号
        /// </summary>
        public string LicenseNum { get; set; }

        /// <summary>
        /// 负责人
        /// </summary>
        public string LeaderName { get; set; }
    }
}
