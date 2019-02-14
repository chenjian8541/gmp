using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace TTY.GMP.Entity.Government.GuangDong.Response
{
    /// <summary>
    /// 地区列表
    /// </summary>
    [XmlRoot("data")]
    public class GetAreasResponse
    {
        /// <summary>
        /// 地区信息
        /// </summary>
        [XmlElement("dataInfo")]
        public List<Area> Areas { get; set; }
    }

    /// <summary>
    /// 地区
    /// </summary>
    [XmlType("dataInfo")]
    public class Area
    {
        /// <summary>
        /// ID
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// 级别
        /// </summary>
        public int level { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 父ID
        /// </summary>
        public string parent_id { get; set; }
    }
}
