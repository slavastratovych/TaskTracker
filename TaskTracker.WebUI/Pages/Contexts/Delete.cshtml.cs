using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TaskTracker.DomainLogic.Contexts;
using TaskTracker.DomainLogic.Models;
using TaskTracker.WebUI.Authorization;
using TaskTracker.WebUI.Pages.Contexts;

namespace TaskTracker.WebUI
{
    public class DeleteModel : BaseContextPageModel
    {
        public DeleteModel(
            ContextRepository contextRepository,
            UserManager<IdentityUser> userManager,
            IAuthorizationService authorizationService)
            : base(contextRepository, userManager, authorizationService)
        {
        }

        [BindProperty]
        public Context Context { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Context = await ContextRepository.GetContextAsync(id.Value).ConfigureAwait(false);

            if (Context == null)
            {
                return NotFound();
            }

            var isAuthorized = await AuthorizationService.AuthorizeAsync(User, Context, ProtectedOperations.AccessContext);

            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Context = await ContextRepository.GetContextAsync(id.Value).ConfigureAwait(false);

            var isAuthorized = await AuthorizationService.AuthorizeAsync(User, Context, ProtectedOperations.AccessContext);

            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            await ContextRepository.RemoveContextAsync(id.Value).ConfigureAwait(false);

            return RedirectToPage("./Index");
        }
    }
}
