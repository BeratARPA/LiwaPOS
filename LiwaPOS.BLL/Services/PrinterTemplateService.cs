using AutoMapper;
using LiwaPOS.BLL.Interfaces;
using LiwaPOS.DAL.Interfaces;
using LiwaPOS.Entities.Entities;
using LiwaPOS.Shared.Models.Entities;
using System.Linq.Expressions;

namespace LiwaPOS.BLL.Services
{
    public class PrinterTemplateService : IPrinterTemplateService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PrinterTemplateService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AddPrinterTemplateAsync(PrinterTemplateDTO printerTemplateDto)
        {
            await _unitOfWork.ExecuteInTransactionAsync(async () =>
            {
                var printerTemplate = _mapper.Map<PrinterTemplate>(printerTemplateDto);
                await _unitOfWork.PrinterTemplates.AddAsync(printerTemplate);
                await _unitOfWork.CommitAsync();
            });
        }

        public async Task DeleteAllPrinterTemplatesAsync(Expression<Func<PrinterTemplate, bool>> filter = null, IEnumerable<PrinterTemplate> entities = null)
        {
            await _unitOfWork.ExecuteInTransactionAsync(async () =>
            {
                await _unitOfWork.PrinterTemplates.DeleteAllAsync(filter, entities);
                await _unitOfWork.CommitAsync();
            });
        }

        public async Task DeletePrinterTemplateAsync(int id)
        {
            await _unitOfWork.ExecuteInTransactionAsync(async () =>
            {
                await _unitOfWork.PrinterTemplates.DeleteAsync(id);
                await _unitOfWork.CommitAsync();
            });
        }

        public async Task<IEnumerable<PrinterTemplateDTO>> GetAllPrinterTemplatesAsNoTrackingAsync(Expression<Func<PrinterTemplate, bool>> filter = null)
        {
            var printerTemplates = await _unitOfWork.PrinterTemplates.GetAllAsNoTrackingAsync(filter);
            return _mapper.Map<IEnumerable<PrinterTemplateDTO>>(printerTemplates);
        }

        public async Task<IEnumerable<PrinterTemplateDTO>> GetAllPrinterTemplatesAsync(Expression<Func<PrinterTemplate, bool>> filter = null)
        {
            var printerTemplates = await _unitOfWork.PrinterTemplates.GetAllAsync(filter);
            return _mapper.Map<IEnumerable<PrinterTemplateDTO>>(printerTemplates);
        }

        public async Task<PrinterTemplateDTO> GetPrinterTemplateAsNoTrackingAsync(Expression<Func<PrinterTemplate, bool>> filter = null)
        {
            var printerTemplate = await _unitOfWork.PrinterTemplates.GetAsNoTrackingAsync(filter);
            return _mapper.Map<PrinterTemplateDTO>(printerTemplate);
        }

        public async Task<PrinterTemplateDTO> GetPrinterTemplateAsync(Expression<Func<PrinterTemplate, bool>> filter = null)
        {
            var printerTemplate = await _unitOfWork.PrinterTemplates.GetAsync(filter);
            return _mapper.Map<PrinterTemplateDTO>(printerTemplate);
        }

        public async Task<PrinterTemplateDTO> GetPrinterTemplateByIdAsNoTrackingAsync(int id)
        {
            var printerTemplate = await _unitOfWork.PrinterTemplates.GetByIdAsNoTrackingAsync(id);
            return _mapper.Map<PrinterTemplateDTO>(printerTemplate);
        }

        public async Task<PrinterTemplateDTO> GetPrinterTemplateByIdAsync(int id)
        {
            var printerTemplate = await _unitOfWork.PrinterTemplates.GetByIdAsync(id);
            return _mapper.Map<PrinterTemplateDTO>(printerTemplate);
        }

        public async Task UpdatePrinterTemplateAsync(PrinterTemplateDTO printerTemplateDto)
        {
            await _unitOfWork.ExecuteInTransactionAsync(async () =>
            {
                var printerTemplate = _mapper.Map<PrinterTemplate>(printerTemplateDto);
                await _unitOfWork.PrinterTemplates.UpdateAsync(printerTemplate);
                await _unitOfWork.CommitAsync();
            });
        }
    }
}
