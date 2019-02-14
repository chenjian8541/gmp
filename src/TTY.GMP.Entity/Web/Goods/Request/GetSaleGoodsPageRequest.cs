using System;
using System.Collections.Generic;
using System.Text;
using TTY.GMP.Entity.Common;

namespace TTY.GMP.Entity.Web.Goods.Request
{
    /// <summary>
    /// 分页获取销售商品
    /// </summary>
    public class GetSaleGoodsPageRequest : RequestPagingBase, IQuerySql
    {
        /// <summary>
        ///门店ID
        /// </summary>
        public long ShopId { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string GoodsName { get; set; }

        /// <summary>
        /// 分类名称
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// 生产企业
        /// </summary>
        public string RegistrationHolder { get; set; }

        /// <summary>
        /// 机构ID
        /// </summary>
        public long OrgId { get; set; }

        /// <summary>
        /// 仓库ID
        /// </summary>
        public long StockId { get; set; }

        /// <summary>
        /// 获取条件查询语句
        /// </summary>
        /// <returns></returns>
        public string GetQuerySql()
        {
            var str = new StringBuilder("a.is_deleted = 0 and a.`status` = 1");
            if (OrgId > 0)
            {
                str.AppendFormat(" AND a.org_id = {0} and a.goods_code != '{0}'", OrgId);
            }
            if (!string.IsNullOrEmpty(GoodsName))
            {
                str.AppendFormat(" AND a.goods_name LIKE '%{0}%'", GoodsName);
            }
            if (!string.IsNullOrEmpty(CategoryName))
            {
                str.AppendFormat(" AND b.goods_category_name LIKE '%{0}%'", CategoryName);
            }
            if (!string.IsNullOrEmpty(RegistrationHolder))
            {
                str.AppendFormat(" AND a.registration_holder LIKE '%{0}%'", RegistrationHolder);
            }
            return str.ToString();
        }

        /// <summary>
        /// 数据较验
        /// </summary>
        /// <returns></returns>
        public override bool Validate()
        {
            if (ShopId <= 0)
            {
                return false;
            }
            return base.Validate();
        }
    }
}
