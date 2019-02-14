using System;
using System.Collections.Generic;
using System.Text;
using TTY.GMP.Entity.Config;
using TTY.GMP.IOC;
using System.Linq;
using System.Security.Cryptography;
using System.IO;
using TTY.GMP.LOG;

namespace TTY.GMP.Utility
{
    /// <summary>
    /// 平台帮助类
    /// </summary>
    public class PlatformHelper
    {
        /// <summary>
        /// 平台配置
        /// </summary>
        private static PlatformConfig _platformConfig;

        /// <summary>
        /// 请求平台接口
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="api"></param>
        /// <param name="formData"></param>
        /// <returns></returns>
        public static T RequestApi<T>(string api, Dictionary<string, string> formData)
        {
            var url = string.Concat(_platformConfig.Url, api);
            formData = AddSignParam(formData);
            try
            {
                return HttpHelper.HttpPost<T>(url, formData);
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("调用平台接口出错，地址{0}", url), ex, typeof(PlatformHelper));
                return default(T);
            }
        }

        /// <summary>
        /// 添加安全签名
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        private static Dictionary<string, string> AddSignParam(Dictionary<string, string> param)
        {
            param.Add("timestamp", DateTime.Now.ToTimestamp().ToString());
            param.Add("apiKey", _platformConfig.ApiKey);
            param = param.OrderBy(c => c.Key).ToDictionary(p => p.Key, o => o.Value);
            var paramStr = "";
            foreach (var p in param)
            {
                paramStr += p.Value + "";
            }
            var sign = HmacSha1Sign(paramStr, param["apiKey"].ToString()).Replace("+", param["timestamp"].ToString()).Replace("/", param["timestamp"].ToString()).Replace("=", "");
            param.Add("sign", sign);
            return param;
        }

        /// <summary>
        ///  签名
        /// </summary>
        /// <param name="text"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private static string HmacSha1Sign(string text, string key)
        {
            var encode = Encoding.GetEncoding("UTF-8");
            var byteData = encode.GetBytes(text);
            var byteKey = encode.GetBytes(key);
            var hmac = new HMACSHA1(byteKey);
            var cs = new CryptoStream(Stream.Null, hmac, CryptoStreamMode.Write);
            cs.Write(byteData, 0, byteData.Length);
            cs.Close();
            return Convert.ToBase64String(hmac.Hash);
        }

        /// <summary>
        /// 静态构造函数
        /// 初始化平台配置信息
        /// </summary>
        static PlatformHelper()
        {
            _platformConfig = CustomServiceLocator.GetInstance<IAppConfigurtaionServices>().AppSettings.PlatformConfig;
        }
    }

    /// <summary>
    /// 平台API地址
    /// </summary>
    public struct PlatformApiConfig
    {
        /// <summary>
        /// 获取用户信息
        /// </summary>
        public const string GetUserInfo = "org/user/info";

        /// <summary>
        /// 批量获取负责人信息
        /// </summary>
        public const string GetFullNames = "admin/org/legalPerson/fullNames";

        /// <summary>
        /// 调用O端的接口，获取组织信息
        /// </summary>
        public const string GetOrgInfo = "admin/organization/getInfo";

        /// <summary>
        /// 获取机构推荐人信息
        /// </summary>
        public const string GetOrgRecommend = "admin/recommend/detail";
    }
}
