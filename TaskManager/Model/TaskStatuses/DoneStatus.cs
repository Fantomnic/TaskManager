using System.Windows.Media;
using TaskManager.Helpers;

namespace TaskManager.Model.TaskStatuses
{
    /// <summary>Статус задачи "Выполнено"</summary>
    public class DoneStatus : TaskStatusBase
    {
        public DoneStatus()
        {
            ResetBackground();
        }

        public override string DisplayName => "Выполнено";

        internal override List<TaskStatusBase> Transitions => [TaskStatusesInstances.BeginingStatus];

        internal override void ResetBackground()
        {
            Background = Helper.GetResource<SolidColorBrush>("DoneStatusBackground");
        }
    }
}
