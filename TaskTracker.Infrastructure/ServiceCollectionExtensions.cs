using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace TaskTracker.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static void AddPlugin(this IServiceCollection services, string pluginTypeName, IConfiguration config)
        {
            var pluginType = Type.GetType(pluginTypeName);
            var plugin = (IServiceCollectionPlugin)Activator.CreateInstance(pluginType);

            plugin.AddServices(services, config);
        }
    }
}
