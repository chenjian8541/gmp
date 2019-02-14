using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace TTY.GMP.Entity.Government.GuangDong.Response
{
    /// <summary>
    /// 一般返回信息
    /// </summary>
    [XmlRoot("data")]
    public class GdCommonResponse
    {
        /// <summary>
        /// 返回信息
        /// </summary>
        [XmlElement("dataInfo")]
        public CommonData result;
    }

    /// <summary>
    /// 一般返回信息
    /// </summary>
    [XmlType("dataInfo")]
    public class CommonData
    {
        /// <summary>
        /// 状态
        /// </summary>
        public string status;

        /// <summary>
        /// 描述
        /// </summary>
        public string msg;
    }
}
