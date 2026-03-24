using System.Windows.Media;
using TaskManager.Helpers;

namespace TaskManager.Model.TaskStatuses
{
    /// <summary>Статус задачи "Выполнено"</summary>
    public class DoneStatus : TaskStatusBase
    {
        public override string DisplayName => "Выполнено";

        internal override List<TaskStatusBase> Transitions => [TaskStatusesInstances.BeginingStatus];

        public override SolidColorBrush Background => Helper.GetResource<SolidColorBrush>("doneStatusBackground");
    }
}
