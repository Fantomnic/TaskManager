using System.Windows.Media;
using TaskManager.Helpers;

namespace TaskManager.Model.TaskPriorities
{
    public class PostLowPriority : TaskPriorityBase
    {
        public PostLowPriority()
        {
            ResetForeground();
        }

        internal override int ID => 2;

        public override string DisplayName => "Пониженный";

        internal override void ResetForeground()
        {
            Foreground = Helper.GetResource<SolidColorBrush>("PostLowPriorityForeground");
        }
    }
}
