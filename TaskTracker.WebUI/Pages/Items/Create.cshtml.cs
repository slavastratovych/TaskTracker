using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;
using TaskTracker.DomainLogic.Items;
using TaskTracker.WebUI.ViewModels;

namespace TaskTracker.WebUI.Pages.Items
{
    public class CreateModel : PageModel
    {
        private readonly ItemRepository _itemRepository;

        public CreateModel(ItemRepository itemRepository)
        {
            _itemRepository = itemRepository ?? throw new ArgumentNullException(nameof(itemRepository));
        }        

        [BindProperty]
        public ItemModel Item { get; set; }

        public IActionResult OnGet(int contextId)
        {
            Item = new ItemModel();
            Item.ContextId = contextId;

            return Page();
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Item.CreatedDate = DateTime.UtcNow;
            await _itemRepository.AddItemAsync(Item.ToDomainModel()).ConfigureAwait(false);

            return RedirectToPage("/Contexts/Index", new { contextId = Item.ContextId });
        }
    }
}
