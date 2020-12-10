using System;

namespace TaskTracker.DomainLogic.Models
{
    public class Item
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsCompleted { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? DueDate { get; set; }

        public int ContextId { get; set; }

        public Context Context { get; set; }
    }
}
