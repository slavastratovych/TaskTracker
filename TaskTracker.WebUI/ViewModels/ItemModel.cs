using System;
using System.ComponentModel.DataAnnotations;
using TaskTracker.DomainLogic.Models;

namespace TaskTracker.WebUI.ViewModels
{
    public class ItemModel
    {
        public int ID { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; }

        [Display(Name = "Done")]
        public bool IsCompleted { get; set; }

        [DataType(DataType.Date), Display(Name = "Date of creation")]
        public DateTime CreatedDate { get; set; }

        [DataType(DataType.Date), Display(Name = "Due date")]
        public DateTime? DueDate { get; set; }

        public int ContextID { get; set; }
    }

    public static class ItemModelExtensions
    {
        public static ItemModel ToViewModel(this Item item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return new ItemModel()
            {
                ID = item.ID,
                CreatedDate = item.CreatedDate,
                IsCompleted = item.IsCompleted,
                Name = item.Name,
                DueDate = item.DueDate,
                ContextID = item.ContextID
            };
        }

        public static Item ToDomainModel(this ItemModel viewModel)
        {
            if (viewModel is null)
            {
                throw new ArgumentNullException(nameof(viewModel));
            }

            return new Item()
            {
                ID = viewModel.ID,
                CreatedDate = viewModel.CreatedDate,
                IsCompleted = viewModel.IsCompleted,
                Name = viewModel.Name,
                DueDate = viewModel.DueDate,
                ContextID = viewModel.ContextID
            };
        }
    }
}
