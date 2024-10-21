using LiwaPOS.DAL.Context;
using LiwaPOS.DAL.Interfaces;
using LiwaPOS.Shared.Services;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LiwaPOS.DAL.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly DataContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(DataContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            try
            {
                IQueryable<TEntity> query = _dbSet;

                if (filter != null)
                {
                    query = query.Where(filter);
                }

                return await query.ToListAsync();
            }
            catch (Exception exception)
            {
                await LoggingService.LogErrorAsync("Error occurred while processing data.", typeof(GenericRepository<TEntity>).ToString(), "Filter: " + filter?.ToString(), exception);
                throw;
            }
        }

        public async Task<IEnumerable<TEntity>> GetAllAsNoTrackingAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            try
            {
                IQueryable<TEntity> query = _dbSet.AsNoTracking();

                if (filter != null)
                {
                    query = query.Where(filter);
                }

                return await query.ToListAsync();
            }
            catch (Exception exception)
            {
                await LoggingService.LogErrorAsync("Error occurred while processing data.", typeof(GenericRepository<TEntity>).ToString(), "Filter: " + filter?.ToString(), exception);
                throw;
            }           
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            try
            {
                IQueryable<TEntity> query = _dbSet;

                if (filter != null)
                {
                    query = query.Where(filter);
                }

                return await query.FirstOrDefaultAsync();
            }
            catch (Exception exception)
            {
                await LoggingService.LogErrorAsync("Error occurred while processing data.", typeof(GenericRepository<TEntity>).ToString(), "Filter: " + filter?.ToString(), exception);
                throw;
            }
        }

        public async Task<TEntity> GetAsNoTrackingAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            try
            {
                IQueryable<TEntity> query = _dbSet.AsNoTracking();

                if (filter != null)
                {
                    query = query.Where(filter);
                }

                return await query.FirstOrDefaultAsync();
            }
            catch (Exception exception)
            {
                await LoggingService.LogErrorAsync("Error occurred while processing data.", typeof(GenericRepository<TEntity>).ToString(), "Filter: " + filter?.ToString(), exception);
                throw;
            }
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            try
            {
                return await _dbSet.FindAsync(id);
            }
            catch (Exception exception)
            {
                await LoggingService.LogErrorAsync("Error occurred while processing data.", typeof(GenericRepository<TEntity>).ToString(), "Id: " + id, exception);
                throw;
            }
        }

        public async Task<TEntity> GetByIdAsNoTrackingAsync(int id)
        {
            try
            {
                var entity = await _dbSet.FindAsync(id);
                if (entity != null)
                {
                    _context.Entry(entity).State = EntityState.Detached;
                }
                return entity;
            }
            catch (Exception exception)
            {
                await LoggingService.LogErrorAsync("Error occurred while processing data.", typeof(GenericRepository<TEntity>).ToString(), "Id: " + id, exception);
                throw;
            }
        }

        public async Task AddAsync(TEntity entity)
        {
            try
            {
                await _dbSet.AddAsync(entity);
            }
            catch (Exception exception)
            {
                await LoggingService.LogErrorAsync("Error occurred while processing data.", typeof(GenericRepository<TEntity>).ToString(), entity.ToString(), exception);
                throw;
            }
        }

        public async Task UpdateAsync(TEntity entity)
        {
            try
            {
                var localEntity = await _dbSet.FindAsync(entity.GetType().GetProperty("Id")?.GetValue(entity));
                if (localEntity != null)
                {
                    _context.Entry(localEntity).State = EntityState.Detached;
                }
                _dbSet.Update(entity);
            }
            catch (Exception exception)
            {
                await LoggingService.LogErrorAsync("Error occurred while processing data.", typeof(GenericRepository<TEntity>).ToString(), entity.ToString(), exception);
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var entity = await GetByIdAsync(id);
                if (entity != null)
                {
                    _dbSet.Remove(entity);
                }
            }
            catch (Exception exception)
            {
                await LoggingService.LogErrorAsync("Error occurred while processing data.", typeof(GenericRepository<TEntity>).ToString(), "Id: " + id, exception);
                throw;
            }
        }
    }
}
