using LiwaPOS.Entities.Entities;
using System.Linq.Expressions;

namespace LiwaPOS.DAL.Interfaces
{
    public interface IRelationshipChecker
    {
        Task<bool> HasRelationshipAsync<TEntity>(int id, params Expression<Func<TEntity, object>>[] navigationProperties) where TEntity : class;
    }
}
