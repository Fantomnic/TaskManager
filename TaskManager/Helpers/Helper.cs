using System.Windows;
using System.Windows.Controls;
using TaskManager.Model;
using TaskManager.View;
using TaskManager.ViewModel;

namespace TaskManager.Helpers
{
    public static class Helper
    {
        internal static MainWindow MainWindow => (MainWindow)Application.Current.MainWindow;

        internal static MainViewModel MainViewModel => MainWindow.MainViewModel;

        internal static BaseSection BaseSection => MainViewModel.BaseSection;

        internal static Section? GetSectionFromTabItem(TabItem? tabItem)
            => GetSectionViewModelFromTabItem(tabItem)?.Section;

        internal static SectionViewModel? GetSectionViewModelFromTabItem(TabItem? tabItem)
            => GetSectionViewFromTabItem(tabItem)?.DataContext as SectionViewModel;

        internal static SectionView? GetSectionViewFromTabItem(TabItem? tabItem) => tabItem?.Content as SectionView;

        internal static bool IsBaseSection(Section? section) => section is null || section.IsBaseSection;

        internal static List<TaskObject> GetAllTasks() => [.. BaseSection.Tasks];

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
    }
}
