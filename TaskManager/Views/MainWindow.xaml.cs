using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
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

            LoadData();
            InitializeData();
        }

        internal MainViewModel MainViewModel { get; }

        // Добавляем тут, а не в конструкторе MainViewModel, т.к. команда добавления обращается к MainViewModel
        private void InitializeData()
        {
            if (_masterSectionWasInitialized)
                return;

            var masterSectionViewModel = MainViewModel.CreateMasterSection();
            InitializeMasterSectionView(masterSectionViewModel);

            //NewSectionCommand.Test();
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
            string dataDirectory = Helper.GetDataDirectory(Enums.DataDirectory.Root, false);

            if (String.IsNullOrEmpty(dataDirectory))
                return;

            var allSectionsFiles = Directory.GetFiles(dataDirectory).Where(f => String.Equals(Path.GetExtension(f), Constants.DataExtension)).ToList();

            if (allSectionsFiles.Count == 0)
                return;

#pragma warning disable SYSLIB0011 // Type or member is obsolete
            var serialiser = new BinaryFormatter();
#pragma warning restore SYSLIB0011 // Type or member is obsolete

            var additionalSections = new List<AdditionalSection>();

            // Первым должен инициализоваться основной раздел
            foreach (string sectionFile in allSectionsFiles)
            {
                using var stream = new FileStream(sectionFile, FileMode.OpenOrCreate);
                var value = serialiser.Deserialize(stream);

                if (value is AdditionalSection additionalSection)
                {
                    additionalSections.Add(additionalSection);
                }
                else if (value is MasterSection masterSection)
                {
                    var masterSectionViewModel = MainViewModel.CreateMasterSectionViewModel(masterSection);
                    InitializeMasterSectionView(masterSectionViewModel);
                };
            }

            InitializeData();

            foreach (var additionalSection in additionalSections)
            {
                var additionalSectionViewModel = MainViewModel.CreateAdditionalSectionViewModel(additionalSection);
                NewSectionCommand.AddSectionCore(MainViewModel, sections.Items, additionalSectionViewModel, false);
            }
        }

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

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            Settings.FillFromConfig();
        }

        private void OnClosed(object sender, EventArgs e)
        {
            Settings.SaveToConfig();
        }

        #region Фильтры

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

        private void ChBTodayChecked(object sender, RoutedEventArgs e)
            => SetNewTodayTasksVisible(true);

        private void ChBTodayUnchecked(object sender, RoutedEventArgs e)
            => SetNewTodayTasksVisible(false);

        private void SetNewTodayTasksVisible(bool visible)
        {
            if (!IsLoaded)
                return;

            Settings.Instanse.ShowTodayTasks = visible;
            MainViewModel.SelectedSectionViewModel.RefreshVisibleTaskViewModels();
        }

        #endregion Фильтры
    }
}