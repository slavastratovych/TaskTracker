using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TaskTracker.DomainLogic.Contexts;
using TaskTracker.DomainLogic.Items;
using TaskTracker.WebUI.Authorization;
using TaskTracker.WebUI.ViewModels;

namespace TaskTracker.WebUI.Pages.Items
{
    public class CreateModel : BaseItemPageModel
    {
        private readonly ContextRepository _contextRepository;

        public CreateModel(
            ItemRepository itemRepository,
            ContextRepository contextRepository,
            UserManager<IdentityUser> userManager,
            IAuthorizationService authorizationService)
            : base(itemRepository, userManager, authorizationService)
        {
            _contextRepository = contextRepository ?? throw new ArgumentNullException(nameof(contextRepository));
        }

        [BindProperty]
        public ItemModel Item { get; set; }

        public async Task<IActionResult> OnGet(int contextId)
        {
            var context = await _contextRepository.GetContextAsync(contextId).ConfigureAwait(false);

            if (context == null)
            {
                return NotFound();
            }

            var isAuthorized = await AuthorizationService.AuthorizeAsync(User, context, ProtectedOperations.AccessContext);

            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

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

            var context = await _contextRepository.GetContextAsync(Item.ContextId).ConfigureAwait(false);

            if (context == null)
            {
                return NotFound();
            }

            var isAuthorized = await AuthorizationService.AuthorizeAsync(User, context, ProtectedOperations.AccessContext);

            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            Item.CreatedDate = DateTime.UtcNow;
            await ItemRepository.AddItemAsync(Item.ToDomainModel()).ConfigureAwait(false);

            return RedirectToPage("/Contexts/Index", new { contextId = Item.ContextId });
        }
    }
}
