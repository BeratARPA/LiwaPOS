namespace LiwaPOS.BLL.Interfaces
{
    public interface INavigatorService
    {
        void Navigate(string viewName, object? parameter = null);
        void GoBack();
        void GoForward();
    }
}
