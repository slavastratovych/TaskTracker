using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using TaskTracker.DomainLogic.Contexts;
using TaskTracker.DomainLogic.Models;

namespace TaskTracker.WebUI
{
    public class DetailsModel : PageModel
    {
        private readonly ContextRepository _contextRepository;

        public DetailsModel(ContextRepository contextRepository)
        {
            this._contextRepository = contextRepository;
        }

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
    }
}
