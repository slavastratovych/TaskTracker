using System.Collections.Generic;
using System.Threading.Tasks;
using TaskTracker.DomainLogic.Models;

namespace TaskTracker.DomainLogic.Items
{
    public abstract class ItemRepository
    {
        public abstract Task<IEnumerable<Item>> GetItemsByContextAsync(int contextID, string searchString, bool includeCompleted);

        public abstract Task AddItemAsync(Item item);

        public abstract Task<Item> GetItemAsync(int id);

        public abstract Task RemoveItemAsync(int id);

        public abstract Task UpdateItemAsync(Item item);
    }
}
