using System;
using System.Collections.Generic;
using System.Text;
using TTY.GMP.Entity.Common;

namespace TTY.GMP.Entity.Web.Goods.Request
{
    /// <summary>
    /// 获取商品销售记录
    /// </summary>
    public class GetSaleGoodsLogPageRequest : RequestPagingBase, IDataLimitRequest, IQuerySql
    {
        /// <summary>
        ///门店ID
        /// </summary>
        public long ShopId { get; set; }

        /// <summary>
        /// 门店名称
        /// </summary>
        public string ShopName { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string GoodsName { get; set; }

        /// <summary>
        /// 购买人
        /// </summary>
        public string BuyInfo { get; set; }

        /// <summary>
        /// 单号
        /// </summary>
        public string BillCode { get; set; }

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
            if (ShopId > 0)
            {
                str.AppendFormat(" AND d.shop_id = {0}", ShopId);
            }
            if (!string.IsNullOrEmpty(ShopName))
            {
                str.AppendFormat(" AND d.shop_name LIKE '%{0}%'", ShopName);
            }
            if (!string.IsNullOrEmpty(GoodsName))
            {
                str.AppendFormat(" AND e.goods_name LIKE '%{0}%'", GoodsName);
            }
            if (!string.IsNullOrEmpty(BuyInfo))
            {
                str.AppendFormat(" AND (c.member_name LIKE '%{0}%' or c.identification LIKE '%{0}%' OR c.member_tel LIKE '%{0}%')", BuyInfo);
            }
            if (!string.IsNullOrEmpty(BillCode))
            {
                str.AppendFormat(" AND a.bill_code LIKE '%{0}%'", BillCode);
            }
            if (StartTimeStamp > 0)
            {
                str.AppendFormat(" AND a.bill_date >= {0}", StartTimeStamp);
            }
            if (EndTimeStamp > 0)
            {
                str.AppendFormat(" AND a.bill_date <= {0}", EndTimeStamp);
            }
            if (ShopId <= 0)
            {
                if (!string.IsNullOrEmpty(LimitShops))
                {
                    str.AppendFormat(" AND d.shop_id IN ({0})", LimitShops);
                }
                return this.GetAreaLimitSql(str, "d.").ToString();
            }
            return str.ToString();
        }
    }
}
