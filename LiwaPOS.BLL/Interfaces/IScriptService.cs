using LiwaPOS.Entities.Entities;
using LiwaPOS.Shared.Models.Entities;
using System.Linq.Expressions;

namespace LiwaPOS.BLL.Interfaces
{
    public interface IScriptService
    {
        Task<IEnumerable<ScriptDTO>> GetAllScriptsAsync(Expression<Func<Script, bool>> filter = null);
        Task<ScriptDTO> GetScriptAsync(Expression<Func<Script, bool>> filter = null);
        Task<ScriptDTO> GetScriptByIdAsync(int id);
        Task AddScriptAsync(ScriptDTO scriptDto);
        Task UpdateScriptAsync(ScriptDTO scriptDto);
        Task DeleteScriptAsync(int id);
    }
}
