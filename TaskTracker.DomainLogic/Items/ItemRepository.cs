using System.Collections.Generic;
using System.Security.Principal;
using System.Threading.Tasks;
using TaskTracker.DomainLogic.Models;

namespace TaskTracker.DomainLogic.Items
{
    public abstract class ItemRepository
    {
        public abstract Task<IList<Item>> GetItemsAsync(
            IPrincipal user, string searchString, bool includeCompleted);

        public abstract Task<IList<Item>> GetItemsByContextAsync(
            IPrincipal user, int contextID, string searchString, bool includeCompleted);

        public abstract Task AddItemAsync(Item item);

        public abstract Task<Item> GetItemAsync(int id);

        public abstract Task RemoveItemAsync(int id);

        public abstract Task UpdateItemAsync(Item item);
    }
}
