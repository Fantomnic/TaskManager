using System.Windows.Media;
using TaskManager.Helpers;

namespace TaskManager.Model.TaskStatuses
{
    /// <summary>Статус задачи "Отложено"</summary>
    public class DeferredStatus : TaskStatusBase
    {
        public override string DisplayName => "Отложено";

        internal override List<TaskStatusBase> Transitions => [TaskStatusesInstances.BeginingStatus, TaskStatusesInstances.RejectedStatus];

        public override SolidColorBrush Background => Helper.GetResource<SolidColorBrush>("deferredStatusBackground");
    }
}
