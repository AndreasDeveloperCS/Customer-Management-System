using System.Data.Entity;
using CMS.Data.Access.Interfaces;

namespace CMS.Data.Access
{
    public static class DbSetExtensions
    {
        public static TEntity Update<TEntity>(this DbSet<TEntity> dbSet, DbContext context, TEntity entity)
            where TEntity : class, IEntity
        {
            var entityToUpdate = dbSet.FirstOrDefault(e => e.Id == entity.Id);
            if (entityToUpdate == null)
                throw new KeyNotFoundException("Cannot find entity");

            context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
            context.Entry(entityToUpdate).State = EntityState.Modified;
            return entity;
        }

        public static bool Remove<TEntity>(this DbSet<TEntity> dbSet, DbContext context, TEntity entity)
            where TEntity : class, IEntity
        {
            var entityToRemove = dbSet.FirstOrDefault(e => e.Id == entity.Id);
            if (entityToRemove == null)
                return false;

            context.Entry(entityToRemove).State = EntityState.Deleted;
            return true;
        }
    }
}
