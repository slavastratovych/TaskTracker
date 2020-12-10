using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaskTracker.DomainLogic.Models
{
    public class Context
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsDefault { get; set; }

        public ICollection<Item> Items { get; set; }

        public string UserId { get; set; }

        public IdentityUser User { get; set; }
    }
}
