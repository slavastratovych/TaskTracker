using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TaskTracker.DomainLogic.Contexts;
using TaskTracker.DomainLogic.Models;

namespace TaskTracker.WebUI
{
    public class EditModel : PageModel
    {
        private readonly ContextRepository _contextRepository;

        public EditModel(ContextRepository contextRepository)
        {
            _contextRepository = contextRepository;
        }

        [BindProperty]
        public Context Context { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Context = await _contextRepository.GetContextAsync(id.Value).ConfigureAwait(false);

            if (Context == null)
            {
                return NotFound();
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

            try
            {
                await _contextRepository.UpdateContextAsync(Context).ConfigureAwait(false);
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

            return RedirectToPage("./Index", new { selectedID = Context.Id });
        }

        private async Task<bool> ContextExists(int id)
        {
            return await _contextRepository.GetContextAsync(id).ConfigureAwait(false) != null;
        }
    }
}
