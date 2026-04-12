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

        public override string DisplayName => "Высокий";

        internal override void ResetForeground()
        {
            Foreground = Helper.GetResource<SolidColorBrush>("HighPriorityForeground");
        }
    }
}
