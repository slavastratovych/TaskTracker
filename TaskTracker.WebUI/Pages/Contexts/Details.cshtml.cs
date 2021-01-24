using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TaskTracker.DomainLogic.Contexts;
using TaskTracker.WebUI.Authorization;
using TaskTracker.WebUI.Pages.Contexts;
using TaskTracker.WebUI.ViewModels;

namespace TaskTracker.WebUI
{
    public class DetailsModel : BaseContextPageModel
    {
        public DetailsModel(
            ContextRepository contextRepository,
            UserManager<IdentityUser> userManager,
            IAuthorizationService authorizationService)
            : base(contextRepository, userManager, authorizationService)
        {
        }

        public ContextModel Context { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var context = await ContextRepository.GetContextAsync(id.Value).ConfigureAwait(false);

            if (context == null)
            {
                return NotFound();
            }

            var isAuthorized = await AuthorizationService.AuthorizeAsync(User, context, ProtectedOperations.AccessContext);

            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            Context = context.ToViewModel();

            return Page();
        }
    }
}
