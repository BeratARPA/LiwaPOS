using LiwaPOS.DAL.Interfaces;
using System.Linq.Expressions;

namespace LiwaPOS.DAL.Repositories
{
    public class RelationshipChecker : IRelationshipChecker
    {
        private readonly IServiceProvider _serviceProvider;

        public RelationshipChecker(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<bool> HasRelationshipAsync<TEntity>(int id, params Expression<Func<TEntity, object>>[] navigationProperties) where TEntity : class
        {
            // Repository çözümle
            var repositoryType = typeof(IGenericRepository<>).MakeGenericType(typeof(TEntity));
            var repository = _serviceProvider.GetService(repositoryType) as IGenericRepository<TEntity>;

            if (repository == null)
                throw new Exception($"Repository for {typeof(TEntity).Name} not found.");

            // İlgili varlığı getir
            var entity = await repository.GetByIdAsync(id);
            if (entity == null)
                return false;

            // Belirtilen tüm ilişkisel özellikleri kontrol et
            foreach (var navigationProperty in navigationProperties)
            {
                var compiledProperty = navigationProperty.Compile();
                var relatedValue = compiledProperty(entity);

                if (relatedValue != null)
                {
                    var relatedRepositoryType = typeof(IGenericRepository<>).MakeGenericType(relatedValue.GetType());
                    var relatedRepository = _serviceProvider.GetService(relatedRepositoryType) as IGenericRepository<object>;

                    if (relatedRepository != null)
                    {
                        //var exists = await relatedRepository.ExistsAsync(x => x.Equals(relatedValue));
                        //if (exists)
                            return true;
                    }
                }
            }

            return false;
        }
    }
}
