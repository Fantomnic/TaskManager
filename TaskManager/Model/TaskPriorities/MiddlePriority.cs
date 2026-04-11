using System.Windows.Media;
using TaskManager.Helpers;

namespace TaskManager.Model.TaskPriorities
{
    public class MiddlePriority : TaskPriorityBase
    {
        public MiddlePriority()
        {
            ResetForeground();
        }

        public override string DisplayName => "Средний";

        internal override void ResetForeground()
        {
            Foreground = Helper.GetResource<SolidColorBrush>("middlePriorityForeground");
        }
    }
}
