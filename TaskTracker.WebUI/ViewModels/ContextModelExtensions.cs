using System;
using TaskTracker.DomainLogic.Models;

namespace TaskTracker.WebUI.ViewModels
{
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
                IsDefault = item.IsDefault,
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
                IsDefault = viewModel.IsDefault,
            };
        }
    }
}
