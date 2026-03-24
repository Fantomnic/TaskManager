using System.Windows.Media;
using TaskManager.Helpers;

namespace TaskManager.Model.TaskStatuses
{
    /// <summary>Статус задачи "Отклонено"</summary>
    public class RejectedStatus : TaskStatusBase
    {
        public override string DisplayName => "Отклонено";

        internal override List<TaskStatusBase> Transitions => [TaskStatusesInstances.BeginingStatus];

        public override SolidColorBrush Background => Helper.GetResource<SolidColorBrush>("rejectedStatusBackground");
    }
}
