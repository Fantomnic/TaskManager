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

        internal override int ID => 2;

        public override string DisplayName => "Средний";

        internal override void ResetForeground()
        {
            Foreground = Helper.GetResource<SolidColorBrush>("MiddlePriorityForeground");
        }
    }
}
