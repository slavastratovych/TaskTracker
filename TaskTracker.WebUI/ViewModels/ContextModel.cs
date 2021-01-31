using System.ComponentModel.DataAnnotations;

namespace TaskTracker.WebUI.ViewModels
{
    public class ContextModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public string UserId { get; set; }

        public bool IsDefault { get; set; }
    }
}
