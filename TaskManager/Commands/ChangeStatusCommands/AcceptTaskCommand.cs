using TaskManager.Helpers;
using TaskManager.Model;
using TaskManager.Model.TaskStatuses;
using TaskManager.ViewModels;

namespace TaskManager.Commands
{
    public class AcceptTaskCommand : ChangeTaskStatusCommand
    {
        private protected override TaskStatusBase _targetStatus => TaskStatusesInstances.BeginingStatus;

        internal override bool CanChange(TaskObject taskObject) => taskObject.Status.HasAcceptCommandTransition();

        internal override void ExecuteImplement(object? parameter)
        {
            if (parameter is not TaskObjectViewModel taskObjectViewModel || taskObjectViewModel.TaskObject is not TaskObject taskObject)
                return;

            var sourceStatus = taskObject.Status;

            if (!ExecuteImplementCore(taskObjectViewModel, taskObject) || sourceStatus == TaskStatusesInstances.WaitingStatus || sourceStatus == TaskStatusesInstances.DeferredStatus)
                return;

            var currentSection = Helper.MainViewModel.SelectedSectionViewModel;
            taskObjectViewModel.ResetEndDate(currentSection.DefaultReleaseDays);
        }
    }
}
