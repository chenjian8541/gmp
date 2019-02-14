using System;
using System.Collections.Generic;
using System.Text;
using TTY.GMP.Entity.Common;

namespace TTY.GMP.Entity.Web.Goods.Request
{
    /// <summary>
    /// 分页获取采购记录
    /// </summary>
    public class ReceiptInLogPageRequest : RequestPagingBase, IDataLimitRequest, IQuerySql
    {
        /// <summary>
        /// 门店名称
        /// </summary>
        public string ShopName { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string GoodsName { get; set; }

        /// <summary>
        /// 供应商名称
        /// </summary>
        public string SupplierName { get; set; }

        /// <summary>
        /// 单据编号
        /// </summary>
        public string InCode { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 开始时间戳
        /// </summary>
        public long StartTimeStamp { get; set; }

        /// <summary>
        /// 结束时间戳
        /// </summary>
        public long EndTimeStamp { get; set; }

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
            var str = new StringBuilder("1=1");
            if (!string.IsNullOrEmpty(ShopName))
            {
                str.AppendFormat(" AND h.shop_name LIKE '%{0}%'", ShopName);
            }
            if (!string.IsNullOrEmpty(GoodsName))
            {
                str.AppendFormat(" AND b.goods_name LIKE '%{0}%'", GoodsName);
            }
            if (!string.IsNullOrEmpty(SupplierName))
            {
                str.AppendFormat(" AND g.supplier_name LIKE '%{0}%'", SupplierName);
            }
            if (!string.IsNullOrEmpty(InCode))
            {
                str.AppendFormat(" AND f.in_code LIKE '%{0}%'", InCode);
            }
            if (StartTimeStamp > 0)
            {
                str.AppendFormat(" AND f.in_date >= {0}", StartTimeStamp);
            }
            if (EndTimeStamp > 0)
            {
                str.AppendFormat(" AND f.in_date <= {0}", EndTimeStamp);
            }
            if (!string.IsNullOrEmpty(LimitShops))
            {
                str.AppendFormat(" AND f.user_shop_id IN ({0})", LimitShops);
            }
            return this.GetAreaLimitSql(str, "h.").ToString();
        }
    }
}
