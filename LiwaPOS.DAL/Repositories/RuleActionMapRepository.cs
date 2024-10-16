using LiwaPOS.DAL.Context;
using LiwaPOS.DAL.Interfaces;
using LiwaPOS.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LiwaPOS.DAL.Repositories
{
    public class RuleActionMapRepository : IRuleActionMapRepository
    {
        private readonly DataContext _context;

        public RuleActionMapRepository(DataContext context)
        {
            _context = context;
        }
        public async Task AddAsync(RuleActionMap entity)
        {
            await _context.RuleActionMaps.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _context.RuleActionMaps.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<RuleActionMap>> GetAllAsync(Expression<Func<RuleActionMap, bool>> filter = null)
        {
            return filter != null
                ? await _context.RuleActionMaps.Where(filter).ToListAsync()
                : await _context.RuleActionMaps.ToListAsync();
        }

        public async Task<IEnumerable<RuleActionMap>> GetAllAsNoTrackingAsync(Expression<Func<RuleActionMap, bool>> filter = null)
        {
            return filter != null
                ? await _context.RuleActionMaps.AsNoTracking().Where(filter).ToListAsync()
                : await _context.RuleActionMaps.AsNoTracking().ToListAsync();
        }

        public async Task<RuleActionMap> GetAsNoTrackingAsync(Expression<Func<RuleActionMap, bool>> filter = null)
        {
            return filter != null
                 ? await _context.RuleActionMaps.AsNoTracking().Where(filter).FirstOrDefaultAsync()
                 : await _context.RuleActionMaps.AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<RuleActionMap> GetAsync(Expression<Func<RuleActionMap, bool>> filter = null)
        {
            return filter != null
                ? await _context.RuleActionMaps.Where(filter).FirstOrDefaultAsync()
                : await _context.RuleActionMaps.FirstOrDefaultAsync();
        }

        public async Task<RuleActionMap> GetByIdAsNoTrackingAsync(int id)
        {
            return await _context.RuleActionMaps.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<RuleActionMap> GetByIdAsync(int id)
        {
            return await _context.RuleActionMaps.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task UpdateAsync(RuleActionMap entity)
        {
            var localEntity = _context.Scripts.Local.FirstOrDefault(e => e.Id == entity.Id);
            if (localEntity != null)
            {
                _context.Entry(localEntity).State = EntityState.Detached;
            }

            _context.RuleActionMaps.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
