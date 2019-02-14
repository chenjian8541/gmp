﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TTY.GMP.Entity.View
{
    /// <summary>
    /// 商品库存信息
    /// </summary>
    public class DbStatisticsStockView
    {
        /// <summary>
        /// 地区ID
        /// </summary>
        public long AreaId { get; set; }

        /// <summary>
        /// 总重量
        /// </summary>
        public decimal TotalWeight { get; set; }

        /// <summary>
        /// 重量折百比
        /// </summary>
        public decimal TotalContentsWeight { get; set; }

        /// <summary>
        /// 类别ID
        /// </summary>
        public long GoodsCategoryId { get; set; }

        /// <summary>
        /// 类别名称
        /// </summary>
        public string GoodsCategoryName { get; set; }
    }
}