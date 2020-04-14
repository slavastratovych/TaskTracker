using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaskTracker.DomainLogic.Models
{
    public class Context
    {
        public int ID { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; }

        public ICollection<Item> Items { get; set; }
    }
}
