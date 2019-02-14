using System;
using System.Collections.Generic;
using System.Text;

namespace TTY.GMP.Entity.Common
{
    /// <summary>
    /// 分页请求响应基类
    /// </summary>
    public class ResponsePagingBase : ResponseBase
    {
        /// <summary>
        /// 记录数
        /// </summary>
        public int RecordCount { get; set; }

        /// <summary>
        /// 执行成功
        /// </summary>
        /// <param name="entity">成功返回的数据信息</param>
        /// <param name="recordCount">总条数</param>
        /// <returns></returns>
        public static ResponsePagingBase Success(object entity, int recordCount)
        {
            var response = new ResponsePagingBase();
            response.code = StatusCode.Succeed;
            response.resultdata = entity;
            response.RecordCount = recordCount;
            return response;
        }
    }
}
