using System.Windows;
using System.Windows.Media;

namespace TaskManager.Model.TaskPriorities
{
    public abstract class TaskPriorityBase : DependencyObject
    {
        public static readonly DependencyProperty ForegroundProperty;

        static TaskPriorityBase()
        {
            ForegroundProperty = DependencyProperty.Register(nameof(Foreground), typeof(SolidColorBrush), typeof(TaskPriorityBase));
        }

        public SolidColorBrush Foreground
        {
            get => (SolidColorBrush)GetValue(ForegroundProperty);
            set => SetValue(ForegroundProperty, value);
        }

        internal abstract int ID { get; }

        public abstract string DisplayName { get; }

        internal abstract void ResetForeground();
    }
}
