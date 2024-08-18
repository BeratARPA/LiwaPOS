namespace LiwaPOS.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IAppRuleRepository AppRules { get; }
        IAppActionRepository AppActions { get; }
        IUserRepository Users { get; }

        void Commit();
    }
}
