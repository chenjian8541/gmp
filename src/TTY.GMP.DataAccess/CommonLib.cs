using System;
using System.Collections.Generic;
using System.Text;
using TTY.GMP.Entity.Enum;

namespace TTY.GMP.DataAccess
{
    /// <summary>
    /// 公共操作
    /// </summary>
    public class CommonLib
    {
        /// <summary>
        /// 获取地区级别对应的数据库字段
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        internal static string GetAreaLevelFieldName(string level)
        {
            switch (level)
            {
                case AreaLevelEnum.Province:
                    return "Province";
                case AreaLevelEnum.City:
                    return "City";
                case AreaLevelEnum.District:
                    return "District";
                case AreaLevelEnum.Street:
                    return "Street";
                default:
                    return "Province";
            }
        }
    }
}
