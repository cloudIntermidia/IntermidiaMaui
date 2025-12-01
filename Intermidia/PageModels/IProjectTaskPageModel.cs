using CommunityToolkit.Mvvm.Input;
using Intermidia.Models;

namespace Intermidia.PageModels
{
    public interface IProjectTaskPageModel
    {
        IAsyncRelayCommand<ProjectTask> NavigateToTaskCommand { get; }
        bool IsBusy { get; }
    }
}