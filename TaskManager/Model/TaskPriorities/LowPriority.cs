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

        public override string DisplayName => "Низкий";

        internal override void ResetForeground()
        {
            Foreground = Helper.GetResource<SolidColorBrush>("LowPriorityForeground");
        }
    }
}
