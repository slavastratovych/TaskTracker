using System;

namespace TaskTracker.DomainLogic.Models
{
    public class Item
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public bool IsCompleted { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? DueDate { get; set; }

        public int ContextID { get; set; }

        public Context Context { get; set; }
    }
}
