using System;
using System.Collections.Generic;
using System.Text;
using TTY.GMP.Entity.Common;

namespace TTY.GMP.Entity.Open.Request
{
    /// <summary>
    /// 获取采购退货信息
    /// </summary>
    public class GetPurchaseBackPagingRequest : RequestPagingBase, IDataLimitRequest, IQuerySql
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
        /// 获取条件查询语句
        /// </summary>
        /// <returns></returns>
        public string GetQuerySql()
        {
            var str = new StringBuilder("f.receipt_back_red = 0");
            if (!string.IsNullOrEmpty(LimitShops))
            {
                str.AppendFormat(" AND f.user_shop_id IN ({0})", LimitShops);
            }
            return this.GetAreaLimitSql(str, "h.").ToString();
        }

        /// <summary>
        /// 数据校验
        /// </summary>
        /// <returns></returns>
        public override bool Validate()
        {
            if (PageSize > 100)
            {
                return false;
            }
            return base.Validate();
        }
    }
}
