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

        public override string DisplayName => "Ожидает принятия";

        internal override List<TaskStatusBase> Transitions => [TaskStatusesInstances.BeginingStatus, TaskStatusesInstances.RejectedStatus];

        internal override void ResetBackground()
        {
            Background = Helper.GetResource<SolidColorBrush>("WaitingStatusBackground");
        }
    }
}