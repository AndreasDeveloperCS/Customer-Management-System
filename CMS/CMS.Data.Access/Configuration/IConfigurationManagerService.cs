using Microsoft.EntityFrameworkCore;

namespace CMS.Data.Access.Configuration
{
    public interface IConfigurationManagerService
    {
        T Get<T>(string key);
        T GetOrDefault<T>(string key, T defaultValue);
        string GetConnectionString(string name);
        DbContextOptions GetDbContextOptions(string name);
    }
}
