namespace LiwaPOS.DAL.Interfaces
{
    public interface IUnitOfWork : IAsyncDisposable, IDisposable
    {
        IAppRuleRepository AppRules { get; }
        IAppActionRepository AppActions { get; }
        IAutomationCommandRepository AutomationCommands { get; }
        IAutomationCommandMapRepository AutomationCommandMaps { get; }
        IRuleActionMapRepository RuleActionMaps { get; }
        IScriptRepository Scripts { get; }
        IUserRepository Users { get; }

        Task CommitAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
        Task ExecuteInTransactionAsync(Func<Task> action);
    }
}
