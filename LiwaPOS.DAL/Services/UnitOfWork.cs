using LiwaPOS.DAL.Context;
using LiwaPOS.DAL.Interfaces;
using LiwaPOS.DAL.Repositories;

namespace LiwaPOS.DAL.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;

        public UnitOfWork(DataContext context)
        {
            _context = context;
        }

        public IAppRuleRepository AppRules => new AppRuleRepository(_context);
        public IAppActionRepository AppActions => new AppActionRepository(_context);
        public IRuleActionMapRepository RuleActionMaps => new RuleActionMapRepository(_context);
        public IUserRepository Users => new UserRepository(_context);

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }       
    }
}
