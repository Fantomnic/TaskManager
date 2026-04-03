using System.Windows;
using System.Windows.Controls;

namespace TaskManager.Views
{
    public class StretchingTreeView : TreeView
    {
        protected override DependencyObject GetContainerForItemOverride() => new StretchingTreeViewItem();

        protected override bool IsItemItsOwnContainerOverride(object item) => item is StretchingTreeViewItem;
    }

    // https://stackoverflow.com/questions/47258955/how-to-stretch-treeviewitem-width-to-fill-parent
    // Переопределяем элемент дерева и удаляем из его грида последний столбец
    // Альтернатива: присвоить элементу шаблон с кастомным гридом, но тогда придётся много чего в этом шаблоне прописывать
    public class StretchingTreeViewItem : TreeViewItem
    {
        public StretchingTreeViewItem()
        {
            Loaded += new RoutedEventHandler(StretchingTreeViewItemLoaded);
        }

        private void StretchingTreeViewItemLoaded(object sender, RoutedEventArgs e)
        {
            if (VisualChildrenCount == 0 || GetVisualChild(0) is not Grid grid || grid.ColumnDefinitions.Count != 3)
                return;

            grid.ColumnDefinitions.RemoveAt(2);
            grid.ColumnDefinitions[1].Width = new GridLength(1, GridUnitType.Star);
        }

        protected override DependencyObject GetContainerForItemOverride() => new StretchingTreeViewItem();

        protected override bool IsItemItsOwnContainerOverride(object item) => item is StretchingTreeViewItem;
    }
}
