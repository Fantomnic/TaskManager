using System.Windows;
using System.Windows.Controls;
using TaskManager.ViewModels;
using TaskManager.Views;

namespace TaskManager.Helpers
{
    internal static class UIHelper
    {
        internal static MainWindow MainWindow => (MainWindow)Application.Current.MainWindow;

        // У основного раздела ListBox, и там ContentControl
        // У неосновного сразу AdditionalSectionView
        internal static SectionView GetSectionViewFromTabItem(TabItem? tabItem)
        {
            var content = tabItem?.Content;

            if (content is AdditionalSectionView additionalSectionView)
                return additionalSectionView;

            if (content is ContentControl contentControl && contentControl.Content is MasterSectionView masterSectionView)
                return masterSectionView;

            throw new InvalidOperationException("Не удалось получить представление раздела из вкладки");
        }

        internal static TabItem? GetTabItemWithSectionViewModel(SectionViewModel sectionViewModel)
            => MainWindow.sections.Items.OfType<TabItem>().FirstOrDefault(t => Helper.GetSectionViewModelFromTabItem(t) == sectionViewModel);

        internal static SectionView GetCurrentSectionView() => GetSectionViewFromTabItem(MainWindow.sections.SelectedItem as TabItem);
    }
}
