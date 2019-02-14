using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TTY.GMP.Utility;

namespace TTY.GMP.WebApi.Extensions
{
    /// <summary>
    /// String扩展
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// url加密
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static string UrlEncode(this object @this)
        {
            var str = @this.ToString().Trim();
            if (string.IsNullOrWhiteSpace(str))
            {
                return string.Empty;
            }
            return HttpUtility.UrlEncode(CryptogramHelper.Encrypt3DES(str));
        }

        /// <summary>
        /// url解密
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static string UrlDecode(this string @this)
        {
            if (string.IsNullOrWhiteSpace(@this))
            {
                return string.Empty;
            }
            return CryptogramHelper.Decrypt3DES(HttpUtility.UrlDecode(@this).Replace(" ", "+"));
        }

        /// <summary>
        /// url解密
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <returns></returns>
        public static T UrlDecode<T>(this string @this)
        {
            if (string.IsNullOrWhiteSpace(@this))
            {
                return default(T);
            }
            var decodeStr = CryptogramHelper.Decrypt3DES(HttpUtility.UrlDecode(@this).Replace(" ", "+"));
            if (string.IsNullOrEmpty(decodeStr))
            {
                return default(T);
            }
            try
            {
                return (T)Convert.ChangeType(decodeStr, typeof(T));
            }
            catch (Exception)
            {
                return default(T);
            }
        }
    }
}
