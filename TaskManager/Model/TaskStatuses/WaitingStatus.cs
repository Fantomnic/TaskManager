using System.Windows.Media;
using TaskManager.Helpers;

namespace TaskManager.Model.TaskStatuses
{
    /// <summary>Статус задачи "Ожидает принятия"</summary>
    public class WaitingStatus : TaskStatusBase
    {
        public WaitingStatus()
        {
            ResetBackground();
            TaskVisible = true;
        }

        internal override int ID => 1;

        public override string DisplayName => "Ожидает принятия";

        public override bool CalendarIsEnabled => true;

        internal override List<TaskStatusBase> Transitions => [TaskStatusesInstances.BeginingStatus, TaskStatusesInstances.RejectedStatus];

        internal override void ResetBackground()
        {
            Background = Helper.GetResource<SolidColorBrush>("WaitingStatusBackground");
        }
    }
}