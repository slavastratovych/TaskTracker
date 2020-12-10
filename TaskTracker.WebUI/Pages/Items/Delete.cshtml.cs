using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using TaskTracker.DomainLogic.Items;
using TaskTracker.WebUI.ViewModels;

namespace TaskTracker.WebUI.Pages.Items
{
    public class DeleteModel : PageModel
    {
        private readonly ItemRepository _itemRepository;

        public DeleteModel(ItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await _itemRepository.RemoveItemAsync(id.Value).ConfigureAwait(false);

            return RedirectToPage("/Contexts/Index", new { selectedID = ItemModel.ContextId });
        }
    }
}
