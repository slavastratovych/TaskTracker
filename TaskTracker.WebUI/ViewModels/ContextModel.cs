using System;
using System.ComponentModel.DataAnnotations;
using TaskTracker.DomainLogic.Models;

namespace TaskTracker.WebUI.ViewModels
{
    public class ContextModel
    {
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; }

        public string UserId { get; set; }

        public bool IsDefault { get; set; }
    }

    public static class ContextModelExtensions
    {
        public static ContextModel ToViewModel(this Context item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return new ContextModel()
            {
                Id = item.Id,
                Name = item.Name,
                UserId = item.UserId,
                IsDefault = item.IsDefault
            };
        }

        public static Context ToDomainModel(this ContextModel viewModel)
        {
            if (viewModel is null)
            {
                throw new ArgumentNullException(nameof(viewModel));
            }

            return new Context()
            {
                Id = viewModel.Id,
                Name = viewModel.Name,
                UserId = viewModel.UserId,
                IsDefault = viewModel.IsDefault
            };
        }
    }
}
