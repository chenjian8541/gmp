using System;
using System.Collections.Generic;
using System.Text;

namespace TTY.GMP.Entity.Government.GuangDong.Request
{
    /// <summary>
    /// 公共请求
    /// </summary>
    public class GdCommonRequest
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="id"></param>
        /// <param name="code"></param>
        /// <param name="name"></param>
        public GdCommonRequest(string id, string code, string name)
        {
            this.id = id;
            this.code = code;
            this.name = name;
        }

        /// <summary>
        /// 经销商id
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// 经销商编码
        /// </summary>
        public string code { get; set; }

        /// <summary>
        /// 经销商名称
        /// </summary>
        public string name { get; set; }
    }
}
