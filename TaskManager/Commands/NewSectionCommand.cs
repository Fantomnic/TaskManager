using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using TaskManager.Helpers;
using TaskManager.Model;
using TaskManager.View;
using TaskManager.ViewModel;

namespace TaskManager.Commands
{
    /// <summary>Команда "Создать новый раздел"</summary>
    public class NewSectionCommand : BaseCommand
    {
        internal override void ExecuteImplement(object? parameter)
        {
            AddSection();
        }

        internal static void AddSection(bool baseSection = false)
        {
            string name = baseSection ? "Все" : GetDefaultSectionName();
            var mainWindow = UIHelper.MainWindow;
            var mainViewModel = mainWindow.MainViewModel;
            var sectionViewModel = mainViewModel.CreateSection(name, baseSection);

            if (baseSection)
            {
                var startTask = Section.CreateTask("Тестовая");
                sectionViewModel.AddTask(startTask);
            }
            else
            {
                var windowProperty = new SectionPropertyWindow(sectionViewModel);

                if (windowProperty.ShowDialog() != true)
                    return;
            }

            var newItem = CreateTabItem(sectionViewModel, baseSection);
            mainViewModel.AddSection(sectionViewModel.Section, sectionViewModel);
            mainWindow.sections.Items.Add(newItem);
            newItem.Focus();
        }

        private static TabItem CreateTabItem(SectionViewModel sectionViewModel, bool baseSection = false)
        {
            var textBlock = CreateSectionHeader(sectionViewModel);

            var sectionTabItem = new TabItem()
            {
                Header = textBlock,
                Content = new SectionView(sectionViewModel),
            };

            textBlock.ContextMenu = CreateSectionHeaderContextMenu();

            return sectionTabItem;

            ContextMenu CreateSectionHeaderContextMenu()
            {
                var menuItems = baseSection
                    ? CreateBaseSectionHeaderContextMenuItemsList(sectionViewModel)
                    : CreateNewSectionHeaderContextMenuItemsList(sectionViewModel, sectionTabItem);

                menuItems = [.. menuItems.OrderBy(x => ((TextBlock)x.Header).Text)];

                var menu = new ContextMenu();

                foreach (var menuItem in menuItems)
                    menu.Items.Add(menuItem);

                return menu;
            }
        }

        // Создаёт заголовок для TabItam'а в виде контрола
        private static TextBlock CreateSectionHeader(SectionViewModel sectionViewModel)
        {
            var textBlock = CreateFontTextBlock(String.Empty);
            textBlock.DataContext = sectionViewModel;

            var headerNameBinding = new Binding
            {
                Path = new PropertyPath(nameof(sectionViewModel.Name))
            };

            textBlock.SetBinding(TextBlock.TextProperty, headerNameBinding);

            return textBlock;
        }

        private static List<MenuItem> CreateNewSectionHeaderContextMenuItemsList(SectionViewModel sectionViewModel, TabItem sectionTabItem)
        {
            var menuItems = CreateBaseSectionHeaderContextMenuItemsList(sectionViewModel);

            menuItems.AddRange(
                [
                    new() { Header = CreateFontTextBlock("Удалить раздел"), Command = Helper.GetCommandInstance<DeleteSectionCommand>(), CommandParameter = sectionTabItem },
                ]);

            return menuItems;
        }

        private static List<MenuItem> CreateBaseSectionHeaderContextMenuItemsList(SectionViewModel sectionViewModel) => 
            [
                new() { Header = CreateFontTextBlock("Свойства"), Command = Helper.GetCommandInstance<ShowSectionPropertyCommand>(), CommandParameter = sectionViewModel },
            ];

        private static TextBlock CreateFontTextBlock(string name) =>
            new()
            {
                Text = name,
                Style = Application.Current.Resources["baseTextBlock"] as Style,
            };

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
    }
}
