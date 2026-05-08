using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using TaskManager.Commands;
using TaskManager.Helpers;
using TaskManager.Model;
using TaskManager.Model.TaskStatuses;
using TaskManager.ViewModels;
using static TaskManager.Helpers.Enums;

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

        private void MenuClick(object sender, RoutedEventArgs e)
        {
            StartMenuAnimation();
            ResetMenuButtonsFocus();
            SetMenuButtonsFocusable(false);
        }

        private void MenuMouseLeave(object sender, MouseEventArgs e)
        {
            StartMenuAnimation(true);
            SetMenuButtonsFocusable(true);
        }

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

            InitializeMasterSectionViewCore(masterSectionViewModel);

            MainViewModel.AddSectionViewModel(masterSectionViewModel);
            MainViewModel.SelectedSectionViewModel = masterSectionViewModel;

            _masterSectionWasInitialized = true;
        }

        internal void InitializeMasterSectionViewCore(MasterSectionViewModel masterSectionViewModel)
        {
            // Устанавливаем для вкладки, чтобы распространялось ещё и на хедер
            masterSection.DataContext = masterSectionViewModel;
            var masterSectionView = UIHelper.GetSectionViewFromTabItem(masterSection);
            masterSectionView.InitializeData(masterSectionViewModel);
        }

        private void LoadData()
        {
            // Сначала копируем файлы из FinishedSections в SourceSections
            string finishedDirectory = DataHelper.GetDataDirectory(DataDirectory.FinishedSections, false);
            string sourceDirectory = DataHelper.GetDataDirectory(DataDirectory.SourceSections);

            List<string> sourceFiles;
            var errors = new StringBuilder();

            if (String.IsNullOrEmpty(finishedDirectory))
            {
                sourceFiles = [.. Helper.GetAppFiles(sourceDirectory).Select(f => f.FullName)];
            }
            else
            {
                var finishedFiles = Helper.GetAppFiles(finishedDirectory);

                if (finishedFiles.Count == 0)
                {
                    sourceFiles = [.. Helper.GetAppFiles(sourceDirectory).Select(f => f.FullName)];
                }
                else
                {
                    DataHelper.ClearDirectory(sourceDirectory);
                    sourceFiles = MoveToDirectory(finishedFiles, sourceDirectory, ref errors);
                }
            }

            LoadDataCore(sourceFiles, ref errors);

            if (errors.Length > 0)
                UIHelper.ShowMessage(errors.ToString(), MessageBoxImage.Error, "Ошибка загрузки данных");
        }

        internal void LoadDataCore(List<string> sourceFiles, ref StringBuilder errors)
        {
            if (sourceFiles.Count == 0)
                return;

            // Теперь считываем информацию
            string etalonMasterSectionFile = MasterSection.GetFileName();

            var masterSectionFile = sourceFiles.FirstOrDefault(f => f.Contains(etalonMasterSectionFile));
            bool masterSectionInitialized = false;

            if (!String.IsNullOrEmpty(masterSectionFile))
            {
                try
                {
                    if (DataHelper.TryGetObjectFromFile<MasterSection>(masterSectionFile, out var masterSection, out _))
                    {
                        CreateAndInitializeMasterSectionViewModel(masterSection);
                        masterSectionInitialized = true;
                    }
                }
                catch
                {
                    errors.AppendLine("Не удалось загрузить данные основного раздела");
                }
            }

            if (!masterSectionInitialized)
                InitializeData();

            var additionalSectionsFiles = sourceFiles.Except([masterSectionFile]).ToList();

            if (additionalSectionsFiles.Count == 0)
                return;

            if (!DataHelper.TryGetObjectsFromFiles<AdditionalSection>(additionalSectionsFiles, out var additionalSections, out string errorMessage, out _, false))
                errors.AppendLine(errorMessage);

            LoadAdditionalSectionsCore(additionalSections, ref errors);
        }

        internal void CreateAndInitializeMasterSectionViewModel(MasterSection masterSection, bool replace = false)
        {
            var masterSectionViewModel = MainViewModel.CreateMasterSectionViewModel(masterSection);

            if (replace)
            {
                InitializeMasterSectionViewCore(masterSectionViewModel);
                MainViewModel.ReplaceMasterSectionViewModel(masterSectionViewModel);
                masterSectionViewModel.RefreshVisibleTaskViewModels();
            }
            else
            {
                InitializeMasterSectionView(masterSectionViewModel);
            }
        }

        internal void LoadAdditionalSectionsCore(List<AdditionalSection> additionalSections, ref StringBuilder errors)
        {
            foreach (var additionalSection in additionalSections.OrderBy(s => s.CreationDate))
            {
                try
                {
                    var additionalSectionViewModel = MainViewModel.CreateAdditionalSectionViewModel(additionalSection);
                    NewSectionCommand.AddSectionCore(MainViewModel, sections.Items, additionalSectionViewModel, false);
                }
                catch
                {
                    errors.AppendLine($"Ошибка добавления раздела \"{additionalSection.Name}\"");
                }
            }
        }

        private static List<string> MoveToDirectory(List<FileInfo> finishedFiles, string targetDirectoryPath, ref StringBuilder errors)
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
                    errors.AppendLine($"Ошибка перемещения файла \"{file.Name}\"");
                }
            }

            return result;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            LoadData();
            InitializeData();
            Settings.FillFromConfig();
            UpdateDateTimer.Start();

            DataHelper.DataIsLoaded = true;

            FillFromConfigForMainWindow();
        }

        private void OnClosing(object sender, CancelEventArgs e)
        {
            SaveToConfigForMainWindow();
            Settings.SaveToConfig();

            if (DataHelper.SaveData(DataDirectory.FinishedSections))
                DataHelper.DataIsSaved = true;
        }

        private void FillFromConfigForMainWindow()
        {
            if (Settings.AppSettings.StartOnFullWindow)
                MaximizeCore();

            if (MainViewModel.FindSectionViewModel(Settings.AppSettings.InitialSelectedSection) is AdditionalSectionViewModel initialSectionViewModel)
                sections.SelectedItem = UIHelper.GetTabItemWithSectionViewModel(initialSectionViewModel);
        }

        private void SaveToConfigForMainWindow()
        {
            if (WindowState == WindowState.Maximized)
                Settings.AppSettings.StartOnFullWindow = true;
            else if (WindowState == WindowState.Normal)
                Settings.AppSettings.StartOnFullWindow = false;

            Settings.AppSettings.InitialSelectedSection = MainViewModel.SelectedSectionViewModel.Section.Guid;
        }

        private void WindowMouseDown(object sender, MouseButtonEventArgs e)
        {
            ResetMenuButtonsFocus();
        }

        internal void ResetMenuButtonsFocus()
        {
            UIElement focusedButton;

            if (createTaskButton.IsFocused)
                focusedButton = createTaskButton;
            else if (actionButton.IsFocused)
                focusedButton = actionButton;
            else if (settingsButton.IsFocused)
                focusedButton = settingsButton;
            else if (helpButton.IsFocused)
                focusedButton = helpButton;
            else
                return;

            UIHelper.ResetFocus(focusedButton);
        }

        internal void SetMenuButtonsFocusable(bool value)
        {
            createTaskButton.Focusable = value;
            actionButton.Focusable = value;
            settingsButton.Focusable = value;
            helpButton.Focusable = value;
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