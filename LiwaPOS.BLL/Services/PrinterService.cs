using AutoMapper;
using LiwaPOS.BLL.Interfaces;
using LiwaPOS.DAL.Interfaces;
using LiwaPOS.Entities.Entities;
using LiwaPOS.Shared.Models.Entities;
using System.Linq.Expressions;

namespace LiwaPOS.BLL.Services
{
    public class PrinterService: IPrinterService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PrinterService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AddPrinterAsync(PrinterDTO printerDto)
        {
            await _unitOfWork.ExecuteInTransactionAsync(async () =>
            {
                var printer = _mapper.Map<Printer>(printerDto);
                await _unitOfWork.Printers.AddAsync(printer);
                await _unitOfWork.CommitAsync();
            });
        }

        public async Task DeleteAllPrintersAsync(Expression<Func<Printer, bool>> filter = null, IEnumerable<Printer> entities = null)
        {
            await _unitOfWork.ExecuteInTransactionAsync(async () =>
            {
                await _unitOfWork.Printers.DeleteAllAsync(filter, entities);
                await _unitOfWork.CommitAsync();
            });
        }

        public async Task DeletePrinterAsync(int id)
        {
            await _unitOfWork.ExecuteInTransactionAsync(async () =>
            {
                await _unitOfWork.Printers.DeleteAsync(id);
                await _unitOfWork.CommitAsync();
            });
        }

        public async Task<IEnumerable<PrinterDTO>> GetAllPrintersAsNoTrackingAsync(Expression<Func<Printer, bool>> filter = null)
        {
            var printers = await _unitOfWork.Printers.GetAllAsNoTrackingAsync(filter);
            return _mapper.Map<IEnumerable<PrinterDTO>>(printers);
        }

        public async Task<IEnumerable<PrinterDTO>> GetAllPrintersAsync(Expression<Func<Printer, bool>> filter = null)
        {
            var printers = await _unitOfWork.Printers.GetAllAsync(filter);
            return _mapper.Map<IEnumerable<PrinterDTO>>(printers);
        }

        public async Task<PrinterDTO> GetPrinterAsNoTrackingAsync(Expression<Func<Printer, bool>> filter = null)
        {
            var printer = await _unitOfWork.Printers.GetAsNoTrackingAsync(filter);
            return _mapper.Map<PrinterDTO>(printer);
        }

        public async Task<PrinterDTO> GetPrinterAsync(Expression<Func<Printer, bool>> filter = null)
        {
            var printer = await _unitOfWork.Printers.GetAsync(filter);
            return _mapper.Map<PrinterDTO>(printer);
        }

        public async Task<PrinterDTO> GetPrinterByIdAsNoTrackingAsync(int id)
        {
            var printer = await _unitOfWork.Printers.GetByIdAsNoTrackingAsync(id);
            return _mapper.Map<PrinterDTO>(printer);
        }

        public async Task<PrinterDTO> GetPrinterByIdAsync(int id)
        {
            var printer = await _unitOfWork.Printers.GetByIdAsync(id);
            return _mapper.Map<PrinterDTO>(printer);
        }

        public async Task UpdatePrinterAsync(PrinterDTO printerDto)
        {
            await _unitOfWork.ExecuteInTransactionAsync(async () =>
            {
                var printer = _mapper.Map<Printer>(printerDto);
                await _unitOfWork.Printers.UpdateAsync(printer);
                await _unitOfWork.CommitAsync();
            });
        }
    }
}
