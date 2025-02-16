using LiwaPOS.Entities.Entities;
using System.Linq.Expressions;

namespace LiwaPOS.DAL.Interfaces
{
    public interface IPrinterTemplateRepository
    {
        Task<IEnumerable<PrinterTemplate>> GetAllAsync(Expression<Func<PrinterTemplate, bool>> filter = null);
        Task<IEnumerable<PrinterTemplate>> GetAllAsNoTrackingAsync(Expression<Func<PrinterTemplate, bool>> filter = null);
        Task<PrinterTemplate> GetAsync(Expression<Func<PrinterTemplate, bool>> filter = null);
        Task<PrinterTemplate> GetAsNoTrackingAsync(Expression<Func<PrinterTemplate, bool>> filter = null);
        Task<PrinterTemplate> GetByIdAsync(int id);
        Task<PrinterTemplate> GetByIdAsNoTrackingAsync(int id);
        Task AddAsync(PrinterTemplate entity);
        Task UpdateAsync(PrinterTemplate entity);
        Task DeleteAsync(int id);
        Task DeleteAllAsync(Expression<Func<PrinterTemplate, bool>> filter = null, IEnumerable<PrinterTemplate> entities = null);
    }
}
