using LiwaPOS.DAL.Context;
using LiwaPOS.DAL.Interfaces;
using LiwaPOS.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LiwaPOS.DAL.Repositories
{
    public class AppActionRepository : IAppActionRepository
    {
        private readonly DataContext _context;

        public AppActionRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AppAction>> GetAllAsync(Expression<Func<AppAction, bool>> filter = null)
        {
            return await _context.AppActions.Where(filter).ToListAsync();
        }

        public async Task<AppAction> GetAsync(Expression<Func<AppAction, bool>> filter = null)
        {
            return await _context.AppActions.Where(filter).FirstOrDefaultAsync();
        }

        public async Task<AppAction> GetByIdAsync(int id)
        {
            return await _context.AppActions.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task AddAsync(AppAction entity)
        {
            await _context.AppActions.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(AppAction entity)
        {
            _context.AppActions.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _context.AppActions.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
