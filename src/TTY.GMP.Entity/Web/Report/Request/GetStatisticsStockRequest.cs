﻿using System;
using System.Collections.Generic;
using System.Text;
using TTY.GMP.Entity.Common;
using TTY.GMP.Entity.Enum;

namespace TTY.GMP.Entity.Web.Report.Request
{
    /// <summary>
    /// 区域农药库存统计
    /// </summary>
    public class GetStatisticsStockRequest : RequestBase
    {
        /// <summary>
        /// 地区
        /// </summary>
        public long AreaId { get; set; }

        /// <summary>
        /// 地区级别
        /// </summary>
        public string Level { get; set; }

        /// <summary>
        /// 统计类型
        ///  <see cref="StatisticsTypeEnum"/>
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 数据较验
        /// </summary>
        /// <returns></returns>
        public override bool Validate()
        {
            if (Level == AreaLevelEnum.Street)
            {
                return false;
            }
            if (AreaId > 0 && string.IsNullOrEmpty(Level))
            {
                return false;
            }
            return base.Validate();
        }
    }
}
