using System.Windows;
using System.Windows.Controls;
using TaskManager.Model;
using TaskManager.View;
using TaskManager.ViewModel;

namespace TaskManager.Helpers
{
    public static class Helper
    {
        internal static MainViewModel MainViewModel => UIHelper.MainWindow.MainViewModel;

        internal static SectionViewModel BaseSectionViewModel => MainViewModel.BaseSectionViewModel;

        internal static MainModel ModelData => MainViewModel.ModelData;

        internal static Section? GetSectionFromTabItem(TabItem? tabItem)
            => GetSectionViewModelFromTabItem(tabItem)?.Section;

        internal static SectionViewModel? GetSectionViewModelFromTabItem(TabItem? tabItem)
            => UIHelper.GetSectionViewFromTabItem(tabItem)?.DataContext as SectionViewModel;

        internal static bool IsBaseSection(Section? section) => section is null || section.IsBaseSection;

        internal static List<TaskObjectViewModel> GetAllTasksViewModels() => [.. BaseSectionViewModel.TasksViewModels];

        internal static List<TaskObject> GetAllTasks() => [.. ModelData.BaseSection.Tasks];

        internal static string GetStringWithCounter(string targetString, IEnumerable<string> sourceStrings)
        {
            for (int i = 0; i < 1000;)
            {
                string result = $"{targetString} {++i}";

                if (sourceStrings.Contains(result))
                    continue;

                return result;
            }

            // TODO: Обработка исключений
            throw new Exception("Ааааа!");
        }

        internal static SectionViewModel? FindSectionViewModel(Section section)
            => MainViewModel.SectionsViewModels.FirstOrDefault(vm => vm.Section == section);
    }
}
