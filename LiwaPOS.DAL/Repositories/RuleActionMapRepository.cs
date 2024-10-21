using LiwaPOS.DAL.Context;
using LiwaPOS.DAL.Interfaces;
using LiwaPOS.Entities.Entities;

namespace LiwaPOS.DAL.Repositories
{
    public class RuleActionMapRepository : GenericRepository<RuleActionMap>, IRuleActionMapRepository
    {
        public RuleActionMapRepository(DataContext context) : base(context) { }
    }
}
