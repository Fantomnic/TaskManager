using Microsoft.Win32;
using System.IO;
using System.Text;
using System.Windows;
using TaskManager.Helpers;
using TaskManager.Model;
using TaskManager.Resources;
using TaskManager.ViewModels;
using static TaskManager.Helpers.Enums;

namespace TaskManager.Views
{
    /// <summary>
    /// Interaction logic for ImportWindow.xaml
    /// </summary>
    public partial class ImportWindow : WindowWithBottomButtons
    {
        private const string _successMessage = "Данные успешно импортированы";

        public ImportWindow()
        {
            InitializeComponent();
        }

        protected override void ButtonOKClick(object sender, RoutedEventArgs e)
        {
            if (!ValidateOK())
                return;

            var errors = new StringBuilder();

            if (importType.SelectedIndex == 0) // Из резервной копии
            {
                string backupDirectory = DataHelper.GetDataDirectory(DataDirectory.Backup, false);

                if (String.IsNullOrEmpty(backupDirectory))
                {
                    ShowMessageDataNotFound();
                    return;
                }

                var directoryInfo = new DirectoryInfo(backupDirectory);
                var allBackupFolders = directoryInfo.GetDirectories();

                if (allBackupFolders.Length == 0)
                {
                    ShowMessageDataNotFound();
                    return;
                }

                string targetDirectory = allBackupFolders.OrderBy(d => d.CreationTime).Last().FullName;

                if (!UIHelper.ShowMessage($"Восстановить данные из папки {targetDirectory}? Все несохранённые изменения будут потеряны", MessageBoxImage.Question))
                    return;

                var sourceFiles = Helper.GetAppFiles(targetDirectory).Select(f => f.FullName).ToList();

                if (sourceFiles.Count == 0)
                {
                    ShowMessageDataNotFound();
                    return;
                }

                var mainWindow = UIHelper.MainWindow;

                var currentSelectedSectionViewModel = mainWindow.MainViewModel.SelectedSectionViewModel;
                var additionalSelectedSection = currentSelectedSectionViewModel.IsMasterSection ? null : currentSelectedSectionViewModel.Section;

                mainWindow.MainViewModel.RemoveAllAdditionalSections();

                DataHelper.GetAnyObjectsFromFiles(sourceFiles, out var masterSection, out var additionalSections, out _);

                if (masterSection is not null)
                    mainWindow.CreateAndInitializeMasterSectionViewModel(masterSection, true);

                mainWindow.LoadAdditionalSectionsCore(additionalSections, ref errors);

                if (additionalSelectedSection is not null && mainWindow.MainViewModel.FindSectionViewModel(additionalSelectedSection) is SectionViewModel newSelectionSectionViewModel)
                    mainWindow.MainViewModel.SelectedSectionViewModel = newSelectionSectionViewModel;

                static void ShowMessageDataNotFound() => UIHelper.ShowMessage("Данные для восстановления не найдены", MessageBoxImage.Warning);
            }
            else
            {
                bool isSections;

                if (importType.SelectedIndex == 1) // Разделы
                    isSections = true;
                else if (importType.SelectedIndex == 2) // Задачи
                    isSections = false;
                else
                    throw new NotImplementedException();

                string extension = isSections ? Constants.SectionDataExtension : Constants.TaskDataExtension;

                var openFileDialog = new OpenFileDialog()
                {
                    Multiselect = true,
                    Filter = $"Раздел планировщика задач (*{extension})|*{extension}",
                };

                if (openFileDialog.ShowDialog() != true)
                    return;

                var selectedFiles = openFileDialog.FileNames.ToList();

                DataHelper.TryGetObjectsFromFiles<AdditionalSection>(selectedFiles, out var additionalSections, out string errorMessage, out var errorFiles, false);
            }

            DialogResult = true;
            Close();

            if (errors.Length > 0)
                UIHelper.ShowMessage(errors.ToString(), MessageBoxImage.Warning, "Ошибка загрузки данных");
            else
                UIHelper.ShowMessage(_successMessage, MessageBoxImage.Information);
        }
    }
}
