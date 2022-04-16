namespace CMS.Data.Access.Interfaces
{
    public interface IRepository<TEntity> : IRepository<TEntity, int> where TEntity : class, IEntity, new()
    {
    }
}
