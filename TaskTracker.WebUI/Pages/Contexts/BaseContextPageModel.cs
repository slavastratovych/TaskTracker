using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskTracker.DomainLogic.Contexts;

namespace TaskTracker.WebUI.Pages.Contexts
{
    public class BaseContextPageModel : PageModel
    {
        public BaseContextPageModel(
            ContextRepository contextRepository,
            UserManager<IdentityUser> userManager,
            IAuthorizationService authorizationService)
        {
            ContextRepository = contextRepository;
            UserManager = userManager;
            AuthorizationService = authorizationService;
        }

        protected ContextRepository ContextRepository { get; }

        protected UserManager<IdentityUser> UserManager { get; }

        protected IAuthorizationService AuthorizationService { get; }
    }
}
