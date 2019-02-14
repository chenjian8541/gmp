using System;
using System.Collections.Generic;
using System.Text;

namespace TTY.GMP.Utility
{
    /// <summary>
    /// String帮助类
    /// </summary>
    public static class StringHelper
    {
        /// <summary>
        /// 截取字符串
        /// </summary>
        /// <param name="this"></param>
        /// <param name="strStart"></param>
        /// <param name="strEnd"></param>
        /// <returns></returns>
        public static string Substring(this string @this, string strStart, string strEnd)
        {
            var indexStart = @this.IndexOf(strStart) + strStart.Length;
            var indexEnd = @this.IndexOf(strEnd);
            return @this.Substring(indexStart, indexEnd - indexStart);
        }
    }
}
