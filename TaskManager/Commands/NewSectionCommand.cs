using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using TaskManager.Helpers;
using TaskManager.Model;
using TaskManager.View;
using TaskManager.ViewModel;

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
            var sectionViewModel = MainViewModel.CreateSection(name);

            var windowProperty = new SectionPropertyWindow(sectionViewModel);

            if (windowProperty.ShowDialog() != true)
                return;

            mainViewModel.AddSectionViewModel(sectionViewModel);
            var newItem = CreateTabItem(sectionViewModel);
            mainWindow.sections.Items.Add(newItem);
            newItem.Focus();
        }

        internal static void Test()
        {
            var mainWindow = UIHelper.MainWindow;
            var mainViewModel = mainWindow.MainViewModel;
            var sectionViewModel = MainViewModel.CreateSection("Раздел 2");



            var startTaskViewModel = SectionViewModel.CreateTask("Тест Родитель 1");
            var startChildTaskViewModel = SectionViewModel.CreateTask("Тест Дочь 1");
            var startUnderchildTaskViewModel = SectionViewModel.CreateTask("Тест Поддочь 1");
            sectionViewModel.AddTaskViewModel(startTaskViewModel);
            mainViewModel.MasterSectionViewModel.AddTaskViewModel(startChildTaskViewModel);
            mainViewModel.MasterSectionViewModel.AddTaskViewModel(startUnderchildTaskViewModel);

            startTaskViewModel.AddChildViewModel(startChildTaskViewModel);
            startChildTaskViewModel.AddChildViewModel(startUnderchildTaskViewModel);



            mainViewModel.AddSectionViewModel(sectionViewModel);
            var newItem = CreateTabItem(sectionViewModel);
            mainWindow.sections.Items.Add(newItem);
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
                Style = Helper.GetResource<Style>("sectionHeaderContextMenu"),
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
