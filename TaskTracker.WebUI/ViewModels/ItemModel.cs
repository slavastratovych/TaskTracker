using System;
using System.ComponentModel.DataAnnotations;

namespace TaskTracker.WebUI.ViewModels
{
    public class ItemModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Display(Name = "Done")]
        public bool IsCompleted { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Due Date")]
        public DateTime? DueDate { get; set; }

        public int ContextId { get; set; }
    }
}
