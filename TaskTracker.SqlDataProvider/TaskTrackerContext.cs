using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskTracker.DomainLogic.Models;

namespace TaskTracker.SqlDataProvider
{
    public class TaskTrackerContext : IdentityDbContext<IdentityUser>
    {
        public TaskTrackerContext(DbContextOptions<TaskTrackerContext> options)
            : base(options)
        {
        }

        public DbSet<Item> Item { get; set; }

        public DbSet<Context> Context { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
