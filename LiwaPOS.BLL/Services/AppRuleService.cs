using AutoMapper;
using LiwaPOS.BLL.Interfaces;
using LiwaPOS.DAL.Interfaces;
using LiwaPOS.Entities.Entities;
using LiwaPOS.Shared.Models.Entities;
using System.Linq.Expressions;

namespace LiwaPOS.BLL.Services
{
    public class AppRuleService : IAppRuleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AppRuleService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AppRuleDTO>> GetAllAppRulesAsync(Expression<Func<AppRule, bool>> filter = null)
        {
            var appRules = await _unitOfWork.AppRules.GetAllAsync(filter);
            return _mapper.Map<IEnumerable<AppRuleDTO>>(appRules);
        }

        public async Task<AppRuleDTO> GetAppRuleAsync(Expression<Func<AppRule, bool>> filter = null)
        {
            var appRule = await _unitOfWork.AppRules.GetAsync(filter);
            return _mapper.Map<AppRuleDTO>(appRule);
        }

        public async Task AddAppRuleAsync(AppRuleDTO appRuleDto)
        {
            await _unitOfWork.ExecuteInTransactionAsync(async () =>
            {
                var appRule = _mapper.Map<AppRule>(appRuleDto);
                await _unitOfWork.AppRules.AddAsync(appRule);
                await _unitOfWork.CommitAsync();
            });
        }

        public async Task UpdateAppRuleAsync(AppRuleDTO appRuleDto)
        {
            await _unitOfWork.ExecuteInTransactionAsync(async () =>
            {
                var appRule = _mapper.Map<AppRule>(appRuleDto);
                await _unitOfWork.AppRules.UpdateAsync(appRule);
                await _unitOfWork.CommitAsync();
            });
        }

        public async Task DeleteAppRuleAsync(int id)
        {
            await _unitOfWork.ExecuteInTransactionAsync(async () =>
            {
                await _unitOfWork.AppRules.DeleteAsync(id);
                await _unitOfWork.CommitAsync();
            });
        }

        public async Task<AppRuleDTO> GetAppRuleByIdAsync(int id)
        {
            var appRule = await _unitOfWork.AppRules.GetByIdAsync(id);
            return _mapper.Map<AppRuleDTO>(appRule);
        }

        public async Task<IEnumerable<AppRuleDTO>> GetAllAppRulesAsNoTrackingAsync(Expression<Func<AppRule, bool>> filter = null)
        {
            var appRules = await _unitOfWork.AppRules.GetAllAsNoTrackingAsync(filter);
            return _mapper.Map<IEnumerable<AppRuleDTO>>(appRules);
        }

        public async Task<AppRuleDTO> GetAppRuleAsNoTrackingAsync(Expression<Func<AppRule, bool>> filter = null)
        {
            var appRule = await _unitOfWork.AppRules.GetAsNoTrackingAsync(filter);
            return _mapper.Map<AppRuleDTO>(appRule);
        }

        public async Task<AppRuleDTO> GetAppRuleByIdAsNoTrackingAsync(int id)
        {
            var appRule = await _unitOfWork.AppRules.GetByIdAsNoTrackingAsync(id);
            return _mapper.Map<AppRuleDTO>(appRule);
        }

        public async Task DeleteAllAppRulesAsync(Expression<Func<AppRule, bool>> filter = null, IEnumerable<AppRule> entities = null)
        {
            await _unitOfWork.ExecuteInTransactionAsync(async () =>
            {
                await _unitOfWork.AppRules.DeleteAllAsync(filter,entities);
                await _unitOfWork.CommitAsync();
            });
        }
    }
}
