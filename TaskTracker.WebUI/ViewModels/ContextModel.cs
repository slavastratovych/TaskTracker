using TaskTracker.DomainLogic.Models;

namespace TaskTracker.WebUI.ViewModels
{
    public class ContextModel
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public bool IsSelected { get; set; }
    }

    public static class ContextModelExtensions
    {
        public static ContextModel ToViewModel(this Context item)
        {
            if (item is null)
            {
                throw new System.ArgumentNullException(nameof(item));
            }

            return new ContextModel()
            {
                ID = item.ID,
                Name = item.Name
            };
        }
    }
}
