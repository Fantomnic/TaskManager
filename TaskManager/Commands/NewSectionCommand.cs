using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using TaskManager.Helpers;
using TaskManager.Model;
using TaskManager.Views;
using TaskManager.ViewModels;

namespace TaskManager.Commands
{
    /// <summary>Команда "Создать новый раздел" (неосновной)</summary>
    public class NewSectionCommand : BaseCommand
    {
        internal override void ExecuteImplement(object? parameter)
        {
            string name = GetDefaultSectionName();
            var mainWindow = UIHelper.MainWindow;
            var mainViewModel = mainWindow.MainViewModel;
            var sectionViewModel = mainViewModel.CreateAdditionalSection(name);

            var windowProperty = new SectionPropertyWindow(sectionViewModel, true);

            if (windowProperty.ShowDialog() != true)
                return;

            windowProperty.SaveToViewModel();
            AddSectionCore(mainViewModel, mainWindow.sections.Items, sectionViewModel);
        }

        internal static void AddSectionCore(MainViewModel mainViewModel, ItemCollection tabItems, AdditionalSectionViewModel newSectionViewModel, bool setFocus = true)
        {
            mainViewModel.AddSectionViewModel(newSectionViewModel);
            var newItem = CreateTabItem(newSectionViewModel);
            tabItems.Add(newItem);

            if (setFocus)
                newItem.Focus();
        }

        private static string GetDefaultSectionName()
        {
            if (Settings.SetDefaultSectionName != true)
                return String.Empty;

            string result = Settings.DefaultSectionName;

            if (Settings.IncrementSectionName == true)
            {
                var existingNames = Helper.MainViewModel.SectionsViewModels.Select(s => s.Name);
                result = Helper.GetStringWithCounter(result, existingNames);
            }

            return result;
        }

        private static TabItem CreateTabItem(AdditionalSectionViewModel sectionViewModel, bool baseSection = false)
        {
            var textBlock = CreateSectionHeader(sectionViewModel);

            var sectionTabItem = new TabItem()
            {
                Header = textBlock,
                Content = new AdditionalSectionView(sectionViewModel),
            };

            return sectionTabItem;
        }

        // Создаёт заголовок для TabItam'а в виде контрола
        private static TextBlock CreateSectionHeader(AdditionalSectionViewModel sectionViewModel)
        {
            var textBlock = new TextBlock()
            {
                Style = Helper.GetResource<Style>("AdditionalSectionHeaderContextMenu"),
                DataContext = sectionViewModel,
            };

            var headerNameBinding = new Binding
            {
                Path = new PropertyPath(nameof(sectionViewModel.Name))
            };

            textBlock.SetBinding(TextBlock.TextProperty, headerNameBinding);

            return textBlock;
        }
    }
}
