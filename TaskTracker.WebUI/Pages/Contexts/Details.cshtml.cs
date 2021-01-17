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
    public class DetailsModel : BaseContextPageModel
    {
        public DetailsModel(
            ContextRepository contextRepository,
            UserManager<IdentityUser> userManager,
            IAuthorizationService authorizationService)
            : base(contextRepository, userManager, authorizationService)
        {
        }

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

            var isAuthorized = await AuthorizationService.AuthorizeAsync(User, Context, Operations.AccessContext);

            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            return Page();
        }
    }
}
