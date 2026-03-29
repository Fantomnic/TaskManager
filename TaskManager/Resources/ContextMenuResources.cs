using System.Windows.Input;
using TaskManager.View;

namespace TaskManager.Resources
{
    // Этот класс нужен, чтобы была возможность использовать EventTrigger
    // Для обработки логики можно задать любой класс в x:Class для ResourceDictionary (который не будет противоречить наследованию)
    public partial class ContextMenuResources
    {
        // TODO: MouseDown ??
        private void PreviewMouseRightButtonDownItem(object sender, MouseButtonEventArgs e)
        {
            if (sender is StretchingTreeViewItem item)
            {
                item.Focus();
                //e.Handled = true; // Прим.: Если ставим true - событие помечается как обработанное и не туннелирует дальше
            }
        }
    }
}
