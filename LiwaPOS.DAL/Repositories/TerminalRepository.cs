using LiwaPOS.DAL.Context;
using LiwaPOS.DAL.Interfaces;
using LiwaPOS.Entities.Entities;

namespace LiwaPOS.DAL.Repositories
{
    public class TerminalRepository : GenericRepository<Terminal>, ITerminalRepository
    {
        public TerminalRepository(DataContext context) : base(context) { }
    }
}
