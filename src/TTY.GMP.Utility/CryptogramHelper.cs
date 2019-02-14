using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace TTY.GMP.Utility
{
    /// <summary>
    /// 加解密助手
    /// </summary>
    public class CryptogramHelper
    {
        /// <summary>
        /// 加解密Key
        /// </summary>
        private static readonly string key;

        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="sourceStr"></param>
        /// <returns></returns>
        public static string Decrypt3DES(string sourceStr)
        {
            var provider = new TripleDESCryptoServiceProvider();
            provider.Key = new MD5CryptoServiceProvider().ComputeHash(Encoding.UTF8.GetBytes(key));
            provider.Mode = CipherMode.ECB;
            var transform = provider.CreateDecryptor();
            string str = "";
            try
            {
                var inputBuffer = Convert.FromBase64String(sourceStr);
                str = Encoding.UTF8.GetString(transform.TransformFinalBlock(inputBuffer, 0, inputBuffer.Length));
            }
            catch
            {
            }
            return str;
        }

        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="sourceStr"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Decrypt3DES(string sourceStr, string key)
        {
            var provider = new TripleDESCryptoServiceProvider();
            provider.Key = new MD5CryptoServiceProvider().ComputeHash(Encoding.UTF8.GetBytes(key));
            provider.Mode = CipherMode.ECB;
            var transform = provider.CreateDecryptor();
            string str = "";
            try
            {
                var inputBuffer = Convert.FromBase64String(sourceStr);
                str = Encoding.UTF8.GetString(transform.TransformFinalBlock(inputBuffer, 0, inputBuffer.Length));
            }
            catch
            {
            }
            return str;
        }

        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="sourceStr"></param>
        /// <param name="key"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string Decrypt3DES(string sourceStr, string key, Encoding encoding)
        {
            var provider = new TripleDESCryptoServiceProvider();
            provider.Key = new MD5CryptoServiceProvider().ComputeHash(encoding.GetBytes(key));
            provider.Mode = CipherMode.ECB;
            var transform = provider.CreateDecryptor();
            string str = "";
            try
            {
                var inputBuffer = Convert.FromBase64String(sourceStr);
                str = encoding.GetString(transform.TransformFinalBlock(inputBuffer, 0, inputBuffer.Length));
            }
            catch
            {
            }
            return str;
        }

        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="sourceStr"></param>
        /// <returns></returns>
        public static string Encrypt3DES(string sourceStr)
        {
            var provider = new TripleDESCryptoServiceProvider();
            provider.Key = new MD5CryptoServiceProvider().ComputeHash(Encoding.UTF8.GetBytes(key));
            provider.Mode = CipherMode.ECB;
            var transform = provider.CreateEncryptor();
            byte[] bytes = Encoding.UTF8.GetBytes(sourceStr);
            return Convert.ToBase64String(transform.TransformFinalBlock(bytes, 0, bytes.Length));
        }

        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="sourceStr"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Encrypt3DES(string sourceStr, string key)
        {
            var provider = new TripleDESCryptoServiceProvider();
            provider.Key = new MD5CryptoServiceProvider().ComputeHash(Encoding.UTF8.GetBytes(key));
            provider.Mode = CipherMode.ECB;
            var transform = provider.CreateEncryptor();
            var bytes = Encoding.UTF8.GetBytes(sourceStr);
            return Convert.ToBase64String(transform.TransformFinalBlock(bytes, 0, bytes.Length));
        }

        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="sourceStr"></param>
        /// <param name="key"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string Encrypt3DES(string sourceStr, string key, Encoding encoding)
        {
            var provider = new TripleDESCryptoServiceProvider();
            provider.Key = new MD5CryptoServiceProvider().ComputeHash(encoding.GetBytes(key));
            provider.Mode = CipherMode.ECB;
            var transform = provider.CreateEncryptor();
            var bytes = encoding.GetBytes(sourceStr);
            return Convert.ToBase64String(transform.TransformFinalBlock(bytes, 0, bytes.Length));
        }

        /// <summary>
        /// Base64解密
        /// </summary>
        /// <param name="sourceStr"></param>
        /// <returns></returns>
        public static string DecryptBase64(string sourceStr)
        {
            var bytes = Convert.FromBase64String(sourceStr);
            return Encoding.UTF8.GetString(bytes);
        }

        /// <summary>
        /// Base64解密
        /// </summary>
        /// <param name="sourceStr"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string DecryptBase64(string sourceStr, Encoding encoding)
        {
            var bytes = Convert.FromBase64String(sourceStr);
            return encoding.GetString(bytes);
        }

        /// <summary>
        /// Base64加密
        /// </summary>
        /// <param name="sourceStr"></param>
        /// <returns></returns>
        public static string EncryptBase64(string sourceStr)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(sourceStr));
        }

        /// <summary>
        /// Base64加密
        /// </summary>
        /// <param name="sourceStr"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string EncryptBase64(string sourceStr, Encoding encoding)
        {
            return Convert.ToBase64String(encoding.GetBytes(sourceStr));
        }

        /// <summary>
        /// 静态构造函数
        /// 初始化默认key
        /// </summary>
        static CryptogramHelper()
        {
            key = "lQa9_&skzly%!9fs2@*UNA($ck_^:)'aI9e.^2Lbx9,5lf!j+~Hz@^hakuJ^crOb";
        }
    }
}
