using System.Data.Entity;

namespace CMS.Data.Access
{
    public class EntitiesDbConfiguration : DbConfiguration
    {
        public EntitiesDbConfiguration()
        {
            SetManifestTokenResolver(new EntitiesManifestTokenResolver());
        }
    }
}