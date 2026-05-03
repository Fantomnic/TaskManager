using Microsoft.Win32;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using TaskManager.Helpers;
using TaskManager.Model;
using TaskManager.Model.BaseClasses;
using TaskManager.Resources;
using TaskManager.ViewModels;

namespace TaskManager.Views
{
    /// <summary>
    /// Interaction logic for ExportWindow.xaml
    /// </summary>
    public partial class ExportWindow : WindowWithBottomButtons
    {
        private const string _successMessage = "Данные успешно сохранены";

        private readonly ExportViewModel _exportViewModel;

        internal ExportWindow()
        {
            Settings.SettingsInstanse.NotResetIndication = true;

            try
            {
                // Почему-то иногда при инициализации устанавливается false в выбранный в меню выриант индикации
                InitializeComponent();
            }
            finally
            {
                Settings.SettingsInstanse.NotResetIndication = false;
            }
            
            DataContext = _exportViewModel = new ExportViewModel();
            sectionsExportList.SelectedItem = Helper.MasterSectionViewModel;
        }

        private void SectionsSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _exportViewModel.RefreshTasksViewModels(GetSelectedSections());

            if (_exportViewModel.TasksViewModels.FirstOrDefault() is TaskObjectViewModel firstTaskViewModel)
                tasksExportList.SelectedItem = firstTaskViewModel;
        }

        private IEnumerable<SectionViewModel> GetSelectedSections() => sectionsExportList.SelectedItems.OfType<SectionViewModel>();

        private IEnumerable<TaskObjectViewModel> GetSelectedTasks() => tasksExportList.SelectedItems.OfType<TaskObjectViewModel>();

        protected override void ButtonOKClick(object sender, RoutedEventArgs e)
        {
            if (!ValidateOK())
                return;

            var errors = new StringBuilder();
            bool hasSerializedObjects = false;

            if (backup.IsChecked != true)
            {
                var objectsToSerialize = new List<BaseObject>();
                string extension = Constants.XmlExtension;

                if (exportSections.IsChecked == true)
                {
                    objectsToSerialize = [.. GetSelectedSections().Select(vm => vm.Section)];
                    extension = Constants.SectionDataExtension;
                }
                else if (exportTasks.IsChecked == true)
                {
                    objectsToSerialize = [.. GetSelectedTasks().Select(vm => vm.TaskObject)];
                    extension = Constants.TaskDataExtension;
                }

                if (objectsToSerialize.Count == 0)
                {
                    UIHelper.ShowMessage("Выберите объекты для экспорта", MessageBoxImage.Warning);
                    return;
                }

                var saveDialog = new OpenFolderDialog();

                if (saveDialog.ShowDialog() != true)
                    return;

                string targetFolder = saveDialog.FolderName;

                foreach (var @object in objectsToSerialize)
                {
                    string fileName = useName.IsChecked == true ? @object.Name + extension : @object.FileName;
                    string resultPath = Path.Combine(targetFolder, fileName);

                    try
                    {
                        if (replaceExists.IsChecked == true || !File.Exists(resultPath))
                        {
                            @object.Serialize(resultPath);
                            hasSerializedObjects = true;
                        }
                    }
                    catch
                    {
                        if (errors.Length == 0)
                            errors.AppendLine("Ошибка сохранения следующих объектов:");

                        errors.AppendLine(@object.Name);
                    }
                }
            }

            DialogResult = true;
            Close();

            if (backup.IsChecked == true)
            {
                if (DataHelper.SaveData(Enums.DataDirectory.BackupWithDate, out string resultDirectory))
                   UIHelper.ShowMessage($"{_successMessage} в папку {resultDirectory}", MessageBoxImage.Information);
            }
            else
            {
                if (errors.Length > 0)
                    UIHelper.ShowMessage(errors.ToString(), MessageBoxImage.Warning);
                else if (hasSerializedObjects)
                    UIHelper.ShowMessage(_successMessage, MessageBoxImage.Information);
            }
        }

        private void ExportTasksChecked(object sender, RoutedEventArgs e)
        {
            if (!IsLoaded)
                return;

            parametersGroup.Visibility = Visibility.Visible;
            tasksExportList.Visibility = Visibility.Visible;
            sectionsExportList.Visibility = Visibility.Visible;
        }

        private void ExportSectionsChecked(object sender, RoutedEventArgs e)
        {
            if (!IsLoaded)
                return;

            parametersGroup.Visibility = Visibility.Visible;
            tasksExportList.Visibility = Visibility.Hidden;
            sectionsExportList.Visibility = Visibility.Visible;
        }

        private void BackupChecked(object sender, RoutedEventArgs e)
        {
            if (!IsLoaded)
                return;

            parametersGroup.Visibility = Visibility.Hidden;
            tasksExportList.Visibility = Visibility.Hidden;
            sectionsExportList.Visibility = Visibility.Hidden;
        }
    }
}
