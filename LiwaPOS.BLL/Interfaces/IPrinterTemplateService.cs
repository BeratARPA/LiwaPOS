using LiwaPOS.Entities.Entities;
using LiwaPOS.Shared.Models.Entities;
using System.Linq.Expressions;

namespace LiwaPOS.BLL.Interfaces
{
    public interface IPrinterTemplateService
    {
        Task<IEnumerable<PrinterTemplateDTO>> GetAllPrinterTemplatesAsync(Expression<Func<PrinterTemplate, bool>> filter = null);
        Task<IEnumerable<PrinterTemplateDTO>> GetAllPrinterTemplatesAsNoTrackingAsync(Expression<Func<PrinterTemplate, bool>> filter = null);
        Task<PrinterTemplateDTO> GetPrinterTemplateAsync(Expression<Func<PrinterTemplate, bool>> filter = null);
        Task<PrinterTemplateDTO> GetPrinterTemplateAsNoTrackingAsync(Expression<Func<PrinterTemplate, bool>> filter = null);
        Task<PrinterTemplateDTO> GetPrinterTemplateByIdAsync(int id);
        Task<PrinterTemplateDTO> GetPrinterTemplateByIdAsNoTrackingAsync(int id);
        Task AddPrinterTemplateAsync(PrinterTemplateDTO printerTemplateDto);
        Task UpdatePrinterTemplateAsync(PrinterTemplateDTO printerTemplateDto);
        Task DeletePrinterTemplateAsync(int id);
        Task DeleteAllPrinterTemplatesAsync(Expression<Func<PrinterTemplate, bool>> filter = null, IEnumerable<PrinterTemplate> entities = null);
    }
}
