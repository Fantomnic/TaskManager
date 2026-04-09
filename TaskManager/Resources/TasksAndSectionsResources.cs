using System.Windows;
using System.Windows.Input;
using TaskManager.ViewModels;
using TaskManager.Views;

namespace TaskManager.Resources
{
    // Этот класс нужен, чтобы была возможность использовать EventTrigger
    // Для обработки логики можно задать любой класс в x:Class для ResourceDictionary (который не будет противоречить наследованию)
    public partial class TasksAndSectionsResources
    {
        #region Drad&Drop

        // Пока извне не используется
        internal static StretchingTreeViewItem? DraggingItem { get; private set; }

        private void PreviewMouseRightButtonDownItem(object sender, MouseButtonEventArgs e)
        {
            if (sender is StretchingTreeViewItem item)
            {
                item.Focus();
                //e.Handled = true; // Прим.: Если ставим true - событие помечается как обработанное и не туннелирует дальше
            }
        }

        // TODO: Почему-то поднимающееся событие не вызывается
        // Прим.: Если поставить e.Handled = true, сломается древовидная обработка
        private void PreviewMouseLeftButtonDownItem(object sender, MouseButtonEventArgs e)
        {
            // Прим.: В e.Source лежит ItemsPresenter
            if (sender is not StretchingTreeViewItem sourceItem || DraggingItem == sourceItem)
                return;

            if (DraggingItem is not null)
                DraggingItem.AllowDrop = true;

            DraggingItem = sourceItem;
            DraggingItem.AllowDrop = false; // Блокируем, чтобы нельзя было добавить сам в себя
        }

        private void PreviewMouseMoveItem(object sender, MouseEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Pressed || DraggingItem is null)
                return;

            DragDrop.DoDragDrop(DraggingItem, DraggingItem.DataContext, DragDropEffects.Move);
        }

        private void DropItem(object sender, DragEventArgs e)
        {
            if ((sender as StretchingTreeViewItem)?.DataContext is not TaskObjectViewModel targetTaskViewModel
                || DraggingItem?.DataContext is not TaskObjectViewModel sourceTaskViewModel)
            {
                return;
            }

            targetTaskViewModel.AddChildViewModel(sourceTaskViewModel);

            e.Handled = true;
        }

        #endregion Drad&Drop
    }
}
