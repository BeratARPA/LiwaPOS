using AutoMapper;
using LiwaPOS.BLL.Interfaces;
using LiwaPOS.DAL.Interfaces;
using LiwaPOS.Entities.Entities;
using LiwaPOS.Shared.Models.Entities;
using System.Linq.Expressions;

namespace LiwaPOS.BLL.Services
{
    public class ProgramSettingValueService : IProgramSettingValueService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProgramSettingValueService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AddProgramSettingValueAsync(ProgramSettingValueDTO programSettingValueDto)
        {
            await _unitOfWork.ExecuteInTransactionAsync(async () =>
            {
                var programSettingValue = _mapper.Map<ProgramSettingValue>(programSettingValueDto);
                await _unitOfWork.ProgramSettingValues.AddAsync(programSettingValue);
                await _unitOfWork.CommitAsync();
            });
        }

        public async Task DeleteAllProgramSettingValuesAsync(Expression<Func<ProgramSettingValue, bool>> filter = null, IEnumerable<ProgramSettingValue> entities = null)
        {
            await _unitOfWork.ExecuteInTransactionAsync(async () =>
            {
                await _unitOfWork.ProgramSettingValues.DeleteAllAsync(filter, entities);
                await _unitOfWork.CommitAsync();
            });
        }

        public async Task DeleteProgramSettingValueAsync(int id)
        {
            await _unitOfWork.ExecuteInTransactionAsync(async () =>
            {
                await _unitOfWork.ProgramSettingValues.DeleteAsync(id);
                await _unitOfWork.CommitAsync();
            });
        }

        public async Task<IEnumerable<ProgramSettingValueDTO>> GetAllProgramSettingValuesAsNoTrackingAsync(Expression<Func<ProgramSettingValue, bool>> filter = null)
        {
            var programSettingValues = await _unitOfWork.ProgramSettingValues.GetAllAsNoTrackingAsync(filter);
            return _mapper.Map<IEnumerable<ProgramSettingValueDTO>>(programSettingValues);
        }

        public async Task<IEnumerable<ProgramSettingValueDTO>> GetAllProgramSettingValuesAsync(Expression<Func<ProgramSettingValue, bool>> filter = null)
        {
            var programSettingValues = await _unitOfWork.ProgramSettingValues.GetAllAsync(filter);
            return _mapper.Map<IEnumerable<ProgramSettingValueDTO>>(programSettingValues);
        }

        public async Task<ProgramSettingValueDTO> GetProgramSettingValueAsNoTrackingAsync(Expression<Func<ProgramSettingValue, bool>> filter = null)
        {
            var programSettingValue = await _unitOfWork.ProgramSettingValues.GetAsNoTrackingAsync(filter);
            return _mapper.Map<ProgramSettingValueDTO>(programSettingValue);
        }

        public async Task<ProgramSettingValueDTO> GetProgramSettingValueAsync(Expression<Func<ProgramSettingValue, bool>> filter = null)
        {
            var programSettingValue = await _unitOfWork.ProgramSettingValues.GetAsync(filter);
            return _mapper.Map<ProgramSettingValueDTO>(programSettingValue);
        }

        public async Task<ProgramSettingValueDTO> GetProgramSettingValueByIdAsNoTrackingAsync(int id)
        {
            var programSettingValue = await _unitOfWork.ProgramSettingValues.GetByIdAsNoTrackingAsync(id);
            return _mapper.Map<ProgramSettingValueDTO>(programSettingValue);
        }

        public async Task<ProgramSettingValueDTO> GetProgramSettingValueByIdAsync(int id)
        {
            var programSettingValue = await _unitOfWork.ProgramSettingValues.GetByIdAsync(id);
            return _mapper.Map<ProgramSettingValueDTO>(programSettingValue);
        }

        public async Task UpdateProgramSettingValueAsync(ProgramSettingValueDTO programSettingValueDto)
        {
            await _unitOfWork.ExecuteInTransactionAsync(async () =>
            {
                var programSettingValue = _mapper.Map<ProgramSettingValue>(programSettingValueDto);
                await _unitOfWork.ProgramSettingValues.UpdateAsync(programSettingValue);
                await _unitOfWork.CommitAsync();
            });
        }
    }
}
