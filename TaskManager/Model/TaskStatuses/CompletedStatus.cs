using System.Windows.Media;
using TaskManager.Helpers;

namespace TaskManager.Model.TaskStatuses
{
    /// <summary>Статус задачи "Завершено"</summary>
    public class CompletedStatus : TaskStatusBase
    {
        public override string DisplayName => "Завершено";

        public override SolidColorBrush Background => Helper.GetResource<SolidColorBrush>("completedStatusBackground");
    }
}
