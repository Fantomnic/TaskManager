using System.Windows.Media;
using TaskManager.Helpers;

namespace TaskManager.Model.TaskStatuses
{
    /// <summary>Статус задачи "Завершено"</summary>
    public class CompletedStatus : TaskStatusBase
    {
        public CompletedStatus()
        {
            ResetBackground();
        }

        public override string DisplayName => "Завершено";

        internal override void ResetBackground()
        {
            Background = Helper.GetResource<SolidColorBrush>("completedStatusBackground");
        }
    }
}
