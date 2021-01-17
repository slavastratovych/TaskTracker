using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TaskTracker.DomainLogic.Contexts;
using TaskTracker.WebUI.Pages.Contexts;
using TaskTracker.WebUI.ViewModels;

namespace TaskTracker.WebUI
{
    public class CreateModel : BaseContextPageModel
    {
        public CreateModel(
            ContextRepository contextRepository,
            UserManager<IdentityUser> userManager,
            IAuthorizationService authorizationService)
            : base(contextRepository, userManager, authorizationService)
        {
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public ContextModel Context { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Context.UserId = UserManager.GetUserId(User);

            await ContextRepository
                .AddContextAsync(Context.ToDomainModel())
                .ConfigureAwait(false);

            return RedirectToPage("./Index");
        }
    }
}
