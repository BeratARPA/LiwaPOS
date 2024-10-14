using LiwaPOS.DAL.Context;
using LiwaPOS.DAL.Interfaces;
using LiwaPOS.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LiwaPOS.DAL.Repositories
{
    public class ScriptRepository : IScriptRepository
    {
        private readonly DataContext _context;

        public ScriptRepository(DataContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Script entity)
        {
            await _context.Scripts.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _context.Scripts.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Script> GetAsync(Expression<Func<Script, bool>> filter = null)
        {
            return await _context.Scripts.Where(filter).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Script>> GetAllAsync(Expression<Func<Script, bool>> filter = null)
        {
            return await _context.Scripts.Where(filter).ToListAsync();
        }

        public async Task<Script> GetByIdAsync(int id)
        {
            return await _context.Scripts.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task UpdateAsync(Script entity)
        {
            _context.Scripts.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
