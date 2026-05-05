using System.Windows.Media;
using TaskManager.Helpers;

namespace TaskManager.Model.TaskPriorities
{
    public class PreHighPriority : TaskPriorityBase
    {
        public PreHighPriority()
        {
            ResetForeground();
        }

        internal override int ID => 4;

        public override string DisplayName => "Повышенный";

        internal override void ResetForeground()
        {
            Foreground = Helper.GetResource<SolidColorBrush>("PreHighPriorityForeground");
        }
    }
}
