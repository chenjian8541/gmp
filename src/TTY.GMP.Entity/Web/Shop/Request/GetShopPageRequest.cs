using System;
using System.Collections.Generic;
using System.Text;
using TTY.GMP.Entity.Common;

namespace TTY.GMP.Entity.Web.Shop.Request
{
    /// <summary>
    /// 通过地区获取店面信息
    /// </summary>
    public class GetShopPageRequest : RequestPagingBase, IDataLimitRequest, IQuerySql
    {
        /// <summary>
        /// 省
        /// </summary>
        public long Province { get; set; }

        /// <summary>
        /// 市
        /// </summary>
        public long City { get; set; }

        /// <summary>
        /// 区
        /// </summary>
        public long District { get; set; }

        /// <summary>
        /// 关键字(门店名称、联系号码)
        /// </summary>
        public string KeyWord { get; set; }

        /// <summary>
        /// 门店名称
        /// </summary>
        public string ShopName { get; set; }

        /// <summary>
        /// 联系号码
        /// </summary>
        public string ShopTelphone { get; set; }

        /// <summary>
        /// 场景类型
        /// <see cref="TTY.GMP.Entity.Enum.GetShopPageSceneTypeEnum"/>
        /// </summary>
        public int SceneType { get; set; }

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
            var str = new StringBuilder("`status` = 1");
            if (Province > 0)
            {
                str.AppendFormat(" AND province = {0}", Province);
            }
            if (City > 0)
            {
                str.AppendFormat(" AND city = {0}", City);
            }
            if (District > 0)
            {
                str.AppendFormat(" AND district = {0}", District);
            }
            if (!string.IsNullOrEmpty(KeyWord))
            {
                str.AppendFormat(" AND (shop_name LIKE '%{0}%' OR shop_telphone LIKE '%{0}%')", KeyWord);
            }
            if (!string.IsNullOrEmpty(ShopName))
            {
                str.AppendFormat(" AND shop_name LIKE '%{0}%'", ShopName);
            }
            if (!string.IsNullOrEmpty(ShopTelphone))
            {
                str.AppendFormat(" AND shop_telphone LIKE '%{0}%'", ShopTelphone);
            }
            if (!string.IsNullOrEmpty(LimitShops))
            {
                str.AppendFormat(" AND shop_id IN ({0})", LimitShops);
            }
            return this.GetAreaLimitSql(str).ToString();
        }

        /// <summary>
        /// 数据较验
        /// </summary>
        /// <returns></returns>
        public override bool Validate()
        {
            return true;
        }
    }
}
