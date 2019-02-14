using System;
using System.Collections.Generic;
using System.Text;

namespace TTY.GMP.Utility
{
    /// <summary>
    /// 日期时间扩展类
    /// </summary>
    public static class DataTimeExtensions
    {
        /// <summary>
        /// 计算时间戳
        /// </summary>
        /// <param name="dateTime">时间值</param>
        /// <returns>
        /// 返回时间戳
        /// </returns>
        public static long ToTimestamp(this DateTime dateTime)
        {
            TimeSpan timeSpan = dateTime.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(timeSpan.TotalMilliseconds);
        }

        /// <summary>
        /// 获取日期时间
        /// </summary>
        /// <param name="value">时间戳</param>
        /// <returns>
        /// 返回日期时间
        /// </returns>
        public static DateTime ToDateTimeFromTimeStamp(this long value)
        {
            if (value.Equals(0L)) return default(DateTime);
            return TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1, 0, 0, 0, 0)).AddMilliseconds(value);
        }

        /// <summary>
        /// 获取该周的开始时间
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime GetStartOfTheWeek(this DateTime dateTime)
        {
            var weeknow = Convert.ToInt32(dateTime.DayOfWeek);
            weeknow = (weeknow == 0 ? (7 - 1) : (weeknow - 1));
            var daydiff = (-1) * weeknow;
            var firstDay = dateTime.AddDays(daydiff).ToString("yyyy-MM-dd");
            return Convert.ToDateTime(firstDay);
        }
    }
}
