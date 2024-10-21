namespace LiwaPOS.DAL.Interfaces
{
    public interface IUnitOfWork : IAsyncDisposable, IDisposable
    {
        IAppRuleRepository AppRules { get; }
        IAppActionRepository AppActions { get; }
        IRuleActionMapRepository RuleActionMaps { get; }
        IUserRepository Users { get; }
        IScriptRepository Scripts { get; }

        Task CommitAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
        Task ExecuteInTransactionAsync(Func<Task> action);
    }
}
