using System.Windows;
using System.Windows.Controls;
using TaskManager.Commands;
using TaskManager.Model;
using TaskManager.Resources;
using TaskManager.ViewModel;

namespace TaskManager.Helpers
{
    public static class Helper
    {
        internal static MainViewModel MainViewModel => UIHelper.MainWindow.MainViewModel;

        internal static MasterSectionViewModel MasterSectionViewModel => MainViewModel.MasterSectionViewModel;

        internal static MainModel ModelData => MainViewModel.ModelData;

        internal static Section? GetSectionFromTabItem(TabItem? tabItem)
            => GetSectionViewModelFromTabItem(tabItem)?.Section;

        internal static AdditionalSectionViewModel? GetSectionViewModelFromTabItem(TabItem? tabItem)
            => UIHelper.GetSectionViewFromTabItem(tabItem)?.DataContext as AdditionalSectionViewModel;

        internal static bool IsBaseSection(Section? section) => section is null || section.IsMasterSection;

        internal static List<TaskObjectViewModel> GetAllTasksViewModels() => [.. MasterSectionViewModel.AllTasksViewModels];

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

        internal static T GetCommandInstance<T>() where T : BaseCommand
        {
            string key;

            if (typeof(T) == typeof(ChangeSectionCommand))
                key = Texts.KeyChangeSectionCommand;
            else if (typeof(T) == typeof(DeleteSectionCommand))
                key = "deleteSectionCommand";
            else if(typeof(T) == typeof(DeleteTaskCommand))
                key = "deleteTaskCommand";
            else if (typeof(T) == typeof(NewSectionCommand))
                key = "newSectionCommand";
            else if (typeof(T) == typeof(NewTaskCommand))
                key = "newTaskCommand";
            else if (typeof(T) == typeof(OpenSettingsCommand))
                key = "openSettingsCommand";
            else if (typeof(T) == typeof(ShowSectionPropertyCommand))
                key = "showSectionPropertyCommand";
            else if (typeof(T) == typeof(AcceptTaskCommand))
                key = "acceptTaskCommand";
            else if (typeof(T) == typeof(ChangeTaskStatusCommand))
                key = "changeTaskStatusCommand";
            else if (typeof(T) == typeof(CompleteTaskCommand))
                key = "completeTaskCommand";
            else if (typeof(T) == typeof(DeferTaskCommand))
                key = "deferTaskCommand";
            else if (typeof(T) == typeof(DoneTaskCommand))
                key = "doneTaskCommand";
            else if (typeof(T) == typeof(RejectTaskCommand))
                key = "rejectTaskCommand";
            else 
                throw new NotImplementedException();

            return GetResource<T>(key);
        }

        internal static T GetResource<T>(string key) where T : class
            => Application.Current.Resources[key] as T ?? throw new InvalidOperationException("Не удалось получить команду из ресурсов");
    }
}
