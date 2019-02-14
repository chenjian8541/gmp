using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace TTY.GMP.Http
{
    public interface IHttpClient
    {
        Task<string> GetStringAsync(string uri, string authorizationToken = null, string authorizationMethod = "Bearer");

        Task<TU> PostAsync<T, TU>(string uri, T item, string authorizationToken = null, string requestId = null, string authorizationMethod = "Bearer");

        Task<T> PostAsync<T>(string url, Dictionary<string, string> formData, string authorizationToken = null, string requestId = null, string authorizationMethod = "Bearer");

        Task<HttpResponseMessage> DeleteAsync(string uri, string authorizationToken = null, string requestId = null, string authorizationMethod = "Bearer");

        Task<TU> PutAsync<T, TU>(string uri, T item, string authorizationToken = null, string requestId = null, string authorizationMethod = "Bearer");

        T Post<T>(string url, Dictionary<string, string> formData = null, int timeOut = 10000);
    }
}
