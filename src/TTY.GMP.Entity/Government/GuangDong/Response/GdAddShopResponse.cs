using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace TTY.GMP.Entity.Government.GuangDong.Response
{
    /// <summary>
    /// 经销商数据上传 响应数据
    /// </summary>
    [XmlRoot("data")]
    public class GdAddShopResponse
    {
        [XmlElement("dataInfo")]
        public GdShop Shop;
    }

    /// <summary>
    /// 经销商信息
    /// </summary>
    [XmlType("dataInfo")]
    public class GdShop
    {
        /// <summary>
        /// 经销商id
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// 经销商名称 
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 经销商编码
        /// </summary>
        public string code { get; set; }

        /// <summary>
        /// 负责人
        /// </summary>
        public string headMan { get; set; }

        /// <summary>
        /// 负责人电话
        /// </summary>
        public string headPhone { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        public string linkMan { get; set; }

        /// <summary>
        /// 联系人电话
        /// </summary>
        public string linkPhone { get; set; }

        /// <summary>
        /// 省
        /// </summary>
        public string province { get; set; }

        /// <summary>
        /// 市
        /// </summary>
        public string city { get; set; }

        /// <summary>
        /// 区
        /// </summary>
        public string country { get; set; }

        /// <summary>
        /// 街
        /// </summary>
        public string town { get; set; }

        /// <summary>
        /// 所属省编码
        /// </summary>
        public string provinceCode { get; set; }

        /// <summary>
        /// 所属市编码
        /// </summary>
        public string cityCode { get; set; }

        /// <summary>
        /// 所属区编码
        /// </summary>
        public string countryCode { get; set; }

        /// <summary>
        /// 所属街编码
        /// </summary>
        public string townCode { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string address { get; set; }

        /// <summary>
        /// 信息
        /// </summary>
        public string info { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        public string account { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string password { get; set; }

        /// <summary>
        /// 邮寄地址
        /// </summary>
        public string yjdz { get; set; }

        /// <summary>
        /// 邮政编码
        /// </summary>
        public string postCode { get; set; }

        /// <summary>
        /// 是否试点（0：否;1：是）
        /// </summary>
        public string isSd { get; set; }

        /// <summary>
        /// 是否限用农药经销商（0：否;1：是）
        /// </summary>
        public string isxzshop { get; set; }
    }
}
