using CMS.Data.Access;
using CMS.Data.Access.Commands;
using CMS.Data.Access.Commands.Interfaces;
using CMS.Data.Access.Configuration;
using CMS.Data.Access.Helpers;
using CMS.Data.Access.Interfaces;
using CMS.Data.Access.Queries;
using CMS.Data.Access.Queries.Interfaces;
using CMS.Data.Access.Repositories;
using CMS.Data.Access.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CMS.Services.Extensions
{
    public static class RegistrationHelper
    {
        public static void Add(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IConfigurationManagerService, ConfigurationManagerService>();

            services.AddDb(configuration);

            services.AddScoped<ICustomerCommandsRepository, CustomerCommandsRepository>();
            services.AddScoped<IOrderCommandsRepository, OrderCommandsRepository>();
            services.AddScoped<ICustomerQueriesRepository, CustomerQueriesRepository>();
            services.AddScoped<IOrderQueriesRepository, OrderQueriesRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IOrderQueries, OrderQueries>();
            services.AddScoped<IOrderCommands, OrderCommands>();


            services.AddScoped<ICustomerQueries, CustomerQueries>();
            services.AddScoped<ICustomerCommands, CustomerCommands>();

            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<ICustomerService, CustomerService>();
        }
    }
}
