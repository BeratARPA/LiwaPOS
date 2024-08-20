namespace LiwaPOS.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IAppRuleRepository AppRules { get; }
        IAppActionRepository AppActions { get; }
        IRuleActionMapRepository RuleActionMaps { get; }
        IUserRepository Users { get; }

        void Commit();
    }
}
