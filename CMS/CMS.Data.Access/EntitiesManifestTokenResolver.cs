using System.Data.Common;
using System.Data.Entity.Infrastructure;

namespace CMS.Data.Access
{
    public class EntitiesManifestTokenResolver : IManifestTokenResolver
    {
        public static IManifestTokenResolver Default { get; set; } = new DefaultManifestTokenResolver();

        public string ResolveManifestToken(DbConnection connection)
        {
            return Default.ResolveManifestToken(connection);
        }
    }
}