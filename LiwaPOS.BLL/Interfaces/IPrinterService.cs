using LiwaPOS.Entities.Entities;
using LiwaPOS.Shared.Models.Entities;
using System.Linq.Expressions;

namespace LiwaPOS.BLL.Interfaces
{
    public interface IPrinterService
    {
        Task<IEnumerable<PrinterDTO>> GetAllPrintersAsync(Expression<Func<Printer, bool>> filter = null);
        Task<IEnumerable<PrinterDTO>> GetAllPrintersAsNoTrackingAsync(Expression<Func<Printer, bool>> filter = null);
        Task<PrinterDTO> GetPrinterAsync(Expression<Func<Printer, bool>> filter = null);
        Task<PrinterDTO> GetPrinterAsNoTrackingAsync(Expression<Func<Printer, bool>> filter = null);
        Task<PrinterDTO> GetPrinterByIdAsync(int id);
        Task<PrinterDTO> GetPrinterByIdAsNoTrackingAsync(int id);
        Task AddPrinterAsync(PrinterDTO PrinterDto);
        Task UpdatePrinterAsync(PrinterDTO PrinterDto);
        Task DeletePrinterAsync(int id);
        Task DeleteAllPrintersAsync(Expression<Func<Printer, bool>> filter = null, IEnumerable<Printer> entities = null);
    }
}
