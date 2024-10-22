using LiwaPOS.BLL.Interfaces;
using LiwaPOS.Shared.Helpers;
using LiwaPOS.Shared.Models;

namespace LiwaPOS.BLL.Actions
{
    public class SendHttpRequestAction : IAction
    {
        private readonly IHttpService _httpService;

        public SendHttpRequestAction(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task Execute(string properties)
        {
            var sendHttpRequestProperties = JsonHelper.Deserialize<SendHttpRequestDTO>(properties);
            if (sendHttpRequestProperties == null)
                return;

            HttpMethod httpMethod = HttpMethod.Get;
            switch (sendHttpRequestProperties.Method.ToUpper())
            {
                case "GET":
                    httpMethod = HttpMethod.Get;
                    break;
                case "POST":
                    httpMethod = HttpMethod.Post;
                    break;
                case "PUT":
                    httpMethod = HttpMethod.Put;
                    break;
                case "DELETE":
                    httpMethod = HttpMethod.Delete;
                    break;
                default:
                    break;
            }

            await _httpService.SendAsync<string, string>(httpMethod, sendHttpRequestProperties.Url, sendHttpRequestProperties.Data, null, sendHttpRequestProperties.Accept);
        }
    }
}
