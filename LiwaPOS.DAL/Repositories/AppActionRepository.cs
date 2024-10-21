using LiwaPOS.DAL.Context;
using LiwaPOS.DAL.Interfaces;
using LiwaPOS.Entities.Entities;

namespace LiwaPOS.DAL.Repositories
{
    public class AppActionRepository : GenericRepository<AppAction>, IAppActionRepository
    {
        public AppActionRepository(DataContext context) : base(context) { }
    }
}
