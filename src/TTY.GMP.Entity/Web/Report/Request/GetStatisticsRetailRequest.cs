using System;
using TTY.GMP.Entity.Common;
using TTY.GMP.Entity.Enum;

namespace TTY.GMP.Entity.Web.Report.Request
{
    /// <summary>
    /// 获取区域农药销售统计
    /// </summary>
    public class GetStatisticsRetailRequest : RequestBase
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
        /// 开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }

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
