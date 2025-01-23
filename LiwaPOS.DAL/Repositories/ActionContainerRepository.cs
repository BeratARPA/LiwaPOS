using LiwaPOS.DAL.Context;
using LiwaPOS.DAL.Interfaces;
using LiwaPOS.Entities.Entities;

namespace LiwaPOS.DAL.Repositories
{
    public class ActionContainerRepository : GenericRepository<ActionContainer>, IActionContainerRepository
    {
        public ActionContainerRepository(DataContext context) : base(context) { }
    }
}
