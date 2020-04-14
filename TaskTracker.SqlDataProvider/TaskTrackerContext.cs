using Microsoft.EntityFrameworkCore;
using TaskTracker.DomainLogic.Models;

namespace TaskTracker.SqlDataProvider
{
    public class TaskTrackerContext : DbContext
    {
        public TaskTrackerContext(DbContextOptions<TaskTrackerContext> options)
            : base(options)
        {
        }

        public DbSet<Item> Item { get; set; }

        public DbSet<Context> Context { get; set; }
    }
}
