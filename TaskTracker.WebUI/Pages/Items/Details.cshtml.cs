using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TaskTracker.DomainLogic.Items;
using TaskTracker.WebUI.Authorization;
using TaskTracker.WebUI.ViewModels;

namespace TaskTracker.WebUI.Pages.Items
{
    public class DetailsModel : BaseItemPageModel
    {
        public DetailsModel(
            ItemRepository itemRepository,
            UserManager<IdentityUser> userManager,
            IAuthorizationService authorizationService)
            : base(itemRepository, userManager, authorizationService)
        {
        }

        public ItemModel ItemModel { get; set; }

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

            return Page();
        }
    }
}
