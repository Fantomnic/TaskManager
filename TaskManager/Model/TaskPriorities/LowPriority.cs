using System.Windows.Media;
using TaskManager.Helpers;

namespace TaskManager.Model.TaskPriorities
{
    public class LowPriority : TaskPriorityBase
    {
        public LowPriority()
        {
            ResetForeground();
        }

        internal override int ID => 1;

        public override string DisplayName => Settings.PrioritiesSetID == 0 ? "Низкий" : "Минимальный";

        internal override void ResetForeground()
        {
            Foreground = Helper.GetResource<SolidColorBrush>("LowPriorityForeground");
        }
    }
}
