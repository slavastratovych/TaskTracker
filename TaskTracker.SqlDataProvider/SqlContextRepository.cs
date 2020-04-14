using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.Principal;
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

        public override async Task<IList<Context>> GetContextsAsync(IPrincipal user)
        {
            return await _context.Context.ToListAsync().ConfigureAwait(false);
        }

        public override async Task<Context> GetContextAsync(int id)
        {
            return await _context.Context.FirstOrDefaultAsync(m => m.ID == id).ConfigureAwait(false);
        }
    }
}
