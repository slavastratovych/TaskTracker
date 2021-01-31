using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using TaskTracker.SqlDataProvider;

[assembly: HostingStartup(typeof(TaskTracker.WebUI.Areas.Identity.IdentityHostingStartup))]

namespace TaskTracker.WebUI.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.ConfigureServices((context, services) =>
            {
                services.AddDbContext<TaskTrackerContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("TaskTrackerContext")));

                services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<TaskTrackerContext>();
            });
        }
    }
}