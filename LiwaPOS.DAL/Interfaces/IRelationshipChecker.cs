namespace LiwaPOS.DAL.Interfaces
{
    public interface IRelationshipChecker
    {
        bool HasRelationship<TEntity>(int id) where TEntity : class;
    }
}
