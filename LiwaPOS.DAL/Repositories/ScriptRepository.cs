using LiwaPOS.DAL.Context;
using LiwaPOS.DAL.Interfaces;
using LiwaPOS.Entities.Entities;

namespace LiwaPOS.DAL.Repositories
{
    public class ScriptRepository : GenericRepository<Script>, IScriptRepository
    {
        public ScriptRepository(DataContext context) : base(context) { }
    }
}
