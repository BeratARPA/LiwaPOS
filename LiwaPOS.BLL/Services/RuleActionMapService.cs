using AutoMapper;
using LiwaPOS.BLL.Interfaces;
using LiwaPOS.DAL.Interfaces;
using LiwaPOS.Entities.DTOs;
using LiwaPOS.Entities.Entities;
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
            var ruleActionMap = _mapper.Map<RuleActionMap>(ruleActionMapDto);
            await _unitOfWork.RuleActionMaps.AddAsync(ruleActionMap);
            _unitOfWork.Commit();
        }

        public async Task DeleteRuleActionMapAsync(int id)
        {
            await _unitOfWork.RuleActionMaps.DeleteAsync(id);
            _unitOfWork.Commit();
        }

        public async Task<IEnumerable<RuleActionMapDTO>> GetAllRuleActionMapsAsync(Expression<Func<RuleActionMap, bool>> filter = null)
        {
            var ruleActionMaps = await _unitOfWork.RuleActionMaps.GetAllAsync(filter);
            return _mapper.Map<IEnumerable<RuleActionMapDTO>>(ruleActionMaps);
        }

        public async Task<RuleActionMapDTO> GetRuleActionMapAsync(Expression<Func<RuleActionMap, bool>> filter = null)
        {
            var ruleActionMap = await _unitOfWork.RuleActionMaps.GetAsync(filter);
            return _mapper.Map<RuleActionMapDTO>(ruleActionMap);
        }

        public async Task<RuleActionMapDTO> GetRuleActionMapByIdAsync(int id)
        {
            var ruleActionMap = await _unitOfWork.RuleActionMaps.GetByIdAsync(id);
            return _mapper.Map<RuleActionMapDTO>(ruleActionMap);
        }

        public async Task UpdateRuleActionMapAsync(RuleActionMapDTO ruleActionMapDto)
        {
            var ruleActionMap = _mapper.Map<RuleActionMap>(ruleActionMapDto);
            await _unitOfWork.RuleActionMaps.UpdateAsync(ruleActionMap);
            _unitOfWork.Commit();
        }
    }
}
