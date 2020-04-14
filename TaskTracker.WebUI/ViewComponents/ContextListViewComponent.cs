using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskTracker.DomainLogic.Contexts;
using TaskTracker.DomainLogic.Models;
using TaskTracker.WebUI.ViewModels;

namespace TaskTracker.WebUI.ViewComponents
{
    public class ContextListViewComponent : ViewComponent
    {
        private readonly ContextRepository _contextRepository;

        public ContextListViewComponent(ContextRepository contextRepository)
        {
            _contextRepository = contextRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync(int selectedID)
        {
            IList<Context> contexts = await _contextRepository.GetContextsAsync(User).ConfigureAwait(false);

            List<ContextModel> model = contexts.Select(x => x.ToViewModel()).ToList();
            ContextModel selectedItem = model.First(x => x.ID == selectedID);
            selectedItem.IsSelected = true;

            return View(model);
        }
    }
}
