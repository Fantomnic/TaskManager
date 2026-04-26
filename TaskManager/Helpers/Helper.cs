using System.IO;
using System.Windows;
using System.Windows.Controls;
using TaskManager.Commands;
using TaskManager.Model;
using TaskManager.Model.BaseClasses;
using TaskManager.Resources;
using TaskManager.ViewModels;
using static TaskManager.Helpers.Enums;

namespace TaskManager.Helpers
{
    public static class Helper
    {
        internal static MainViewModel MainViewModel => UIHelper.MainWindow.MainViewModel;

        internal static MasterSectionViewModel MasterSectionViewModel => MainViewModel.MasterSectionViewModel;

        internal static MainModel ModelData => MainViewModel.ModelData;

        internal static Section? GetSectionFromTabItem(TabItem? tabItem)
            => GetSectionViewModelFromTabItem(tabItem)?.Section;

        internal static SectionViewModel? GetSectionViewModelFromTabItem(TabItem? tabItem)
            => UIHelper.GetSectionViewFromTabItem(tabItem)?.DataContext as SectionViewModel;

        internal static bool IsMasterSection(Section? section) => section is null || section.IsMasterSection;

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

        internal static string GetDataDirectory(DataDirectory dataDirectoryType, bool createIfNotExists = true)
        {
            var appDirectory = AppDomain.CurrentDomain.BaseDirectory;

            string dataDirectory = Path.Combine(appDirectory, Constants.DataDirectoty);

            string targetDirectory = dataDirectoryType switch
            {
                DataDirectory.Root => dataDirectory,
                DataDirectory.Tasks => Path.Combine(dataDirectory, Constants.TasksDirectoty),
                DataDirectory.Sections => Path.Combine(dataDirectory, Constants.SectionsDirectoty),
                _ => throw new NotImplementedException()
            };

            if (Directory.Exists(targetDirectory))
                return targetDirectory;

            if (!createIfNotExists)
                return String.Empty;

            Directory.CreateDirectory(targetDirectory);
            return targetDirectory;
        }

        internal static Guid GenereateGuid(GenereateGuidTarget target)
        {
            Guid result = Guid.NewGuid();

            if (target == GenereateGuidTarget.None)
                return result;

            IEnumerable<BaseSerializableObject> collection = target switch
            {
                GenereateGuidTarget.Task => ModelData.BaseSection.Tasks,
                GenereateGuidTarget.Section => ModelData.AllSections,
                _ => throw new NotImplementedException()
            };

            while (true)
            {
                if (collection.Select(t => t.Guid).Contains(result))
                    result = Guid.NewGuid();
                else
                    return result;
            }
        }

        internal static T GetCommandInstance<T>() where T : BaseCommand
        {
            string key;

            if (typeof(T) == typeof(ChangeSectionCommand))
                key = Texts.KeyChangeSectionCommand;
            else if (typeof(T) == typeof(DeleteSectionCommand))
                key = "DeleteSectionCommand";
            else if(typeof(T) == typeof(DeleteTaskCommand))
                key = "DeleteTaskCommand";
            else if (typeof(T) == typeof(NewSectionCommand))
                key = "NewSectionCommand";
            else if (typeof(T) == typeof(NewTaskCommand))
                key = "NewTaskCommand";
            else if (typeof(T) == typeof(OpenSettingsCommand))
                key = "OpenSettingsCommand";
            else if (typeof(T) == typeof(ShowSectionPropertyCommand))
                key = "ShowSectionPropertyCommand";
            else if (typeof(T) == typeof(AcceptTaskCommand))
                key = "AcceptTaskCommand";
            else if (typeof(T) == typeof(ChangeTaskStatusCommand))
                key = "changeTaskStatusCommand";
            else if (typeof(T) == typeof(CompleteTaskCommand))
                key = "CompleteTaskCommand";
            else if (typeof(T) == typeof(DeferTaskCommand))
                key = "DeferTaskCommand";
            else if (typeof(T) == typeof(DoneTaskCommand))
                key = "DoneTaskCommand";
            else if (typeof(T) == typeof(RejectTaskCommand))
                key = "RejectTaskCommand";
            else 
                throw new NotImplementedException();

            return GetResource<T>(key);
        }

        internal static T GetResource<T>(string key) where T : class
            => Application.Current.Resources[key] as T ?? throw new InvalidOperationException("Не удалось получить команду из ресурсов");
    }
}
