namespace LiwaPOS.BLL.Interfaces
{
    public interface IHttpService
    {
        Task<TResponse> SendAsync<TRequest, TResponse>(HttpMethod method, string url, TRequest data = default, IDictionary<string, string> headers = null, string accept = "application/json");
    }
}
