namespace LiwaPOS.BLL.Interfaces
{
    public interface IAction
    {
        Task<object> Execute(string properties);
    }
}
