using System.Collections.Generic;
using System.Threading.Tasks;
using TaskTracker.DomainLogic.Models;

namespace TaskTracker.DomainLogic.Contexts
{
    public abstract class ContextRepository
    {
        public abstract Task<IList<Context>> GetContextsAsync(string userId);

        public abstract Task<Context> GetContextAsync(int id);

        public abstract Task AddContextAsync(Context context);

        public abstract Task RemoveContextAsync(int id);

        public abstract Task UpdateContextAsync(Context context);
    }
}
