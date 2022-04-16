using CMS.Data.Access.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CMS.Data.Access
{
    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class, new()
    {
        protected EntitiesContext _context;
        protected DbSet<TEntity> _set;

        public Repository(EntitiesContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _set = _context.Set<TEntity>();
        }

        public IQueryable<TEntity> Get()
        {
            return _set;
        }

        public async ValueTask<TEntity> FindAsync(TKey key, CancellationToken cancellationToken)
        {
            return await _set.FindAsync(cancellationToken, key);
        }

        public async ValueTask<IEnumerable<TEntity>> FindRangeAsync(IEnumerable<TKey> keys, CancellationToken cancellationToken)
        {
            if (keys == null)
            {
                throw new ArgumentNullException(nameof(keys));
            }

            LinkedList<TEntity> entities = new LinkedList<TEntity>();

            // TODO N+1 issue, Replace by RAW SQL, Dynamic LINQ or other solution
            foreach (TKey key in keys)
            {
                TEntity entity = await FindAsync(key, cancellationToken);

                if (entity != null)
                {
                    entities.AddLast(entity);
                }
            }

            return entities;
        }

        public TEntity Create(TEntity entity)
        {
            _set.Add(entity);
            return entity;
        }

        public IEnumerable<TEntity> CreateRange(IEnumerable<TEntity> entities)
        {
             _set.AddRange(entities);
            return entities;
        }
     
        public async Task<TEntity> UpdateAsync(TKey key, TEntity entity, CancellationToken cancellationToken)
        {
            TEntity entityToUpdate = await FindAsync(key, cancellationToken);

            if (entityToUpdate == null)
                throw new KeyNotFoundException($"There are no records with key: {key}");

            _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);

            return entityToUpdate;
        }

        public async Task<TEntity> DeleteAsync(TKey key, CancellationToken cancellationToken)
        {
            TEntity entityToDelete = await FindAsync(key, cancellationToken);

            if (entityToDelete != null)
                Delete(entityToDelete);

            return entityToDelete;
        }

        public void Delete(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            if (_context.Entry(entity).State == EntityState.Detached)
                _set.Attach(entity);

            _set.Remove(entity);
        }

        public void DeleteRange(IEnumerable<TEntity> entities)
        {
            foreach (TEntity entity in entities)
                Delete(entity);
        }
    }
}
