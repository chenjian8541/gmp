using System;
using System.Collections.Generic;
using System.Text;

namespace TTY.GMP.Entity.Platform.Response
{
    /// <summary>
    /// 平台接口返回数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PfResponse<T>
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public int ret { get; set; }

        /// <summary>
        /// 返回数据
        /// </summary>
        public DataModel<T> data { get; set; }

        /// <summary>
        /// 返回信息
        /// </summary>
        public string msg { get; set; }
    }

    /// <summary>
    /// 数据Model
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    public class DataModel<T>
    {
        /// <summary>
        /// 返回码
        /// </summary>
        public int code { get; set; }
        /// <summary>
        /// 返回数据
        /// </summary>
        public T info { get; set; }
        /// <summary>
        /// 返回信息
        /// </summary>
        public string msg { get; set; }
    }
}
