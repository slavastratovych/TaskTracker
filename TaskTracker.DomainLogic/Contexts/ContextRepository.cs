using System.Collections.Generic;
using System.Security.Principal;
using System.Threading.Tasks;
using TaskTracker.DomainLogic.Models;

namespace TaskTracker.DomainLogic.Contexts
{
    public abstract class ContextRepository
    {
        public abstract Task<IList<Context>> GetContextsAsync(IPrincipal user);

        public abstract Task<Context> GetContextAsync(int id);
    }
}
