using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using TaskManager.Helpers.Exceptions;
using TaskManager.ViewModels;
using TaskManager.Views;

namespace TaskManager.Helpers
{
    internal static class UIHelper
    {
        internal static MainWindow MainWindow => (MainWindow)Application.Current.MainWindow;

        internal static SectionView GetSectionViewFromTabItem(TabItem? tabItem)
        {
            var content = tabItem?.Content;

            if (content is AdditionalSectionView additionalSectionView)
                return additionalSectionView;

            if (content is ContentControl contentControl && contentControl.Content is MasterSectionView masterSectionView)
                return masterSectionView;

            throw new WarningException("Не удалось получить представление раздела из вкладки");
        }

        internal static TabItem? GetTabItemWithSectionViewModel(SectionViewModel sectionViewModel)
            => MainWindow.sections.Items.OfType<TabItem>().FirstOrDefault(t => Helper.GetSectionViewModelFromTabItem(t) == sectionViewModel);

        internal static void RemoveAllAdditionalTabItems()
        {
            var items = MainWindow.sections.Items;

            while (items.Count > 1)
                items.RemoveAt(1);
        }

        internal static SectionView GetCurrentSectionView() => GetSectionViewFromTabItem(MainWindow.sections.SelectedItem as TabItem);

        internal static bool ShowMessage(string message, MessageBoxImage icon = MessageBoxImage.None, string? caption = null)
        {
            caption ??= GetMessageCaptionFromIcon(icon);

            var iconImage = GetIconAsImage(icon);
            var messageWindow = new MessageWindow(message, caption, iconImage, icon == MessageBoxImage.Question);
            return messageWindow.ShowDialog() == true;
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

        private static string GetMessageCaptionFromIcon(MessageBoxImage iconType)
        {
            return iconType switch
            {
                MessageBoxImage.None => "Сообщение",
                MessageBoxImage.Information => "Внимание",
                MessageBoxImage.Warning => "Предупреждение",
                MessageBoxImage.Error => "Ошибка",
                MessageBoxImage.Question => "Вопрос",
                _ => throw new NotImplementedException()
            };
        }

        internal static void SetFocus(UIElement element)
        {
            element.Focus();

            if (element is TextBox textBox)
                textBox.CaretIndex = Int32.MaxValue;
        }
    }
}
