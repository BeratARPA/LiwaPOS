using LiwaPOS.DAL.Context;
using LiwaPOS.DAL.Interfaces;
using LiwaPOS.Entities.Entities;

namespace LiwaPOS.DAL.Repositories
{
    public class AutomationCommandRepository : GenericRepository<AutomationCommand>, IAutomationCommandRepository
    {
        public AutomationCommandRepository(DataContext context) : base(context) { }
    }
}
