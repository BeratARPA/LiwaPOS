using AutoMapper;
using LiwaPOS.BLL.Interfaces;
using LiwaPOS.DAL.Interfaces;
using LiwaPOS.Entities.Entities;
using LiwaPOS.Shared.Models.Entities;
using System.Linq.Expressions;

namespace LiwaPOS.BLL.Services
{
    public class ScriptService : IScriptService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ScriptService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AddScriptAsync(ScriptDTO scriptDto)
        {
            var script = _mapper.Map<Script>(scriptDto);
            await _unitOfWork.Scripts.AddAsync(script);
            _unitOfWork.Commit();
        }

        public async Task DeleteScriptAsync(int id)
        {
            await _unitOfWork.Scripts.DeleteAsync(id);
            _unitOfWork.Commit();
        }

        public async Task<IEnumerable<ScriptDTO>> GetAllScriptsAsNoTrackingAsync(Expression<Func<Script, bool>> filter = null)
        {
            var scripts = await _unitOfWork.Scripts.GetAllAsNoTrackingAsync(filter);
            return _mapper.Map<IEnumerable<ScriptDTO>>(scripts);
        }

        public async Task<IEnumerable<ScriptDTO>> GetAllScriptsAsync(Expression<Func<Script, bool>> filter = null)
        {
            var scripts = await _unitOfWork.Scripts.GetAllAsync(filter);
            return _mapper.Map<IEnumerable<ScriptDTO>>(scripts);
        }

        public async Task<ScriptDTO> GetScriptAsNoTrackingAsync(Expression<Func<Script, bool>> filter = null)
        {
            var script = await _unitOfWork.Scripts.GetAsNoTrackingAsync(filter);
            return _mapper.Map<ScriptDTO>(script);
        }

        public async Task<ScriptDTO> GetScriptAsync(Expression<Func<Script, bool>> filter = null)
        {
            var script = await _unitOfWork.Scripts.GetAsync(filter);
            return _mapper.Map<ScriptDTO>(script);
        }

        public async Task<ScriptDTO> GetScriptByIdAsNoTrackingAsync(int id)
        {
            var script = await _unitOfWork.Scripts.GetByIdAsNoTrackingAsync(id);
            return _mapper.Map<ScriptDTO>(script);
        }

        public async Task<ScriptDTO> GetScriptByIdAsync(int id)
        {
            var script = await _unitOfWork.Scripts.GetByIdAsync(id);
            return _mapper.Map<ScriptDTO>(script);
        }

        public async Task UpdateScriptAsync(ScriptDTO scriptDto)
        {
            var script = _mapper.Map<Script>(scriptDto);
            await _unitOfWork.Scripts.UpdateAsync(script);
            _unitOfWork.Commit();
        }
    }
}
