using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using TaskManager.Commands;
using TaskManager.Helpers;
using TaskManager.Resources;
using TaskManager.ViewModel;

namespace TaskManager.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //Height = Constants.StartHeightMainWindow;
            //Width = Constants.StartWidthMainWindow;

            DataContext = MainViewModel = new MainViewModel();
            InitializeData();
        }

        internal MainViewModel MainViewModel { get; }

        // Добавляем тут, а не в конструкторе MainViewModel, т.к. команда добавления обращается к MainViewModel
        private void InitializeData()
        {
            CommandsInstances.NewSectionCommand.AddSection(true);
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
            if (Helper.GetSectionViewModelFromTabItem((sender as TabControl)?.SelectedItem as TabItem) is not SectionViewModel selectedSectionViewModel)
                return;

            MainViewModel.SelectedSectionViewModel = selectedSectionViewModel;
        }
    }
}