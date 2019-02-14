using System;
using System.Collections.Generic;
using System.Text;
using TTY.GMP.Entity.View;

namespace TTY.GMP.Entity.Web.Shop.Response
{
    /// <summary>
    /// 门店统计信息
    /// </summary>
    public class GetShopStatisticsView
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="data"></param>
        /// <param name="count"></param>
        public GetShopStatisticsView(List<ShopStatisticsView> data, int count)
        {
            this.Data = data;
            this.Count = count;
        }

        /// <summary>
        /// 统计数据
        /// </summary>
        public List<ShopStatisticsView> Data { get; set; }

        /// <summary>
        /// 总数量
        /// </summary>
        public int Count { get; set; }
    }
}
