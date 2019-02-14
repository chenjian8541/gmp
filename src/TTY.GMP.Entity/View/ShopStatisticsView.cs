using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TTY.GMP.Entity.View
{
    /// <summary>
    /// 门店统计
    /// </summary>
    public class ShopStatisticsView
    {
        /// <summary>
        /// 省级ID
        /// </summary>
        [Column("province")]
        public long Province { get; set; }

        /// <summary>
        /// 省级编码
        /// </summary>
        [Column("province_number")]
        public string ProvinceNumber { get; set; }

        /// <summary>
        /// 省级名称
        /// </summary>
        [Column("province_name")]
        public string ProvinceName { get; set; }

        /// <summary>
        /// 市级ID
        /// </summary>
        [Column("city")]
        public long City { get; set; }

        /// <summary>
        /// 市级编码
        /// </summary>
        [Column("city_number")]
        public string CityNumber { get; set; }

        /// <summary>
        /// 市级名称
        /// </summary>
        [Column("city_name")]
        public string CityName { get; set; }

        /// <summary>
        /// 门店个数
        /// </summary>
        [Column("shop_count")]
        public int ShopCount { get; set; }

        /// <summary>
        /// 门店占比
        /// </summary>
        [Column("shop_ratio")]
        public double ShopRatio { get; set; }
    }
}
