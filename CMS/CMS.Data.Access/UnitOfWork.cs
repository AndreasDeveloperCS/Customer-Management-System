using CMS.Data.Access.Interfaces;
using CMS.Data.Access.Repositories;
using CMS.Data.Access.Repositories.Interfaces;

namespace CMS.Data.Access
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly EntitiesContext _context;

        private readonly IDictionary<Type, Func<object>> _repositoriesFactory;

       
        public UnitOfWork(EntitiesContext context)
        {
            _context = context;

            _repositoriesFactory = InitializeRepositoriesFactory();
        }

        public IQueryable<TEntity> GetQuery<TEntity>() where TEntity : class
        {
            return _context.Set<TEntity>();
        }

        public IRepository<TEntity> GetEntityRepository<TEntity>() where TEntity : class, IEntity, new()
        {
            return new Repository<TEntity>(_context);
        }

        public IRepository<TEntity, TKey> GetEntityRepository<TEntity, TKey>() where TEntity : Entity<TKey>, new()
        {
            return new Repository<TEntity, TKey>(_context);
        }

        public TIRepository GetRepository<TIRepository>() where TIRepository : IRepository
        {
            return (TIRepository)_repositoriesFactory[typeof(TIRepository)].Invoke();
        }
        public virtual int Save()
        {
            if (_context.ChangeTracker.HasChanges())
                return _context.SaveChanges();

            return 0;
        }

        public virtual Task<int> SaveAsync(CancellationToken cancellationToken)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }

        private IDictionary<Type, Func<object>> InitializeRepositoriesFactory()
        {
            return new Dictionary<Type, Func<object>>()
            {
                [typeof(ICustomerCommandsRepository)] = () => new CustomerCommandsRepository(_context),
                [typeof(IOrderCommandsRepository)] = () => new OrderCommandsRepository(_context),
                [typeof(ICustomerQueriesRepository )] = () => new CustomerQueriesRepository(_context),
                [typeof(IOrderQueriesRepository )] = () => new OrderQueriesRepository(_context)
            };
        }
    }
}
