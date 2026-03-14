using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
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

            InitializeData();
        }

        private void InitializeData()
        {
            DataContext = new MainViewModel();

            MainViewModel.NewSectionCommand.AddSection(true);
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
    }
}