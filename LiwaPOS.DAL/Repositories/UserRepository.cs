using LiwaPOS.DAL.Context;
using LiwaPOS.DAL.Interfaces;
using LiwaPOS.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LiwaPOS.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public async Task AddAsync(User entity)
        {
            await _context.Users.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _context.Users.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<User> GetAsync(Expression<Func<User, bool>> filter = null)
        {
            return filter != null
                ? await _context.Users.Where(filter).FirstOrDefaultAsync()
                : await _context.Users.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<User>> GetAllAsync(Expression<Func<User, bool>> filter = null)
        {
            return filter != null
                ? await _context.Users.Include(r => r.UserRole).Where(filter).ToListAsync()
                : await _context.Users.Include(r => r.UserRole).ToListAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.Users.Include(r => r.UserRole).FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task UpdateAsync(User entity)
        {
            var localEntity = _context.Scripts.Local.FirstOrDefault(e => e.Id == entity.Id);
            if (localEntity != null)
            {
                _context.Entry(localEntity).State = EntityState.Detached;
            }

            _context.Users.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<User>> GetAllAsNoTrackingAsync(Expression<Func<User, bool>> filter = null)
        {
            return filter != null
                  ? await _context.Users.AsNoTracking().Include(r => r.UserRole).Where(filter).ToListAsync()
                  : await _context.Users.AsNoTracking().Include(r => r.UserRole).ToListAsync();
        }

        public async Task<User> GetAsNoTrackingAsync(Expression<Func<User, bool>> filter = null)
        {
            return filter != null
                    ? await _context.Users.AsNoTracking().Where(filter).FirstOrDefaultAsync()
                    : await _context.Users.AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<User> GetByIdAsNoTrackingAsync(int id)
        {
            return await _context.Users.AsNoTracking().Include(r => r.UserRole).FirstOrDefaultAsync(r => r.Id == id);
        }
    }
}
