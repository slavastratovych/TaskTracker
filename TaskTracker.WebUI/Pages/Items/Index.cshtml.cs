using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskTracker.DomainLogic.Items;
using TaskTracker.WebUI.ViewModels;

namespace TaskTracker.WebUI.Pages.Items
{
    public class IndexModel : PageModel
    {
        private readonly ItemRepository _itemRepository;

        public IndexModel(ItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public IList<ItemModel> ItemModel { get; private set; }

        public int ContextID { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool ShowCompleted { get; set; }

        public async Task OnGetAsync(int contextID = 1)
        {
            var items = await _itemRepository.GetItemsByContextAsync(User, contextID, SearchString, ShowCompleted).ConfigureAwait(false);

            ItemModel = items.Select(x => x.ToViewModel()).ToList();
            ContextID = contextID;
        }
    }
}
