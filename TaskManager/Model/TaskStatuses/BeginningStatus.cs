using System.Windows.Media;
using TaskManager.Helpers;

namespace TaskManager.Model.TaskStatuses
{
    /// <summary>Статус задачи "Текущее"</summary>
    public class BeginningStatus : TaskStatusBase
    {
        public BeginningStatus()
        {
            ResetBackground();
            TaskVisible = true;
        }

        public override string DisplayName => "Текущее";

        internal override List<TaskStatusBase> Transitions => [TaskStatusesInstances.DeferredStatus, TaskStatusesInstances.DoneStatus, TaskStatusesInstances.CompletedStatus];

        internal override void ResetBackground()
        {
            Background = Helper.GetResource<SolidColorBrush>("BeginningStatusBackground");
        }
    }
}
