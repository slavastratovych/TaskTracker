using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Threading.Tasks;
using TaskTracker.DomainLogic.Contexts;
using TaskTracker.DomainLogic.Items;
using TaskTracker.WebUI.Authorization;
using TaskTracker.WebUI.ViewModels;

namespace TaskTracker.WebUI.Pages.Items
{
    public class EditModel : BaseItemPageModel
    {
        private readonly ContextRepository _contextRepository;

        public EditModel(
            ItemRepository itemRepository,
            ContextRepository contextRepository,
            UserManager<IdentityUser> userManager,
            IAuthorizationService authorizationService)
            : base(itemRepository, userManager, authorizationService)
        {
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

            var item = await ItemRepository.GetItemAsync(id.Value).ConfigureAwait(false);

            if (item == null)
            {
                return NotFound();
            }

            var isAuthorized = await AuthorizationService.AuthorizeAsync(User, item.Context, ProtectedOperations.AccessContext);

            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            ItemModel = item.ToViewModel();

            string userId = UserManager.GetUserId(User);
            var contextList = await _contextRepository.GetContextsAsync(userId).ConfigureAwait(false);

            Contexts = new SelectList(contextList, "Id", "Name");

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

            var oldItem = await ItemRepository.GetItemAsync(ItemModel.Id).ConfigureAwait(false);

            if (oldItem == null)
            {
                return NotFound();
            }

            var isOldContexOwner = await AuthorizationService.AuthorizeAsync(User, oldItem.Context, ProtectedOperations.AccessContext);

            if (!isOldContexOwner.Succeeded)
            {
                return Forbid();
            }

            var newContext = await _contextRepository.GetContextAsync(ItemModel.ContextId).ConfigureAwait(false);

            var isNewContextOwner = await AuthorizationService.AuthorizeAsync(User, newContext, ProtectedOperations.AccessContext);

            if (!isNewContextOwner.Succeeded)
            {
                return Forbid();
            }

            oldItem.Name = ItemModel.Name;
            oldItem.IsCompleted = ItemModel.IsCompleted;
            oldItem.DueDate = ItemModel.DueDate;
            oldItem.ContextId = ItemModel.ContextId;

            await ItemRepository.UpdateItemAsync(oldItem).ConfigureAwait(false);

            return RedirectToPage("/Contexts/Index", new { contextId = ItemModel.ContextId });
        }
    }
}
