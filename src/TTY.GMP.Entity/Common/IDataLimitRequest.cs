using System;
using System.Collections.Generic;
using System.Text;

namespace TTY.GMP.Entity.Common
{
    /// <summary>
    /// 数据权限
    /// </summary>
    public interface IDataLimitRequest
    {
        /// <summary>
        /// 限制门店
        /// 门店之间以","隔开
        /// </summary>
        string LimitShops { get; set; }

        /// <summary>
        /// 限制省
        /// 省之间以“,”隔开
        /// </summary>
        string LimitProvince { get; set; }

        /// <summary>
        /// 限制市
        /// 市之间以“,”隔开
        /// </summary>
        string LimitCity { get; set; }

        /// <summary>
        /// 限制区
        /// 区之间以“,”隔开
        /// </summary>
        string LimitDistrict { get; set; }
    }

    /// <summary>
    /// 数据权限
    /// </summary>
    public static class DataLimitHelper
    {
        /// <summary>
        /// 获取地区过滤条件
        /// </summary>
        /// <param name="request"></param>
        /// <param name="sql"></param>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public static StringBuilder GetAreaLimitSql(this IDataLimitRequest request, StringBuilder sql, string prefix = "")
        {
            var areaSql = new StringBuilder();
            if (!string.IsNullOrEmpty(request.LimitProvince))
            {
                areaSql.AppendFormat(" {0}province IN ({1}) OR", prefix, request.LimitProvince);
            }
            if (!string.IsNullOrEmpty(request.LimitCity))
            {
                areaSql.AppendFormat(" {0}city IN ({1}) OR", prefix, request.LimitCity);
            }
            if (!string.IsNullOrEmpty(request.LimitDistrict))
            {
                areaSql.AppendFormat(" {0}district IN ({1}) OR", prefix, request.LimitDistrict);
            }
            if (areaSql.Length > 0)
            {
                sql.AppendFormat(" AND ({0})", areaSql.ToString().TrimEnd('R').TrimEnd('O'));
            }
            return sql;
        }
    }
}
