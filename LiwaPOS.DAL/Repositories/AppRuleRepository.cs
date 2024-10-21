using LiwaPOS.DAL.Context;
using LiwaPOS.DAL.Interfaces;
using LiwaPOS.Entities.Entities;

namespace LiwaPOS.DAL.Repositories
{
    public class AppRuleRepository : GenericRepository<AppRule>, IAppRuleRepository
    {
        public AppRuleRepository(DataContext context) : base(context) { }
    }
}
