using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Threading.Tasks;
using TaskTracker.DomainLogic.Contexts;
using TaskTracker.DomainLogic.Items;
using TaskTracker.WebUI.ViewModels;

namespace TaskTracker.WebUI.Pages.Items
{
    public class CreateModel : PageModel
    {
        private readonly ItemRepository _itemRepository;
        private readonly ContextRepository _contextRepository;

        public CreateModel(ItemRepository itemRepository, ContextRepository contextRepository)
        {
            _itemRepository = itemRepository ?? throw new ArgumentNullException(nameof(itemRepository));
            _contextRepository = contextRepository ?? throw new ArgumentNullException(nameof(contextRepository));
        }        

        [BindProperty]
        public ItemModel ItemModel { get; set; }

        [BindProperty]
        public int ContextID { get; set; }

        public IActionResult OnGet(int contextID)
        {
            ContextID = contextID;

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

            ItemModel.CreatedDate = DateTime.UtcNow;
            await _itemRepository.AddItemAsync(ItemModel.ToDomainModel()).ConfigureAwait(false);

            return RedirectToPage("./Index", new { contextID = ContextID });
        }
    }
}
