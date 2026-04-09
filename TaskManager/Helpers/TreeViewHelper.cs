using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace TaskManager.Helpers
{
    public static class TreeViewHelper
    {
        private static TreeViewItem? _currentItem = null;

        static TreeViewHelper()
        {
            // Get all Mouse enter/leave events for TreeViewItem.
            EventManager.RegisterClassHandler(typeof(TreeViewItem), TreeViewItem.MouseEnterEvent, new MouseEventHandler(OnMouseTransition), true);
            EventManager.RegisterClassHandler(typeof(TreeViewItem), TreeViewItem.MouseLeaveEvent, new MouseEventHandler(OnMouseTransition), true);

            // Listen for the UpdateOverItemEvent on all TreeViewItem's.
            EventManager.RegisterClassHandler(typeof(TreeViewItem), UpdateOverItemEvent, new RoutedEventHandler(OnUpdateOverItem));
        }

        // The property key (since this is a read-only DP)
        private static readonly DependencyPropertyKey IsMouseDirectlyOverItemKey =
            DependencyProperty.RegisterAttachedReadOnly("IsMouseDirectlyOverItem",
                typeof(bool),
                typeof(TreeViewHelper),
                new FrameworkPropertyMetadata(null, new CoerceValueCallback(CalculateIsMouseDirectlyOverItem)));

        // Свойство зависимости, которое будет иметь значение true только для того элемента TreeViewItem, над которым находится непосредственно курсор мыши.
        // То есть, оно не будет установлено для этого родительского элемента.
        // This is the only public member, and is read-only.
        public static readonly DependencyProperty IsMouseDirectlyOverItemProperty = IsMouseDirectlyOverItemKey.DependencyProperty;

        // Прим.: Определяем и сразу регистрируем маршрутизированнное событие (а можно в конструкторе)
        // Используется для поиска ближайшего инкапсулирующего элемента TreeViewItem к текущему положению курсора мыши
        private static readonly RoutedEvent UpdateOverItemEvent = EventManager.RegisterRoutedEvent("UpdateOverItem",
            RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(TreeViewHelper));

        // Строго типизированный геттер для свойства зависимостей
        // Прим.: Нужен для вызова из xaml
        public static bool GetIsMouseDirectlyOverItem(DependencyObject obj)
            => (bool)obj.GetValue(IsMouseDirectlyOverItemProperty);

        // Вызывается при вычислении свойства IsMouseDirectlyOver для элемента TreeViewItem
        private static object CalculateIsMouseDirectlyOverItem(DependencyObject item, object value)
            => item == _currentItem;

        // This method is a listener for the UpdateOverItemEvent.  When it is received,
        // it means that the sender is the closest TreeViewItem to the mouse (closest in the sense of the tree, not geographically).
        // [Этот метод является слушателем события UpdateOverItemEvent. Когда оно получено,
        // это означает, что отправителем является ближайший к курсору мыши элемент TreeViewItem (ближайший в смысле дерева, а не географически)]
        private static void OnUpdateOverItem(object sender, RoutedEventArgs args)
        {
            // Mark this object as the tree view item over which the mouse is currently positioned
            _currentItem = sender as TreeViewItem;

            // Tell that item to re-calculate the IsMouseDirectlyOverItem property
            _currentItem?.InvalidateProperty(IsMouseDirectlyOverItemProperty);

            // Prevent this event from notifying other tree view items higher in the tree
            args.Handled = true;
        }

        // Метод является слушателем событий MouseEnter и MouseLeave для элементов TreeViewItem.
        // Он обновляет _currentItem, а также свойство IsMouseDirectlyOverItem для предыдущего и нового элементов TreeViewItem.
        private static void OnMouseTransition(object sender, MouseEventArgs args)
        {
            lock (IsMouseDirectlyOverItemProperty)
            {
                // Сообщаем предыдущему элементу, на который была наведена мышь, что он больше не используется
                if (_currentItem != null)
                {
                    var oldItem = _currentItem;
                    _currentItem = null;
                    oldItem.InvalidateProperty(IsMouseDirectlyOverItemProperty);
                }

                // Проверяем, что курсор находится над каким-либо элементом WPF
                if (Mouse.DirectlyOver is not IInputElement currentPosition)
                    return;

                // Raise an event from that point.  If a TreeViewItem is anywhere above this point in the tree,
                // it will receive this event and update _currentItem.
                // [Вызываем событие в этой точке. Если элемент TreeViewItem находится где-либо выше этой точки в дереве,
                // он получит это событие и обновит _currentItem]
                var newItemArgs = new RoutedEventArgs(UpdateOverItemEvent);
                currentPosition.RaiseEvent(newItemArgs);
            }
        }
    }
}
