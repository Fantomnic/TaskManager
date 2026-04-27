using System.IO;
using System.Runtime.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using TaskManager.Commands;
using TaskManager.Helpers;
using TaskManager.Model;
using TaskManager.Model.TaskStatuses;
using TaskManager.Resources;
using TaskManager.ViewModels;

namespace TaskManager.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : CustomWindow
    {
        private static bool _masterSectionWasInitialized;

        public MainWindow()
        {
            InitializeComponent();

            DataContext = MainViewModel = new MainViewModel();
        }

        internal MainViewModel MainViewModel { get; }

        private void MenuClick(object sender, RoutedEventArgs e) => StartMenuAnimation();

        private void MenuMouseLeave(object sender, MouseEventArgs e) => StartMenuAnimation(true);

        private void StartMenuAnimation(bool closing = false)
        {
            double time = closing ? 0.25 : 0.4;

            var menuAnimation = new DoubleAnimation
            {
                From = menu.ActualWidth,
                To = closing ? 0 : menuColumn.ActualWidth,
                Duration = TimeSpan.FromSeconds(time),
            };

            if (!closing)
                menuAnimation.EasingFunction = new QuadraticEase();

            menu.BeginAnimation(WidthProperty, menuAnimation);
        }

        // TODO: Событие вызывается также при смене селекции в дочернем листбоксе. Подумать, как это можно обойти
        private void SectionsSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as TabControl)?.SelectedItem is not TabItem selectedTabItem
                || Helper.GetSectionViewModelFromTabItem(selectedTabItem) is not SectionViewModel selectedSectionViewModel)
            {
                return;
            }

            var oldSectionViewModel = MainViewModel.SelectedSectionViewModel;

            if (oldSectionViewModel == selectedSectionViewModel)
            {
                e.Handled = true;
                return;
            }

            MainViewModel.SelectedSectionViewModel = selectedSectionViewModel;
            selectedSectionViewModel.RefreshVisibleTaskViewModels();
        }

        internal void SetMenuColumnWidth(double width) => menuColumn.MinWidth = width;

        // Добавляем тут, а не в конструкторе MainViewModel, т.к. команда добавления обращается к MainViewModel
        private void InitializeData()
        {
            if (_masterSectionWasInitialized)
                return;

            var masterSectionViewModel = MainViewModel.CreateMasterSection();
            InitializeMasterSectionView(masterSectionViewModel);
        }

        private void InitializeMasterSectionView(MasterSectionViewModel masterSectionViewModel)
        {
            if (_masterSectionWasInitialized)
                return;

            // Устанавливаем для вкладки, чтобы распространялось ещё и на хедер
            masterSection.DataContext = masterSectionViewModel;
            var masterSectionView = UIHelper.GetSectionViewFromTabItem(masterSection);
            masterSectionView.InitializeData(masterSectionViewModel);

            MainViewModel.AddSectionViewModel(masterSectionViewModel);
            MainViewModel.SelectedSectionViewModel = masterSectionViewModel;

            _masterSectionWasInitialized = true;
        }

        private void LoadData()
        {
            // Сначала копируем файлы из FinishedSections в SourceSections
            string finishedDirectory = Helper.GetDataDirectory(Enums.DataDirectory.FinishedSections, false);
            string sourceDirectory = Helper.GetDataDirectory(Enums.DataDirectory.SourceSections);

            List<string> sourceFiles;

            if (String.IsNullOrEmpty(finishedDirectory))
            {
                sourceFiles = [.. Helper.GetAppFiles(sourceDirectory).Select(f => f.FullName)];
            }
            else
            {
                var finishedFiles = Helper.GetAppFiles(finishedDirectory);
                ClearDirectory(sourceDirectory);
                sourceFiles = MoveToDirectory(finishedFiles, sourceDirectory);
            }

            if (sourceFiles.Count == 0)
                return;

            // Теперь считываем информацию
            string etalonMasterSectionFile = nameof(MasterSection) + Constants.DataExtension;

            var masterSectionFile = sourceFiles.FirstOrDefault(f => f.Contains(etalonMasterSectionFile));
            bool masterSectionInitialized = false;

            if (!String.IsNullOrEmpty(masterSectionFile))
            {
                try
                {
                    var masterSerialiser = new DataContractSerializer(typeof(MasterSection), [typeof(TaskObject)]);

                    using var stream = new FileStream(masterSectionFile, FileMode.Open);
                    var value = masterSerialiser.ReadObject(stream);

                    if (value is MasterSection masterSection)
                    {
                        var masterSectionViewModel = MainViewModel.CreateMasterSectionViewModel(masterSection);
                        InitializeMasterSectionView(masterSectionViewModel);
                        masterSectionInitialized = true;
                    }
                }
                catch
                {
                    // TODO
                }
            }

            if (!masterSectionInitialized)
                InitializeData();

            var additionalSectionsFiles = sourceFiles.Except([masterSectionFile]).ToList();

            if (additionalSectionsFiles.Count == 0)
                return;

            var additionalSerialiser = new DataContractSerializer(typeof(AdditionalSection), [typeof(TaskObject)]);
            var additionalSections = new List<AdditionalSection>();

            foreach (string sectionFile in additionalSectionsFiles)
            {
                try
                {
                    using var stream = new FileStream(sectionFile, FileMode.Open);
                    var value = additionalSerialiser.ReadObject(stream);

                    if (value is AdditionalSection additionalSection)
                        additionalSections.Add(additionalSection);
                }
                catch
                {
                    // TODO
                }
            }

            foreach (var additionalSection in additionalSections.OrderBy(s => s.CreationDate))
            {
                try
                {
                    var additionalSectionViewModel = MainViewModel.CreateAdditionalSectionViewModel(additionalSection);
                    NewSectionCommand.AddSectionCore(MainViewModel, sections.Items, additionalSectionViewModel, false);
                }
                catch
                {
                    // TODO
                }
            }
        }

        private static List<string> MoveToDirectory(List<FileInfo> finishedFiles, string targetDirectoryPath)
        {
            var result = new List<string>(finishedFiles.Count);

            foreach (var file in finishedFiles)
            {
                string fileName = file.Name;
                string newPath = Path.Combine(targetDirectoryPath, fileName);

                try
                {
                    File.Move(file.FullName, newPath, true);
                    result.Add(newPath);
                }
                catch
                {
                    // TODO
                }
            }

            return result;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            LoadData();
            InitializeData();
            Settings.FillFromConfig();
        }

        private void OnClosed(object sender, EventArgs e)
        {
            Settings.SaveToConfig();
            SaveData();
        }

        private void SaveData()
        {
            ClearDirectory(Helper.GetDataDirectory(Enums.DataDirectory.FinishedSections));

            foreach (var sections in MainViewModel.ModelData.AllSections)
                sections.Serialize(Enums.DataDirectory.FinishedSections);
        }

        private static void ClearDirectory(string directoryPath)
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

        #region Отображение

        private void ChB1Checked(object sender, RoutedEventArgs e)
            => SetNewVisible(TaskStatusesInstances.BeginingStatus, true);

        private void ChB1Unchecked(object sender, RoutedEventArgs e)
            => SetNewVisible(TaskStatusesInstances.BeginingStatus, false);

        private void ChB2Checked(object sender, RoutedEventArgs e)
            => SetNewVisible(TaskStatusesInstances.DeferredStatus, true);

        private void ChB2Unchecked(object sender, RoutedEventArgs e)
            => SetNewVisible(TaskStatusesInstances.DeferredStatus, false);

        private void ChB3Checked(object sender, RoutedEventArgs e)
            => SetNewVisible(TaskStatusesInstances.RejectedStatus, true);

        private void ChB3Unchecked(object sender, RoutedEventArgs e)
            => SetNewVisible(TaskStatusesInstances.RejectedStatus, false);

        private void ChB4Checked(object sender, RoutedEventArgs e)
            => SetNewVisible(TaskStatusesInstances.DoneStatus, true);

        private void ChB4Unchecked(object sender, RoutedEventArgs e)
            => SetNewVisible(TaskStatusesInstances.DoneStatus, false);

        private void ChB5Checked(object sender, RoutedEventArgs e)
            => SetNewVisible(TaskStatusesInstances.CompletedStatus, true);

        private void ChB5Unchecked(object sender, RoutedEventArgs e)
            => SetNewVisible(TaskStatusesInstances.CompletedStatus, false);

        private void SetNewVisible(TaskStatusBase status,  bool visible)
        {
            if (!IsLoaded)
                return;

            status.TaskVisible = visible;
            MainViewModel.SelectedSectionViewModel.RefreshVisibleTaskViewModels();
        }

        #endregion Отображение
    }
}