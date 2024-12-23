using LiwaPOS.Entities.Entities;
using System.Linq.Expressions;

namespace LiwaPOS.DAL.Interfaces
{
    public interface IPrinterRepository
    {
        Task<IEnumerable<Printer>> GetAllAsync(Expression<Func<Printer, bool>> filter = null);
        Task<IEnumerable<Printer>> GetAllAsNoTrackingAsync(Expression<Func<Printer, bool>> filter = null);
        Task<Printer> GetAsync(Expression<Func<Printer, bool>> filter = null);
        Task<Printer> GetAsNoTrackingAsync(Expression<Func<Printer, bool>> filter = null);
        Task<Printer> GetByIdAsync(int id);
        Task<Printer> GetByIdAsNoTrackingAsync(int id);
        Task AddAsync(Printer entity);
        Task UpdateAsync(Printer entity);
        Task DeleteAsync(int id);
        Task DeleteAllAsync(Expression<Func<Printer, bool>> filter = null, IEnumerable<Printer> entities = null);
    }
}
