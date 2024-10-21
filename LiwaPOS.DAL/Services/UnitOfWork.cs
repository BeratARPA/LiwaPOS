using LiwaPOS.DAL.Context;
using LiwaPOS.DAL.Interfaces;
using LiwaPOS.DAL.Repositories;

namespace LiwaPOS.DAL.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;

        private IAppRuleRepository _appRules;
        private IAppActionRepository _appActions;
        private IRuleActionMapRepository _ruleActionMaps;
        private IUserRepository _users;
        private IScriptRepository _scripts;

        public UnitOfWork(DataContext context)
        {
            _context = context;
        }

        public IAppRuleRepository AppRules => _appRules ??= new AppRuleRepository(_context);
        public IAppActionRepository AppActions => _appActions ??= new AppActionRepository(_context);
        public IRuleActionMapRepository RuleActionMaps => _ruleActionMaps ??= new RuleActionMapRepository(_context);
        public IUserRepository Users => _users ??= new UserRepository(_context);
        public IScriptRepository Scripts => _scripts ??= new ScriptRepository(_context);

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task BeginTransactionAsync()
        {
            await _context.Database.BeginTransactionAsync();
        }

        public async Task ExecuteInTransactionAsync(Func<Task> action)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                await action();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw; // Hata fırlat
            }
        }

        public async Task CommitTransactionAsync()
        {
            await _context.Database.CommitTransactionAsync();
        }

        public async Task RollbackTransactionAsync()
        {
            await _context.Database.RollbackTransactionAsync();
        }

        void IDisposable.Dispose()
        {
            _context?.Dispose();
        }

        public async ValueTask DisposeAsync()
        {
            if (_context != null)
            {
                await _context.DisposeAsync();
            }
        }
    }
}
