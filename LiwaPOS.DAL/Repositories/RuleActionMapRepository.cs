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
            var action = await GetByIdAsync(id);
            if (action != null)
            {
                _context.RuleActionMaps.Remove(action);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<RuleActionMap>> GetAllAsync(Expression<Func<RuleActionMap, bool>> filter = null)
        {
            return await _context.RuleActionMaps.Where(filter).ToListAsync();
        }

        public async Task<RuleActionMap> GetAsync(Expression<Func<RuleActionMap, bool>> filter = null)
        {
            return await _context.RuleActionMaps.Where(filter).FirstOrDefaultAsync();
        }

        public async Task<RuleActionMap> GetByIdAsync(int id)
        {
            return await _context.RuleActionMaps.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task UpdateAsync(RuleActionMap entity)
        {
            _context.RuleActionMaps.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
