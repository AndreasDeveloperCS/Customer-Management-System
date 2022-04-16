namespace CMS.Data.Access
{
    public class BaseRepository : IDisposable
    {
        protected EntitiesContext _context;

        public BaseRepository(EntitiesContext context)
        {
            _context = context;
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
                _context?.Dispose();
            }
        }
    }
}
