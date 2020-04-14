using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using TaskTracker.DomainLogic.Items;
using TaskTracker.WebUI.ViewModels;

namespace TaskTracker.WebUI.Pages.Items
{
    public class DetailsModel : PageModel
    {
        private readonly ItemRepository _itemRepository;

        public DetailsModel(ItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public ItemModel ItemModel { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _itemRepository.GetItemAsync(id.Value).ConfigureAwait(false);

            if (item == null)
            {
                return NotFound();
            }

            ItemModel = item.ToViewModel();

            return Page();
        }
    }
}
