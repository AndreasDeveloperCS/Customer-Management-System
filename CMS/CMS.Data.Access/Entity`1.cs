namespace CMS.Data.Access
{
    public abstract class Entity<TKey>
    {
        public abstract TKey Id { get; set; }

        public override string ToString()
        {
            return $"{base.ToString()},Id={Id}";
        }
    }
}