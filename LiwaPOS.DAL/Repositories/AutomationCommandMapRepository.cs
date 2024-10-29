using LiwaPOS.DAL.Context;
using LiwaPOS.DAL.Interfaces;
using LiwaPOS.Entities.Entities;

namespace LiwaPOS.DAL.Repositories
{
    public class AutomationCommandMapRepository:GenericRepository<AutomationCommandMap>, IAutomationCommandMapRepository
    {
        public AutomationCommandMapRepository(DataContext context) : base(context) { }
    }
}
