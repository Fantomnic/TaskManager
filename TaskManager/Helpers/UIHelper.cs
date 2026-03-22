using System.Windows;
using System.Windows.Controls;
using TaskManager.View;

namespace TaskManager.Helpers
{
    internal static class UIHelper
    {
        internal static MainWindow MainWindow => (MainWindow)Application.Current.MainWindow;

        internal static SectionView? GetSectionViewFromTabItem(TabItem? tabItem) => tabItem?.Content as SectionView;

        internal static SectionView? GetCurrentSectionView() => GetSectionViewFromTabItem(MainWindow.sections.SelectedItem as TabItem);
    }
}
