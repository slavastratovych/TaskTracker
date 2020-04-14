using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TaskTracker.Infrastructure
{
    public interface IServiceCollectionPlugin
    {
        void AddServices(IServiceCollection services, IConfiguration config);
    }
}
