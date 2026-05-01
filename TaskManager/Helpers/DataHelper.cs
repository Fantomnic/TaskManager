using System.IO;
using System.Reflection;
using System.Windows;
using TaskManager.Model;
using TaskManager.Resources;
using static TaskManager.Helpers.Enums;

namespace TaskManager.Helpers
{
    internal class DataHelper
    {
        internal static bool DataIsLoaded { get; set; }

        internal static bool DataIsSaved { get; set; }

        internal static MainModel ModelData { get; } = new();

        internal static bool SaveData(DataDirectory dataDirectoryType, bool showMessageOnError = true)
        {
            string finishedDirectoryPath = GetDataDirectory(dataDirectoryType);

            if (dataDirectoryType != DataDirectory.Autosave)
                ClearDirectory(finishedDirectoryPath);

            try
            {
                foreach (var sections in ModelData.AllSections)
                    sections.Serialize(dataDirectoryType);
            }
            catch
            {
                if (showMessageOnError)
                {
                    ClearDirectory(finishedDirectoryPath);
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
                DataDirectory.Autosave => Path.Combine(dataDirectory, $"{Constants.AutosaveFolder}\\{DateTime.Now:dd.MM.yyyy HH-mm-ss}"),
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
    }
}
