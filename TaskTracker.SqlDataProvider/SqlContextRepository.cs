using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskTracker.DomainLogic.Contexts;
using TaskTracker.DomainLogic.Models;

namespace TaskTracker.SqlDataProvider
{
    public class SqlContextRepository : ContextRepository
    {
        private readonly TaskTrackerContext _context;

        public SqlContextRepository(TaskTrackerContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public override async Task<IList<Context>> GetContextsAsync(string userId)
        {
            return await _context.Context.Where(c => c.UserId == userId).ToListAsync().ConfigureAwait(false);
        }

        public override async Task<Context> GetContextAsync(int id)
        {
            return await _context.Context.FirstOrDefaultAsync(m => m.Id == id).ConfigureAwait(false);
        }

        public override async Task AddContextAsync(Context context)
        {
            _context.Context.Add(context);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public override async Task UpdateContextAsync(Context context)
        {
            _context.Attach(context).State = EntityState.Modified;
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public override async Task RemoveContextAsync(int id)
        {
            var context = await _context.Context.FindAsync(id);

            if (context != null)
            {
                _context.Context.Remove(context);
                await _context.SaveChangesAsync().ConfigureAwait(false);
            }
        }
    }
}
