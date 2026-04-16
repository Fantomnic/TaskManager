using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
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

        internal static void ShowMessage(string message, string caption = "Сообщение", MessageBoxImage icon = MessageBoxImage.None)
        {
            var iconImage = GetIconAsImage(icon);
            var messageWindow = new MessageWindow(message, caption, iconImage);
            messageWindow.ShowDialog();
        }

        private static BitmapSource? GetIconAsImage(MessageBoxImage iconType)
        {
            return iconType switch
            {
                MessageBoxImage.None => null,
                MessageBoxImage.Information => GetIconAsImageCore(SystemIcons.Information),
                MessageBoxImage.Warning => GetIconAsImageCore(SystemIcons.Warning),
                MessageBoxImage.Error => GetIconAsImageCore(SystemIcons.Error),
                MessageBoxImage.Question => GetIconAsImageCore(SystemIcons.Question),
                _ => throw new NotImplementedException()
            };

            BitmapSource GetIconAsImageCore(Icon icon)
                => Imaging.CreateBitmapSourceFromHIcon(icon.Handle, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
        }
    }
}
