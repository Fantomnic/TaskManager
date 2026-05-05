using System.Windows.Media;
using TaskManager.Helpers;

namespace TaskManager.Model.TaskPriorities
{
    public class HighPriority : TaskPriorityBase
    {
        public HighPriority()
        {
            ResetForeground();
        }

        internal override int ID => 5;

        public override string DisplayName => Settings.PrioritiesSetID == 0 ? "Высокий" : "Максимальный";

        internal override void ResetForeground()
        {
            Foreground = Helper.GetResource<SolidColorBrush>("HighPriorityForeground");
        }
    }
}
