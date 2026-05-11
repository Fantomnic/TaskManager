using Microsoft.Win32;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using TaskManager.Commands;
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

            UIHelper.SetFocus(okButton);
        }

        protected override void ButtonOKClick(object sender, RoutedEventArgs e)
        {
            if (!ValidateOK())
                return;

            var errors = new StringBuilder();
            var mainWindow = UIHelper.MainWindow;
            var mainViewModel = mainWindow.MainViewModel;
            var currentSelectedSectionViewModel = mainViewModel.SelectedSectionViewModel;

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

                var additionalSelectedSection = currentSelectedSectionViewModel.IsMasterSection ? null : currentSelectedSectionViewModel.Section;

                mainViewModel.RemoveAllAdditionalSections();

                DataHelper.GetAnyObjectsFromFiles(sourceFiles, out var masterSection, out var additionalSections, out _, out var errorFiles);

                if (errorFiles.Count > 0)
                    errors.AppendLine(DataHelper.GetFilesNotLoadedMessage(errorFiles));

                if (masterSection is not null)
                    mainWindow.CreateAndInitializeMasterSectionViewModel(masterSection, true);

                mainWindow.LoadAdditionalSectionsCore(additionalSections, ref errors);

                if (additionalSelectedSection is not null && mainViewModel.FindSectionViewModel(additionalSelectedSection) is SectionViewModel newSelectionSectionViewModel)
                    mainViewModel.SelectedSectionViewModel = newSelectionSectionViewModel;

                static void ShowMessageDataNotFound() => UIHelper.ShowMessage("Данные для восстановления не найдены", MessageBoxImage.Warning);
            }
            else
            {
                bool areSections;

                if (importType.SelectedIndex == 1) // Разделы
                    areSections = true;
                else if (importType.SelectedIndex == 2) // Задачи
                    areSections = false;
                else
                    throw new NotImplementedException();

                string extension = areSections ? Constants.SectionDataExtension : Constants.TaskDataExtension;

                var openFileDialog = new OpenFileDialog()
                {
                    Multiselect = true,
                    Filter = $"Данные планировщика задач (*{extension})|*{extension}",
                };

                if (openFileDialog.ShowDialog() != true)
                    return;

                var selectedFiles = openFileDialog.FileNames.ToList();

                DataHelper.GetAnyObjectsFromFiles(selectedFiles, out var newMasterSection, out var additionalSections, out var newTaskObjects, out var errorFiles);

                if (errorFiles.Count > 0)
                    errors.AppendLine(DataHelper.GetFilesNotLoadedMessage(errorFiles));

                var masterSectionViewModel = mainViewModel.MasterSectionViewModel;
                var masterSection = (MasterSection)masterSectionViewModel.Section;

                if (areSections)
                {
                    if (newMasterSection is not null)
                        TaskObjectsImportCore(masterSectionViewModel, newMasterSection.Tasks, replace.IsChecked == true);

                    foreach (var newAdditionalSection in additionalSections.OrderBy(s => s.CreationDate))
                    {
                        if (newMasterSection is null)
                            TaskObjectsImportCore(masterSectionViewModel, newAdditionalSection.Tasks, false);

                        if (mainViewModel.FindSectionViewModel(newAdditionalSection) is not SectionViewModel existingAdditionalSectionViewModel)
                        {
                            var sectionViewModel = mainViewModel.CreateAdditionalSectionViewModel(newAdditionalSection);
                            NewSectionCommand.AddSectionCore(mainViewModel, mainWindow.sections.Items, sectionViewModel, false);
                        }
                        else if (replace.IsChecked == true)
                        {
                            foreach (var newTaskObject in newAdditionalSection.Tasks)
                            {
                                if (masterSection.FindTaskObject(newTaskObject.Guid) is TaskObject currentTaskObject)
                                    currentTaskObject.CopyFrom(newTaskObject);
                            }
                        }
                    }
                }
                else
                {
                    SectionViewModel targetSectionViewModel = addInCurrentSection.IsChecked == true ? mainViewModel.SelectedSectionViewModel : masterSectionViewModel;
                    TaskObjectsImportCore(targetSectionViewModel, newTaskObjects, replace.IsChecked == true);
                }

                currentSelectedSectionViewModel.RefreshVisibleTaskViewModels();
            }

            DialogResult = true;
            Close();

            if (errors.Length > 0)
                UIHelper.ShowMessage(errors.ToString(), MessageBoxImage.Warning, "Ошибка загрузки данных");
            else
                UIHelper.ShowMessage(_successMessage, MessageBoxImage.Information);
        }

        private void TaskObjectsImportCore(SectionViewModel targetSectionViewModel, List<TaskObject> newTaskObjects, bool replaceIfExists)
        {
            foreach (var newTaskObject in newTaskObjects)
            {
                if (targetSectionViewModel.Section.FindTaskObject(newTaskObject.Guid) is not TaskObject currentTaskObject)
                {
                    var newTaskViewModel = targetSectionViewModel.CreateTaskViewModel(newTaskObject);
                    targetSectionViewModel.AddTaskViewModel(newTaskViewModel, false);
                }
                else if (replaceIfExists)
                {
                    currentTaskObject.CopyFrom(newTaskObject);
                }
            }
        }

        private void ComboboxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!IsLoaded || sender is not ComboBox combobox)
                return;

            int selectedIndex = combobox.SelectedIndex;

            if (selectedIndex == 0) // Из резервной копии
            {
                replace.Visibility = Visibility.Collapsed;
                addInCurrentSection.Visibility = Visibility.Collapsed;
            }
            else if (selectedIndex == 1) // Разделы
            {
                replace.Visibility = Visibility.Visible;
                addInCurrentSection.Visibility = Visibility.Collapsed;
            }
            else if (selectedIndex == 2) // Задачи
            {
                replace.Visibility = Visibility.Visible;
                addInCurrentSection.Visibility = Visibility.Visible;
            }
        }
    }
}
