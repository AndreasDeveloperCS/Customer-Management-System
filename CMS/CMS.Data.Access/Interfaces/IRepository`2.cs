namespace CMS.Data.Access.Interfaces
{
    public interface IRepository<TEntity, in TKey> : IRepository where TEntity : class, new()
    {
        IQueryable<TEntity> Get();
        ValueTask<TEntity> FindAsync(TKey key, CancellationToken cancellationToken);
        ValueTask<IEnumerable<TEntity>> FindRangeAsync(IEnumerable<TKey> keys, CancellationToken cancellationToken);
        TEntity Create(TEntity entity);
        IEnumerable<TEntity> CreateRange(IEnumerable<TEntity> entities);
        Task<TEntity> UpdateAsync(TKey key, TEntity entityToUpdate, CancellationToken cancellationToken);
        Task<TEntity> DeleteAsync(TKey key, CancellationToken cancellationToken);
        void Delete(TEntity entity);
        void DeleteRange(IEnumerable<TEntity> entities);
    }
}