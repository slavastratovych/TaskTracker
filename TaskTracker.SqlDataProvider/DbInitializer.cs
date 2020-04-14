using Microsoft.EntityFrameworkCore;
using System.Linq;
using TaskTracker.DomainLogic.Models;

namespace TaskTracker.SqlDataProvider
{
    public static class DbInitializer
    {
        public static void Initialize(TaskTrackerContext dbContext)
        {
            if (dbContext is null)
            {
                throw new System.ArgumentNullException(nameof(dbContext));
            }

            dbContext.Database.Migrate();

            if (dbContext.Context.Any())
            {
                return;
            }

            var contexts = new Context[]
            {
                new Context { Name="Home" },
                new Context { Name="Work" }
            };

            foreach (var context in contexts)
            {
                dbContext.Context.Add(context);
            }

            dbContext.SaveChanges();
        }
    }
}
