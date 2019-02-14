using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;
using TTY.GMP.LOG;

namespace TTY.GMP.Http
{
    public class StandardHttpClient : IHttpClient
    {
        private readonly HttpClient _client;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public StandardHttpClient(IHttpContextAccessor httpContextAccessor)
        {
            _client = new HttpClient();
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<string> GetStringAsync(string uri, string authorizationToken = null, string authorizationMethod = "Bearer")
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            SetAuthorizationHeader(requestMessage);

            if (authorizationToken != null)
            {
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue(authorizationMethod, authorizationToken);
            }

            var response = await _client.SendAsync(requestMessage);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            return await response.Content.ReadAsStringAsync();
        }

        private async Task<TU> DoPostPutAsync<T, TU>(HttpMethod method, string uri, T item, string authorizationToken = null, string requestId = null, string authorizationMethod = "Bearer")
        {
            if (method != HttpMethod.Post && method != HttpMethod.Put)
            {
                throw new ArgumentException("Value must be either post or put.", nameof(method));
            }

            // a new StringContent must be created for each retry
            // as it is disposed after each call

            var requestMessage = new HttpRequestMessage(method, uri);

            SetAuthorizationHeader(requestMessage);

            if (item != null)
            {
                requestMessage.Content = new StringContent(JsonConvert.SerializeObject(item), System.Text.Encoding.UTF8,
                    "application/json");
            }

            if (authorizationToken != null)
            {
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue(authorizationMethod, authorizationToken);
            }

            if (requestId != null)
            {
                requestMessage.Headers.Add("x-requestid", requestId);
            }

            var response = await _client.SendAsync(requestMessage);

            // raise exception if HttpResponseCode 500
            // needed for circuit breaker to track fails

            if (response.StatusCode == HttpStatusCode.InternalServerError)
            {
                throw new HttpRequestException();
            }

            var result = await response.Content.ReadAsStringAsync();
            try
            {
                return JsonConvert.DeserializeObject<TU>(result);
            }
            catch (Exception ex)
            {
                Log.Error($"参数:{result}", ex, this.GetType());
                return default(TU);
            }
        }

        public async Task<TU> PostAsync<T, TU>(string uri, T item, string authorizationToken = null, string requestId = null, string authorizationMethod = "Bearer")
        {
            return await DoPostPutAsync<T, TU>(HttpMethod.Post, uri, item, authorizationToken, requestId, authorizationMethod);
        }

        public async Task<T> PostAsync<T>(string url, Dictionary<string, string> formData, string authorizationToken = null,
            string requestId = null, string authorizationMethod = "Bearer")
        {
            var newUrl = $"{url}?{GetQueryString(formData)}";
            return await PostAsync<object, T>(newUrl, null, authorizationToken, requestId, authorizationMethod);
        }


        public async Task<TU> PutAsync<T, TU>(string uri, T item, string authorizationToken = null, string requestId = null, string authorizationMethod = "Bearer")
        {
            return await DoPostPutAsync<T, TU>(HttpMethod.Put, uri, item, authorizationToken, requestId, authorizationMethod);
        }
        public async Task<HttpResponseMessage> DeleteAsync(string uri, string authorizationToken = null, string requestId = null, string authorizationMethod = "Bearer")
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            SetAuthorizationHeader(requestMessage);

            if (authorizationToken != null)
            {
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue(authorizationMethod, authorizationToken);
            }

            if (requestId != null)
            {
                requestMessage.Headers.Add("x-requestid", requestId);
            }

            return await _client.SendAsync(requestMessage);
        }

        private void SetAuthorizationHeader(HttpRequestMessage requestMessage)
        {
            if (_httpContextAccessor.HttpContext == null)
            {
                return;
            }
            var authorizationHeader = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
            if (!string.IsNullOrEmpty(authorizationHeader))
            {
                requestMessage.Headers.Add("Authorization", new List<string>() { authorizationHeader });
            }
        }

        /// <summary>
        /// POST 同步
        /// </summary>
        /// <param name="url"></param>
        /// <param name="formData"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public T Post<T>(string url, Dictionary<string, string> formData = null, int timeOut = 10000)
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
                try
                {
                    return JsonConvert.DeserializeObject<T>(result);
                }
                catch (Exception ex)
                {
                    Log.Error($"参数:{result}", ex, this.GetType());
                    return default(T);
                }
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