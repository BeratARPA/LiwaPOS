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
            return filter != null
                ? await _context.Scripts.Where(filter).FirstOrDefaultAsync()
                : await _context.Scripts.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Script>> GetAllAsync(Expression<Func<Script, bool>> filter = null)
        {
            return filter != null
                ? await _context.Scripts.Where(filter).ToListAsync()
                : await _context.Scripts.ToListAsync();
        }

        public async Task<Script> GetByIdAsync(int id)
        {
            return await _context.Scripts.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task UpdateAsync(Script entity)
        {
            var localEntity = _context.Scripts.Local.FirstOrDefault(e => e.Id == entity.Id);
            if (localEntity != null)
            {
                _context.Entry(localEntity).State = EntityState.Detached;
            }

            _context.Scripts.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Script>> GetAllAsNoTrackingAsync(Expression<Func<Script, bool>> filter = null)
        {
            return filter != null
             ? await _context.Scripts.AsNoTracking().Where(filter).ToListAsync()
             : await _context.Scripts.AsNoTracking().ToListAsync();
        }

        public async Task<Script> GetAsNoTrackingAsync(Expression<Func<Script, bool>> filter = null)
        {
           return filter != null
                ? await _context.Scripts.AsNoTracking().Where(filter).FirstOrDefaultAsync()
                : await _context.Scripts.AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<Script> GetByIdAsNoTrackingAsync(int id)
        {
            return await _context.Scripts.AsNoTracking().FirstOrDefaultAsync(r => r.Id == id);
        }
    }
}
