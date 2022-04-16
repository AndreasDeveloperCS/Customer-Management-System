namespace CMS.Data.Access.Interfaces
{
    public interface IUnitOfWork:IDisposable
    {
        IQueryable<TEntity> GetQuery<TEntity>() where TEntity : class;
        IRepository<TEntity> GetEntityRepository<TEntity>() where TEntity : class, IEntity, new();
        IRepository<TEntity, TKey> GetEntityRepository<TEntity, TKey>() where TEntity : Entity<TKey>, new();
        TIRepository GetRepository<TIRepository>() where TIRepository : IRepository;

        int Save();
        Task<int> SaveAsync(CancellationToken cancellationToken);
    }
}
