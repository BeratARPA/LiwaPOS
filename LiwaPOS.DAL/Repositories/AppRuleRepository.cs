using LiwaPOS.DAL.Context;
using LiwaPOS.DAL.Interfaces;
using LiwaPOS.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LiwaPOS.DAL.Repositories
{
    public class AppRuleRepository : IAppRuleRepository
    {
        private readonly DataContext _context;

        public AppRuleRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AppRule>> GetAllAsync(Expression<Func<AppRule, bool>> filter = null)
        {
            return filter != null
                ? await _context.AppRules.Where(filter).ToListAsync()
                : await _context.AppRules.ToListAsync();
        }

        public async Task<AppRule> GetAsync(Expression<Func<AppRule, bool>> filter = null)
        {
            return filter != null
                ? await _context.AppRules.Where(filter).FirstOrDefaultAsync()
                : await _context.AppRules.FirstOrDefaultAsync();
        }

        public async Task<AppRule> GetByIdAsync(int id)
        {
            return await _context.AppRules.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task AddAsync(AppRule entity)
        {
            await _context.AppRules.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(AppRule entity)
        {
            var localEntity = _context.Scripts.Local.FirstOrDefault(e => e.Id == entity.Id);
            if (localEntity != null)
            {
                _context.Entry(localEntity).State = EntityState.Detached;
            }

            _context.AppRules.Update(entity);   
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _context.AppRules.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<AppRule>> GetAllAsNoTrackingAsync(Expression<Func<AppRule, bool>> filter = null)
        {
            return filter != null
               ? await _context.AppRules.AsNoTracking().Where(filter).ToListAsync()
               : await _context.AppRules.AsNoTracking().ToListAsync();
        }

        public async Task<AppRule> GetAsNoTrackingAsync(Expression<Func<AppRule, bool>> filter = null)
        {
            return filter != null
                 ? await _context.AppRules.AsNoTracking().Where(filter).FirstOrDefaultAsync()
                 : await _context.AppRules.AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<AppRule> GetByIdAsNoTrackingAsync(int id)
        {
            return await _context.AppRules.AsNoTracking().FirstOrDefaultAsync(r => r.Id == id);
        }
    }
}
