using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskTracker.DomainLogic.Contexts;
using TaskTracker.DomainLogic.Items;
using TaskTracker.Infrastructure;
using TaskTracker.SqlDataProvider.Repositories;

namespace TaskTracker.SqlDataProvider
{
    public class SqlDataProviderPlugin : IServiceCollectionPlugin
    {
        public void AddServices(IServiceCollection services, IConfiguration config)
        {
            // services.AddDbContext<TaskTrackerContext>(options =>
            // options.UseSqlServer(config.GetConnectionString(nameof(TaskTrackerContext))));
            services.AddScoped<ItemRepository, SqlItemRepository>();
            services.AddScoped<ContextRepository, SqlContextRepository>();
        }
    }
}
