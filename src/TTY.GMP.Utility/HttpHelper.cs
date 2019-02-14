using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Text;
using TTY.GMP.LOG;

namespace TTY.GMP.Utility
{
    /// <summary>
    /// HTTP请求帮助类
    /// </summary>
    public class HttpHelper
    {
        /// <summary>
        /// POST 同步
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postStream"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static T HttpPost<T>(string url, Dictionary<string, string> formData = null, int timeOut = 10000)
        {
            var bufferBytes = Encoding.UTF8.GetBytes(GetQueryString(formData));
            var request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "POST";
            request.ContentLength = bufferBytes.Length;
            request.ContentType = "application/x-www-form-urlencoded";
            request.KeepAlive = true;
            request.Proxy = null;
            request.ServicePoint.Expect100Continue = false;
            request.ServicePoint.ConnectionLimit = int.MaxValue;
            request.ServicePoint.UseNagleAlgorithm = false;
            request.AllowWriteStreamBuffering = false;
            request.AuthenticationLevel = AuthenticationLevel.None;
            request.AutomaticDecompression = DecompressionMethods.None;
            if (bufferBytes.Length > 0)
            {
                using (var requestStream = request.GetRequestStream())
                {
                    requestStream.Write(bufferBytes, 0, bufferBytes.Length);
                }
            }
            var response = request.GetResponse() as HttpWebResponse;
            using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
            {
                var result = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<T>(result);
            }
        }

        /// <summary>
        /// 组装QueryString的方法
        /// 参数之间用&连接，首位没有符号，如：a=1&b=2&c=3
        /// </summary>
        /// <param name="formData"></param>
        /// <returns></returns>
        private static string GetQueryString(Dictionary<string, string> formData)
        {
            if (formData == null || formData.Count == 0)
            {
                return string.Empty;
            }
            StringBuilder sb = new StringBuilder();
            var i = 0;
            foreach (var kv in formData)
            {
                i++;
                sb.AppendFormat("{0}={1}", kv.Key, kv.Value);
                if (i < formData.Count)
                {
                    sb.Append("&");
                }
            }
            return sb.ToString();
        }
    }
}
