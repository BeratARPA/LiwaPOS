using AutoMapper;
using LiwaPOS.BLL.Interfaces;
using LiwaPOS.DAL.Interfaces;
using LiwaPOS.Entities.Entities;
using LiwaPOS.Shared.Models.Entities;
using System.Linq.Expressions;

namespace LiwaPOS.BLL.Services
{
    public class RuleActionMapService : IRuleActionMapService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RuleActionMapService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AddRuleActionMapAsync(RuleActionMapDTO ruleActionMapDto)
        {
            await _unitOfWork.ExecuteInTransactionAsync(async () =>
            {
                var ruleActionMap = _mapper.Map<RuleActionMap>(ruleActionMapDto);
                await _unitOfWork.RuleActionMaps.AddAsync(ruleActionMap);
                await _unitOfWork.CommitAsync();
            });
        }

        public async Task DeleteAllRuleActionMapsAsync(Expression<Func<RuleActionMap, bool>> filter = null, IEnumerable<RuleActionMap> entities = null)
        {
            await _unitOfWork.ExecuteInTransactionAsync(async () =>
            {
                await _unitOfWork.RuleActionMaps.DeleteAllAsync(filter,entities);
                await _unitOfWork.CommitAsync();
            });
        }

        public async Task DeleteRuleActionMapAsync(int id)
        {
            await _unitOfWork.ExecuteInTransactionAsync(async () =>
            {
                await _unitOfWork.RuleActionMaps.DeleteAsync(id);
                await _unitOfWork.CommitAsync();
            });
        }

        public async Task<IEnumerable<RuleActionMapDTO>> GetAllRuleActionMapsAsNoTrackingAsync(Expression<Func<RuleActionMap, bool>> filter = null)
        {
            var ruleActionMaps = await _unitOfWork.RuleActionMaps.GetAllAsNoTrackingAsync(filter);
            return _mapper.Map<IEnumerable<RuleActionMapDTO>>(ruleActionMaps);
        }

        public async Task<IEnumerable<RuleActionMapDTO>> GetAllRuleActionMapsAsync(Expression<Func<RuleActionMap, bool>> filter = null)
        {
            var ruleActionMaps = await _unitOfWork.RuleActionMaps.GetAllAsync(filter);
            return _mapper.Map<IEnumerable<RuleActionMapDTO>>(ruleActionMaps);
        }

        public async Task<RuleActionMapDTO> GetRuleActionMapAsNoTrackingAsync(Expression<Func<RuleActionMap, bool>> filter = null)
        {
            var ruleActionMap = await _unitOfWork.RuleActionMaps.GetAsNoTrackingAsync(filter);
            return _mapper.Map<RuleActionMapDTO>(ruleActionMap);
        }

        public async Task<RuleActionMapDTO> GetRuleActionMapAsync(Expression<Func<RuleActionMap, bool>> filter = null)
        {
            var ruleActionMap = await _unitOfWork.RuleActionMaps.GetAsync(filter);
            return _mapper.Map<RuleActionMapDTO>(ruleActionMap);
        }

        public async Task<RuleActionMapDTO> GetRuleActionMapByIdAsNoTrackingAsync(int id)
        {
            var ruleActionMap = await _unitOfWork.RuleActionMaps.GetByIdAsNoTrackingAsync(id);
            return _mapper.Map<RuleActionMapDTO>(ruleActionMap);
        }

        public async Task<RuleActionMapDTO> GetRuleActionMapByIdAsync(int id)
        {
            var ruleActionMap = await _unitOfWork.RuleActionMaps.GetByIdAsync(id);
            return _mapper.Map<RuleActionMapDTO>(ruleActionMap);
        }

        public async Task UpdateRuleActionMapAsync(RuleActionMapDTO ruleActionMapDto)
        {
            await _unitOfWork.ExecuteInTransactionAsync(async () =>
            {
                var ruleActionMap = _mapper.Map<RuleActionMap>(ruleActionMapDto);
                await _unitOfWork.RuleActionMaps.UpdateAsync(ruleActionMap);
                await _unitOfWork.CommitAsync();
            });
        }
    }
}
