using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskTracker.DomainLogic.Contexts;
using TaskTracker.DomainLogic.Models;
using TaskTracker.WebUI.ViewModels;

namespace TaskTracker.WebUI
{
    public class IndexModel : PageModel
    {
        private readonly ContextRepository _contextRepository;
        private readonly UserManager<IdentityUser> _userManager;

        public IndexModel(ContextRepository contextRepository, UserManager<IdentityUser> userManager)
        {
            _contextRepository = contextRepository;
            _userManager = userManager;
        }

        public IList<ContextModel> ContextModel { get; private set; }

        public string UserId { get; private set; }

        public int SelectedId { get; private set; }

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool ShowCompleted { get; set; }

        public async Task OnGetAsync(int? selectedID)
        {
            UserId = _userManager.GetUserId(User);
            IList<Context> contexts = await _contextRepository.GetContextsAsync(UserId).ConfigureAwait(false);

            if (selectedID.HasValue && contexts.Any(x => x.Id == selectedID))
            {
                SelectedId = selectedID.Value;
            }
            else
            {
                SelectedId = contexts.First(x => x.IsDefault).Id;
            }

            ContextModel = contexts.Select(x => x.ToViewModel()).ToList();
        }
    }
}
