using System.Windows;
using System.Windows.Controls;
using TaskManager.Model;
using TaskManager.View;
using TaskManager.ViewModel;

namespace TaskManager.Helpers
{
    public static class Helper
    {
        internal static MainWindow MainWindow = (MainWindow)Application.Current.MainWindow;

        internal static Section? GetSectionFromTabItem(TabItem? tabItem)
            => ((tabItem?.Content as SectionView)?.DataContext as SectionViewModel)?.Section;
    }
}
