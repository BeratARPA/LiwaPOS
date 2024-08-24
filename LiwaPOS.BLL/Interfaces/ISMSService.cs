namespace LiwaPOS.BLL.Interfaces
{
    public interface ISmsService
    {
        Task SendSmsAsync<T>(T model);
    }
}
