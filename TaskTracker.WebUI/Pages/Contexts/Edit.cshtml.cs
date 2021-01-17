using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TaskTracker.DomainLogic.Contexts;
using TaskTracker.DomainLogic.Models;
using TaskTracker.WebUI.Authorization;
using TaskTracker.WebUI.Pages.Contexts;

namespace TaskTracker.WebUI
{
    public class EditModel : BaseContextPageModel
    {
        public EditModel(
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

            var isAuthorized = await AuthorizationService.AuthorizeAsync(User, Context, Operations.AccessContext);

            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var isAuthorized = await AuthorizationService.AuthorizeAsync(User, Context, Operations.AccessContext);

            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            try
            {
                await ContextRepository.UpdateContextAsync(Context).ConfigureAwait(false);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ContextExists(Context.Id).ConfigureAwait(false))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index", new { contextId = Context.Id });
        }

        private async Task<bool> ContextExists(int id)
        {
            return await ContextRepository.GetContextAsync(id).ConfigureAwait(false) != null;
        }
    }
}
