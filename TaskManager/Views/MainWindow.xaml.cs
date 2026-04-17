using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Animation;
using TaskManager.Commands;
using TaskManager.CustomControls;
using TaskManager.Helpers;
using TaskManager.Model;
using TaskManager.Model.TaskStatuses;
using TaskManager.ViewModels;

namespace TaskManager.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : CustomWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = MainViewModel = new MainViewModel();
            InitializeData();
        }

        internal MainViewModel MainViewModel { get; }

        // Добавляем тут, а не в конструкторе MainViewModel, т.к. команда добавления обращается к MainViewModel
        private void InitializeData()
        {
            var masterSectionViewModel = MainViewModel.CreateMasterSection();

            // Устанавливаем для вкладки, чтобы распространялось ещё и на хедер
            masterSection.DataContext = masterSectionViewModel;
            var masterSectionView = UIHelper.GetSectionViewFromTabItem(masterSection);
            masterSectionView.InitializeData(masterSectionViewModel);

            MainViewModel.AddSectionViewModel(masterSectionViewModel);
            MainViewModel.SelectedSectionViewModel = masterSectionViewModel;

            NewSectionCommand.Test();
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
    }
}