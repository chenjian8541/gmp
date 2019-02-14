using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace TTY.GMP.Utility
{
    /// <summary>
    /// 类型转换
    /// </summary>
    public static class ConvertHelper
    {
        /// <summary>
        /// 将string转换成int32
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static int ToInt(this object @this)
        {
            return Convert.ToInt32(@this);
        }

        /// <summary>
        /// 将string转换成long
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static long ToLong(this object @this)
        {
            return Convert.ToInt64(@this);
        }

        /// <summary>
        /// 将string转换成int?
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static int? ToIntNullable(this string @this)
        {
            if (string.IsNullOrEmpty(@this))
            {
                return null;
            }
            return Convert.ToInt32(@this);
        }

        /// <summary>
        /// 转换成BigInteger类型
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static BigInteger ToBigInteger(this string @this)
        {
            if (string.IsNullOrEmpty(@this))
            {
                return 0;
            }
            if (BigInteger.TryParse(@this, out BigInteger value))
            {
                return value;
            }
            return 0;
        }
    }
}
