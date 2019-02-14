using System;
using System.Collections.Generic;
using System.Text;
using TTY.GMP.Entity.Common;

namespace TTY.GMP.Entity.Web.Common.Request
{
    /// <summary>
    /// 获取系统统计信息
    /// </summary>
    public class GetSystemStatisticsRequest : IDataLimitRequest, IQuerySql
    {
        /// <summary>
        /// 限制门店
        /// 门店之间以","隔开
        /// </summary>
        public string LimitShops { get; set; }

        /// <summary>
        /// 限制省
        /// 省之间以“,”隔开
        /// </summary>
        public string LimitProvince { get; set; }

        /// <summary>
        /// 限制市
        /// 市之间以“,”隔开
        /// </summary>
        public string LimitCity { get; set; }

        /// <summary>
        /// 限制区
        /// 区之间以“,”隔开
        /// </summary>
        public string LimitDistrict { get; set; }

        /// <summary>
        /// 获取查询条件(销售数)
        /// </summary>
        /// <returns></returns>
        public string GetQuerySql()
        {
            var str = new StringBuilder(" 1 = 1");
            if (!string.IsNullOrEmpty(LimitShops))
            {
                str.AppendFormat(" AND b.shop_id IN ({0})", LimitShops);
            }
            return this.GetAreaLimitSql(str, "b.").ToString();
        }

        /// <summary>
        /// 获取查询条件(门店数)
        /// </summary>
        /// <returns></returns>
        public string GetQuerySql2()
        {
            var str = new StringBuilder(" `status` = 1");
            if (!string.IsNullOrEmpty(LimitShops))
            {
                str.AppendFormat(" AND shop_id IN ({0})", LimitShops);
            }
            return this.GetAreaLimitSql(str).ToString();
        }
    }
}
