using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskTracker.DomainLogic.Items;

namespace TaskTracker.WebUI.Pages.Items
{
    public class BaseItemPageModel : PageModel
    {
        public BaseItemPageModel(
            ItemRepository itemRepository,
            UserManager<IdentityUser> userManager,
            IAuthorizationService authorizationService)
        {
            ItemRepository = itemRepository;
            UserManager = userManager;
            AuthorizationService = authorizationService;
        }

        protected ItemRepository ItemRepository { get; }

        protected UserManager<IdentityUser> UserManager { get; }

        protected IAuthorizationService AuthorizationService { get; }
    }
}
