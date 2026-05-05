using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Windows;
using TaskManager.Model;
using TaskManager.Model.BaseClasses;
using TaskManager.Resources;
using static TaskManager.Helpers.Enums;

namespace TaskManager.Helpers
{
    internal static class DataHelper
    {
        internal static bool DataIsLoaded { get; set; }

        internal static bool DataIsSaved { get; set; }

        internal static MainModel ModelData { get; } = new();

        internal static bool SaveData(DataDirectory dataDirectoryType, bool showMessageOnError = true)
            => SaveData(dataDirectoryType, out _, showMessageOnError);

        internal static bool SaveData(DataDirectory dataDirectoryType, out string savedDirectoryPath, bool showMessageOnError = true)
        {
            savedDirectoryPath = GetDataDirectory(dataDirectoryType);

            if (dataDirectoryType != DataDirectory.BackupWithDate)
                ClearDirectory(savedDirectoryPath);

            try
            {
                foreach (var section in ModelData.AllSections)
                    section.Serialize(dataDirectoryType);
            }
            catch
            {
                if (showMessageOnError)
                {
                    ClearDirectory(savedDirectoryPath);
                    UIHelper.ShowMessage("Не удалось сохранить данные разделов", MessageBoxImage.Error, "Ошибка сохранения данных");
                }

                return false;
            }

            return true;
        }

        internal static string GetDataDirectory(DataDirectory dataDirectoryType, bool createIfNotExists = true)
        {
            // При установке, скажем, в Program Files, потребуется запуск от имени администратора
            //string appDirectory = AppDomain.CurrentDomain.BaseDirectory;

            string appDataSystem = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string appDataDirectory = Path.Combine(appDataSystem, Assembly.GetExecutingAssembly().GetName().Name ?? String.Empty);
            string dataDirectory = Path.Combine(appDataDirectory, Constants.DataFolder);

            string targetDirectory = dataDirectoryType switch
            {
                DataDirectory.Root => dataDirectory,
                DataDirectory.SourceSections => Path.Combine(dataDirectory, Constants.SourceSectionsFolder),
                DataDirectory.FinishedSections => Path.Combine(dataDirectory, Constants.FinishedSectionsFolder),
                DataDirectory.Backup => Path.Combine(dataDirectory, Constants.BackupFolder),
                DataDirectory.BackupWithDate => Path.Combine(dataDirectory, $"{Constants.BackupFolder}\\{DateTime.Now:dd.MM.yyyy HH-mm-ss}"),
                _ => throw new NotImplementedException()
            };

            if (Directory.Exists(targetDirectory))
                return targetDirectory;

            if (!createIfNotExists)
                return String.Empty;

            Directory.CreateDirectory(targetDirectory);
            return targetDirectory;
        }

        internal static void ClearDirectory(string directoryPath)
        {
            foreach (var file in Helper.GetAppFiles(directoryPath))
            {
                try
                {
                    file.Delete();
                }
                catch
                {
                    // TODO
                }
            }
        }

        internal static void GetAnyObjectsFromFiles(List<string> files,
            out MasterSection? masterSection,
            out List<AdditionalSection> additionalSections,
            out List<TaskObject> taskObjects,
            out List<string> errorFiles)
        {
            masterSection = null;
            additionalSections = [];
            taskObjects = [];
            errorFiles = [];

            var sectionsFiles = new List<string>();
            var tasksFiles = new List<string>();

            foreach (var file in files)
            {
                string fileExtension = Path.GetExtension(file);

                if (String.Equals(fileExtension, Constants.SectionDataExtension, StringComparison.OrdinalIgnoreCase))
                    sectionsFiles.Add(file);
                else if (String.Equals(fileExtension, Constants.TaskDataExtension, StringComparison.OrdinalIgnoreCase))
                    tasksFiles.Add(file);
            }

            if (sectionsFiles.Count > 0
                && !TryGetObjectsFromFiles(sectionsFiles, out additionalSections, out _, out var errorFilesInner, false)
                && TryGetObjectsFromFiles<MasterSection>(errorFilesInner, out var masterSections, out _, out errorFiles, false))
            {
                masterSection = masterSections.FirstOrDefault();
            }

            if (tasksFiles.Count > 0)
            {
                TryGetObjectsFromFiles(tasksFiles, out taskObjects, out _, out var errorFilesTasks, false);
                errorFiles.AddRange(errorFilesTasks);
            }
        }

        internal static bool TryGetObjectFromFile<T>(string file, out T result, out string errorMessage, bool throwOnError = true)
            where T : BaseObject
        {
            result = null;
            errorMessage = String.Empty;

            try
            {
                var serialiser = new DataContractSerializer(typeof(T), [typeof(TaskObject)]);

                using var stream = new FileStream(file, FileMode.Open);
                var value = serialiser.ReadObject(stream);

                if (value is T targetTypeObject)
                {
                    result = targetTypeObject;
                    return true;
                }
            }
            catch
            {
                errorMessage = $"Не удалось загрузить данные из файла {file}";

                if (throwOnError)
                    throw new InvalidOperationException(errorMessage);
            }

            return false;
        }

        internal static bool TryGetObjectsFromFiles<T>(List<string> files,
            out List<T> result,
            out string errorMessage,
            out List<string> errorFiles,
            bool throwOnError = true)
                where T : BaseObject
        {
            result = [];
            errorMessage = String.Empty;
            errorFiles = [];

            var serialiser = new DataContractSerializer(typeof(T), [typeof(TaskObject)]);

            foreach (string file in files)
            {
                try
                {
                    using var stream = new FileStream(file, FileMode.Open);
                    var value = serialiser.ReadObject(stream);

                    if (value is T targetTypeObject)
                        result.Add(targetTypeObject);
                }
                catch
                {
                    errorFiles.Add(file);
                }
            }

            if (errorFiles.Count > 0)
            {
                errorMessage = GetFilesNotLoadedMessage(errorFiles);

                if (throwOnError)
                    throw new InvalidOperationException(errorMessage);

                return false;
            }

            return result.Count > 0;
        }

        internal static string GetFilesNotLoadedMessage(List<string> errorFiles)
        {
            string filesEnding = errorFiles.Count > 1 ? "файлов" : "файла";
            return $"Не удалось загрузить данные из {filesEnding}" + Environment.NewLine + String.Join(";" + Environment.NewLine, errorFiles);
        }
    }
}
