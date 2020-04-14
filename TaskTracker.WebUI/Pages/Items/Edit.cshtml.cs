using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Threading.Tasks;
using TaskTracker.DomainLogic.Contexts;
using TaskTracker.DomainLogic.Items;
using TaskTracker.DomainLogic.Models;
using TaskTracker.WebUI.ViewModels;

namespace TaskTracker.WebUI.Pages.Items
{
    public class EditModel : PageModel
    {
        private readonly ItemRepository _itemRepository;
        private readonly ContextRepository _contextRepository;

        public EditModel(ItemRepository itemRepository, ContextRepository contextRepository)
        {
            _itemRepository = itemRepository ?? throw new ArgumentNullException(nameof(itemRepository));
            _contextRepository = contextRepository ?? throw new ArgumentNullException(nameof(contextRepository));
        }

        [BindProperty]
        public ItemModel ItemModel { get; set; }

        public SelectList Contexts { get; set; }

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
            var contextList = await _contextRepository.GetContextsAsync(User).ConfigureAwait(false);
            Contexts = new SelectList(contextList, "ID", "Name");

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

            try
            {
                await _itemRepository.UpdateItemAsync(ItemModel.ToDomainModel()).ConfigureAwait(false);
            }
            catch (Exception)
            {
                if (!ItemExists(ItemModel.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ItemExists(int id)
        {
            return _itemRepository.GetItemAsync(id) != null;
        }
    }
}
