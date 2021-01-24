using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskTracker.DomainLogic.Items;
using TaskTracker.DomainLogic.Models;

namespace TaskTracker.SqlDataProvider.Repositories
{
    public class SqlItemRepository : ItemRepository
    {
        private readonly TaskTrackerContext _context;

        public SqlItemRepository(TaskTrackerContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public override async Task<IEnumerable<Item>> GetItemsByContextAsync(int contextId, string searchString, bool includeCompleted)
        {
            IQueryable<Item> items = _context.Item;

            items = items.Where(x => x.ContextId == contextId);

            if (!includeCompleted)
            {
                items = items.Where(i => i.IsCompleted == includeCompleted);
            }

            var result = await items.ToListAsync().ConfigureAwait(false);

            if (!string.IsNullOrEmpty(searchString))
            {
                return result.Where(i => i.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase));
            }

            return result;
        }

        public override async Task<Item> GetItemAsync(int id)
        {
            return await _context.Item
                .Include(x => x.Context)
                .FirstOrDefaultAsync(m => m.Id == id)
                .ConfigureAwait(false);
        }

        public override async Task AddItemAsync(Item item)
        {
            _context.Item.Add(item);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public override async Task RemoveItemAsync(int id)
        {
            var item = await _context.Item.FindAsync(id);

            if (item != null)
            {
                _context.Item.Remove(item);
                await _context.SaveChangesAsync().ConfigureAwait(false);
            }
        }

        public override async Task UpdateItemAsync(Item item)
        {
            _context.Attach(item).State = EntityState.Modified;
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
