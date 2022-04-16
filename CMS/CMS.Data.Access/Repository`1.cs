using CMS.Data.Access.Interfaces;
using System.Data.Entity;

namespace CMS.Data.Access
{
    public sealed class Repository<TEntity> : Repository<TEntity, int>, IRepository<TEntity> where TEntity : class, IEntity, new()
    {
        public Repository(EntitiesContext context) : base(context)
        {
        }
    }
}   
