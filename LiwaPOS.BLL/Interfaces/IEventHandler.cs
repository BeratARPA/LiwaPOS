namespace LiwaPOS.BLL.Interfaces
{
    public interface IEventHandler
    {
        Task HandleEventAsync(string eventData,dynamic dataObject);
    }
}
