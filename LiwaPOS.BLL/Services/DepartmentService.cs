using AutoMapper;
using LiwaPOS.BLL.Interfaces;
using LiwaPOS.DAL.Interfaces;
using LiwaPOS.Entities.Entities;
using LiwaPOS.Shared.Models.Entities;
using System.Linq.Expressions;

namespace LiwaPOS.BLL.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DepartmentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AddDepartmentAsync(DepartmentDTO departmentDto)
        {
            await _unitOfWork.ExecuteInTransactionAsync(async () =>
            {
                var department = _mapper.Map<Department>(departmentDto);
                await _unitOfWork.Departments.AddAsync(department);
                await _unitOfWork.CommitAsync();
            });
        }

        public async Task DeleteAllDepartmentsAsync(Expression<Func<Department, bool>> filter = null, IEnumerable<Department> entities = null)
        {
            await _unitOfWork.ExecuteInTransactionAsync(async () =>
            {
                await _unitOfWork.Departments.DeleteAllAsync(filter, entities);
                await _unitOfWork.CommitAsync();
            });
        }

        public async Task DeleteDepartmentAsync(int id)
        {
            await _unitOfWork.ExecuteInTransactionAsync(async () =>
            {
                await _unitOfWork.Departments.DeleteAsync(id);
                await _unitOfWork.CommitAsync();
            });
        }

        public async Task<IEnumerable<DepartmentDTO>> GetAllDepartmentsAsNoTrackingAsync(Expression<Func<Department, bool>> filter = null)
        {
            var departments = await _unitOfWork.Departments.GetAllAsNoTrackingAsync(filter);
            return _mapper.Map<IEnumerable<DepartmentDTO>>(departments);
        }

        public async Task<IEnumerable<DepartmentDTO>> GetAllDepartmentsAsync(Expression<Func<Department, bool>> filter = null)
        {
            var departments = await _unitOfWork.Departments.GetAllAsync(filter);
            return _mapper.Map<IEnumerable<DepartmentDTO>>(departments);
        }

        public async Task<DepartmentDTO> GetDepartmentAsNoTrackingAsync(Expression<Func<Department, bool>> filter = null)
        {
            var department = await _unitOfWork.Departments.GetAsNoTrackingAsync(filter);
            return _mapper.Map<DepartmentDTO>(department);
        }

        public async Task<DepartmentDTO> GetDepartmentAsync(Expression<Func<Department, bool>> filter = null)
        {
            var department = await _unitOfWork.Departments.GetAsync(filter);
            return _mapper.Map<DepartmentDTO>(department);
        }

        public async Task<DepartmentDTO> GetDepartmentByIdAsNoTrackingAsync(int id)
        {
            var department = await _unitOfWork.Departments.GetByIdAsNoTrackingAsync(id);
            return _mapper.Map<DepartmentDTO>(department);
        }

        public async Task<DepartmentDTO> GetDepartmentByIdAsync(int id)
        {
            var department = await _unitOfWork.Departments.GetByIdAsync(id);
            return _mapper.Map<DepartmentDTO>(department);
        }

        public async Task UpdateDepartmentAsync(DepartmentDTO departmentDto)
        {
            await _unitOfWork.ExecuteInTransactionAsync(async () =>
            {
                var department = _mapper.Map<Department>(departmentDto);
                await _unitOfWork.Departments.UpdateAsync(department);
                await _unitOfWork.CommitAsync();
            });
        }
    }
}
