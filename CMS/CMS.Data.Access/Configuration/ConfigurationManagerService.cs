using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using System.Globalization;

namespace CMS.Data.Access.Configuration
{
    public class ConfigurationManagerService : IConfigurationManagerService
    {
        private readonly IConfiguration _configuration;

        public ConfigurationManagerService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public T Get<T>(string key)
        {
            if (TryGet<T>(key, out var value))
            {
                return value;
            }
            throw new ConfigurationErrorsException($"\"{key}\" application setting not found.");
        }

        public T GetOrDefault<T>(string key, T defaultValue)
        {
            return TryGet(key, out T value) ? value : defaultValue;
        }

        private bool TryGet<T>(string key, out T value)
        {
            string stringValue = _configuration.GetSection("AppSettings")[key];
            if (stringValue != null)
            {
                value = (T)Convert.ChangeType(stringValue, typeof(T), CultureInfo.InvariantCulture);
                return true;
            }
            value = default(T);
            return false;
        }

        public string GetConnectionString(string name)
        {
            return _configuration.GetConnectionString(name);
        }

        public DbContextOptions GetDbContextOptions(string name)
        {
            return GetOptions(_configuration.GetConnectionString(name));
        }

        private static DbContextOptions GetOptions(string connectionString)
        {
            var dbContextOptions = SqlServerDbContextOptionsExtensions
                        .UseSqlServer(new DbContextOptionsBuilder(), connectionString)
                        .EnableSensitiveDataLogging(sensitiveDataLoggingEnabled: true);

            return dbContextOptions.Options;
        }
    }
}
