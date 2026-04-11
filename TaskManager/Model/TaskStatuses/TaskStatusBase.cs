using System.Windows;
using System.Windows.Media;

namespace TaskManager.Model.TaskStatuses
{
    /// <summary>Базовый класс статуса задачи</summary>
    public abstract class TaskStatusBase : DependencyObject
    {
        // Прим.: Чтобы цвет обновлялся автоматически при смене темы без вызова ResetBackground(),
        // нужно установить динамический ресурс для текстбокса
        // Также, если бы не было DependencyProperty, то при изменении цвета (темы) не происходило бы автообновление интерфейса
        public static readonly DependencyProperty BackgroundProperty;

        static TaskStatusBase()
        {
            BackgroundProperty = DependencyProperty.Register(nameof(Background), typeof(SolidColorBrush), typeof(TaskStatusBase));
        }

        public SolidColorBrush Background
        {
            get => (SolidColorBrush)GetValue(BackgroundProperty);
            set => SetValue(BackgroundProperty, value);
        }

        public abstract string DisplayName { get; }

        /// <summary>Статусы, в которые можно перейти из текущего статуса</summary>
        internal virtual List<TaskStatusBase> Transitions => [];

        internal bool HasAcceptCommandTransition() => Transitions.Contains(TaskStatusesInstances.BeginingStatus);

        internal bool HasRejectCommandTransition() => Transitions.Contains(TaskStatusesInstances.RejectedStatus);

        internal bool HasDeferCommandTransition() => Transitions.Contains(TaskStatusesInstances.DeferredStatus);

        internal bool HasDoneCommandTransition() => Transitions.Contains(TaskStatusesInstances.DoneStatus);

        internal bool HasCompleteCommandTransition() => Transitions.Contains(TaskStatusesInstances.CompletedStatus);

        internal abstract void ResetBackground();
    }
}
