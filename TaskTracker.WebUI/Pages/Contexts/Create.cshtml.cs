using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using TaskTracker.DomainLogic.Contexts;
using TaskTracker.WebUI.ViewModels;

namespace TaskTracker.WebUI
{
    public class CreateModel : PageModel
    {
        private readonly ContextRepository _contextRepository;
        private readonly UserManager<IdentityUser> _userManager;

        public CreateModel(ContextRepository contextRepository, UserManager<IdentityUser> userManager)
        {
            _contextRepository = contextRepository;
            _userManager = userManager;
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

            Context.UserId = _userManager.GetUserId(User);

            await _contextRepository
                .AddContextAsync(Context.ToDomainModel())
                .ConfigureAwait(false);

            return RedirectToPage("./Index");
        }
    }
}
