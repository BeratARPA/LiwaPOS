using LiwaPOS.DAL.Context;
using LiwaPOS.DAL.Interfaces;
using LiwaPOS.Entities.Entities;

namespace LiwaPOS.DAL.Repositories
{
    public class ProgramSettingValueRepository : GenericRepository<ProgramSettingValue>, IProgramSettingValueRepository
    {
        public ProgramSettingValueRepository(DataContext context) : base(context) { }
    }
}
