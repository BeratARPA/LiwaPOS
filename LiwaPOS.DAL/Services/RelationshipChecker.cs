using LiwaPOS.DAL.Context;
using LiwaPOS.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LiwaPOS.DAL.Services
{
    public class RelationshipChecker : IRelationshipChecker
    {
        private readonly DataContext _context;

        public RelationshipChecker(DataContext context)
        {
            _context = context;
        }

        public bool HasRelationship<TEntity>(int id) where TEntity : class
        {
            var entityType = _context.Model.FindEntityType(typeof(TEntity));
            if (entityType == null)
                throw new Exception($"Entity type {typeof(TEntity).Name} bulunamadı!");

            // Foreign key içeren tüm tabloları bul
            var foreignKeys = _context.Model
                .GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys())
                .Where(fk => fk.PrincipalEntityType == entityType)
                .ToList();

            foreach (var fk in foreignKeys)
            {
                var dependentEntityType = fk.DeclaringEntityType.ClrType;
                var dependentTableName = fk.DeclaringEntityType.GetTableName();
                var foreignKeyProperty = fk.Properties.First(); // İlk FK alanı alınıyor

                // Dinamik olarak Set<TEntity>() çağır
                var setMethod = typeof(DbContext).GetMethod("Set", Type.EmptyTypes)!.MakeGenericMethod(dependentEntityType);
                var dbSet = setMethod.Invoke(_context, null);

                // IQueryable oluştur
                var param = Expression.Parameter(dependentEntityType, "x");
                var property = Expression.Property(param, foreignKeyProperty.Name);
                var constant = Expression.Constant(id);
                var equals = Expression.Equal(property, constant);
                var lambda = Expression.Lambda(equals, param);

                var anyMethod = typeof(Queryable)
                    .GetMethods()
                    .First(m => m.Name == "Any" && m.GetParameters().Length == 2)
                    .MakeGenericMethod(dependentEntityType);

                var query = anyMethod.Invoke(null, new object[] { dbSet, lambda });

                var result = (bool)query;

                if (result)
                {
                    Console.WriteLine($"{dependentTableName} tablosunda {typeof(TEntity).Name} kullanılıyor.");
                    return true; // Başka tabloda bulunduğu için true döndür.
                }
            }

            return false; // Hiçbir bağlı kayıt bulunamadı.
        }
    }
}
