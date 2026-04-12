using System.Windows.Media;
using TaskManager.Helpers;

namespace TaskManager.Model.TaskStatuses
{
    /// <summary>Статус задачи "Отложено"</summary>
    public class DeferredStatus : TaskStatusBase
    {
        public DeferredStatus()
        {
            ResetBackground();
        }

        public override string DisplayName => "Отложено";

        internal override List<TaskStatusBase> Transitions => [TaskStatusesInstances.BeginingStatus, TaskStatusesInstances.RejectedStatus];

        internal override void ResetBackground()
        {
            Background = Helper.GetResource<SolidColorBrush>("DeferredStatusBackground");
        }
    }
}
