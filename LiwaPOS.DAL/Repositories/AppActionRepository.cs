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
            return filter != null
                ? await _context.AppActions.Where(filter).ToListAsync()
                : await _context.AppActions.ToListAsync();
        }

        public async Task<AppAction> GetAsync(Expression<Func<AppAction, bool>> filter = null)
        {
            return filter != null
                ? await _context.AppActions.Where(filter).FirstOrDefaultAsync()
                : await _context.AppActions.FirstOrDefaultAsync();
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
            var localEntity = _context.AppActions.Local.FirstOrDefault(e => e.Id == entity.Id);
            if (localEntity != null)
            {
                _context.Entry(localEntity).State = EntityState.Detached;
            }

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

        public async Task<IEnumerable<AppAction>> GetAllAsNoTrackingAsync(Expression<Func<AppAction, bool>> filter = null)
        {
            return filter != null
               ? await _context.AppActions.AsNoTracking().Where(filter).ToListAsync()
               : await _context.AppActions.AsNoTracking().ToListAsync();
        }

        public async Task<AppAction> GetAsNoTrackingAsync(Expression<Func<AppAction, bool>> filter = null)
        {
            return filter != null
               ? await _context.AppActions.AsNoTracking().Where(filter).FirstOrDefaultAsync()
               : await _context.AppActions.AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<AppAction> GetByIdAsNoTrackingAsync(int id)
        {
            return await _context.AppActions.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
        }
    }
}
