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
            return await _context.AppRules.Include(r => r.Actions).Where(filter).ToListAsync();
        }

        public async Task<AppRule> GetAsync(Expression<Func<AppRule, bool>> filter = null)
        {
            return await _context.AppRules.Where(filter).FirstOrDefaultAsync();
        }

        public async Task<AppRule> GetByIdAsync(int id)
        {
            return await _context.AppRules.Include(r => r.Actions) .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task AddAsync(AppRule entity)
        {
            await _context.AppRules.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(AppRule entity)
        {
            _context.AppRules.Update(entity);   
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var rule = await GetByIdAsync(id);
            if (rule != null)
            {
                _context.AppRules.Remove(rule);
                await _context.SaveChangesAsync();
            }
        }
    }
}
