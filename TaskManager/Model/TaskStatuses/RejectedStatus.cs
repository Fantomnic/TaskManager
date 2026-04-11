using System.Windows.Media;
using TaskManager.Helpers;

namespace TaskManager.Model.TaskStatuses
{
    /// <summary>Статус задачи "Отклонено"</summary>
    public class RejectedStatus : TaskStatusBase
    {
        public RejectedStatus()
        {
            ResetBackground();
        }

        public override string DisplayName => "Отклонено";

        internal override List<TaskStatusBase> Transitions => [TaskStatusesInstances.BeginingStatus];

        internal override void ResetBackground()
        {
            Background = Helper.GetResource<SolidColorBrush>("rejectedStatusBackground");
        }
    }
}
