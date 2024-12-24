using LiwaPOS.BLL.Interfaces;
using LiwaPOS.Shared.Helpers;
using System.Net.Http.Headers;
using System.Text;

namespace LiwaPOS.BLL.Services
{
    public class HttpService : IHttpService
    {
        private readonly HttpClient _httpClient = new HttpClient();
        public HttpService()
        {
            _httpClient.Timeout = TimeSpan.FromMinutes(1);
        }

        public async Task<TResponse> SendAsync<TRequest, TResponse>(HttpMethod method, string url, TRequest data = default, IDictionary<string, string> headers = null, string accept = "json")
        {
            string mediaType = ConvertToAccept(accept);
            var request = new HttpRequestMessage(method, url);
            AddHeaders(request, headers);

            // Accept header'ı ekliyoruz (Varsayılan: application/json)
            request.Headers.Accept.Clear();
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType));

            // POST ve PUT isteklerinde body eklenir
            if (method == HttpMethod.Post || method == HttpMethod.Put)
            {
                if (data != null)
                {
                    //var json = JsonHelper.Serialize(data);
                    request.Content = new StringContent(data.ToString(), Encoding.UTF8, "application/json");
                }
            }

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            if (typeof(TResponse) == typeof(string))
                return (TResponse)(object)content;

            // Gelen cevabı content type'a göre işle
            if (response.Content.Headers.ContentType.MediaType == mediaType)
            {
                return JsonHelper.Deserialize<TResponse>(content);
            }
            else if (response.Content.Headers.ContentType.MediaType == mediaType)
            {
                return XmlHelper.Deserialize<TResponse>(content);
            }
            else
            {
                throw new NotSupportedException("Desteklenmeyen içerik türü: " + response.Content.Headers.ContentType.MediaType);
            }
        }

        private string ConvertToAccept(string accept)
        {
            if (accept.ToLower() == "json")
            {
                return "application/json";
            }
            else if (accept.ToLower() == "xml")
            {
                return "application/xml";
            }
            else
            {
                return accept;
            }
        }
       
        private void AddHeaders(HttpRequestMessage request, IDictionary<string, string> headers)
        {
            if (headers != null)
            {
                foreach (var header in headers)
                {
                    request.Headers.Add(header.Key, header.Value);
                }
            }
        }
    }
}
