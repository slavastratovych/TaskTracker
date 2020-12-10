using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskTracker.DomainLogic.Items;
using TaskTracker.WebUI.ViewModels;

namespace TaskTracker.WebUI.ViewComponents
{
    public class ItemListViewComponent : ViewComponent
    {
        private readonly ItemRepository _itemRepository;

        public ItemListViewComponent(ItemRepository itemRepository)
        {
            _itemRepository = itemRepository ?? throw new System.ArgumentNullException(nameof(itemRepository));
        }

        public async Task<IViewComponentResult> InvokeAsync(int contextId, string searchString, bool showCompleted)
        {
            var items = await _itemRepository.GetItemsByContextAsync(contextId, searchString, showCompleted).ConfigureAwait(false);

            List<ItemModel> model = items.Select(x => x.ToViewModel()).ToList();

            return View(model);
        }
    }
}
