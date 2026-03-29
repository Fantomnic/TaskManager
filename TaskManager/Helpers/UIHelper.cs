using System.Windows;
using System.Windows.Controls;
using TaskManager.View;

namespace TaskManager.Helpers
{
    internal static class UIHelper
    {
        internal static MainWindow MainWindow => (MainWindow)Application.Current.MainWindow;

        internal static SectionView? GetSectionViewFromTabItem(TabItem? tabItem)
        {
            var content = tabItem?.Content;

            if (content is AdditionalSectionView additionalSectionView)
                return additionalSectionView;
            else if (content is MasterSectionView masterSectionView)
                return masterSectionView;

            return null;
        }

        internal static SectionView? GetCurrentSectionView() => GetSectionViewFromTabItem(MainWindow.sections.SelectedItem as TabItem);
    }
}
