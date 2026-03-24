using System.Windows.Media;
using TaskManager.Helpers;

namespace TaskManager.Model.TaskStatuses
{
    /// <summary>Статус задачи "Текущее"</summary>
    public class BeginningStatus : TaskStatusBase
    {
        public override string DisplayName => "Текущее";

        internal override List<TaskStatusBase> Transitions => [TaskStatusesInstances.DeferredStatus, TaskStatusesInstances.DoneStatus, TaskStatusesInstances.CompletedStatus];

        public override SolidColorBrush Background => Helper.GetResource<SolidColorBrush>("beginningStatusBackground");
    }
}
