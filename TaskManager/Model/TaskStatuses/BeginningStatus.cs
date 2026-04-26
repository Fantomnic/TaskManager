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
        }

        internal override int ID => 3;

        public override string DisplayName => "Текущее";

        public override bool CalendarIsEnabled => true;

        internal override List<TaskStatusBase> Transitions => [TaskStatusesInstances.DeferredStatus, TaskStatusesInstances.DoneStatus, TaskStatusesInstances.CompletedStatus];

        internal override void ResetBackground()
        {
            Background = Helper.GetResource<SolidColorBrush>("BeginningStatusBackground");
        }
    }
}
