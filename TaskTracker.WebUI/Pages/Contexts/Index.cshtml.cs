using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskTracker.DomainLogic.Contexts;
using TaskTracker.DomainLogic.Models;
using TaskTracker.WebUI.Authorization;
using TaskTracker.WebUI.Pages.Contexts;
using TaskTracker.WebUI.ViewModels;

namespace TaskTracker.WebUI
{
    public class IndexModel : BaseContextPageModel
    {
        public IndexModel(
            ContextRepository contextRepository,
            UserManager<IdentityUser> userManager,
            IAuthorizationService authorizationService)
            : base(contextRepository, userManager, authorizationService)
        {
        }

        public IList<ContextModel> ContextModel { get; private set; }

        public string UserId { get; private set; }

        public int SelectedContextId { get; private set; }

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool ShowCompleted { get; set; }

        public async Task<IActionResult> OnGetAsync(int? contextId)
        {
            UserId = UserManager.GetUserId(User);
            IList<Context> userContexts = await ContextRepository.GetContextsAsync(UserId);

            if (contextId.HasValue)
            {
                var selectedContext = await ContextRepository.GetContextAsync(contextId.Value);

                if (selectedContext == null)
                {
                    return NotFound();
                }

                var isAuthorized = await AuthorizationService.AuthorizeAsync(User, selectedContext, ProtectedOperations.AccessContext);

                if (!isAuthorized.Succeeded)
                {
                    return Forbid();
                }

                SelectedContextId = contextId.Value;
            }
            else
            {
                SelectedContextId = userContexts.First(x => x.IsDefault).Id;
            }

            ContextModel = userContexts.Select(x => x.ToViewModel()).ToList();

            return Page();
        }
    }
}
