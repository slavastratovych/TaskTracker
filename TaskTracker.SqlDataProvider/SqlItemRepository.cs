using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
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

        public override async Task<IList<Item>> GetItemsAsync(
            IPrincipal user, string searchString, bool includeCompleted)
        {
            IQueryable<Item> items = DoGetItems(searchString, includeCompleted);

            return await items.ToListAsync().ConfigureAwait(false);
        }

        public override async Task<IList<Item>> GetItemsByContextAsync(IPrincipal user, int contextID, string searchString, bool includeCompleted)
        {
            IQueryable<Item> items = DoGetItems(searchString, includeCompleted);

            items = items.Where(x => x.ContextID == contextID);

            return await items.ToListAsync().ConfigureAwait(false);
        }

        private IQueryable<Item> DoGetItems(string searchString, bool includeCompleted)
        {
            IQueryable<Item> items = _context.Item;

            if (!string.IsNullOrEmpty(searchString))
            {
                items = items.Where(i => i.Name.Contains(searchString));
            }

            if (!includeCompleted)
            {
                items = items.Where(i => i.IsCompleted == includeCompleted);
            }

            return items;
        }

        public override async Task<Item> GetItemAsync(int id)
        {
            return await _context.Item.FirstOrDefaultAsync(m => m.ID == id).ConfigureAwait(false);
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
