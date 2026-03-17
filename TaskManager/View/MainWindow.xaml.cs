using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using TaskManager.Helpers;
using TaskManager.Model;
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

            DataContext = MainViewModel;
            InitializeData();
        }

        internal MainViewModel MainViewModel { get; } = new MainViewModel();

        private void InitializeData()
        {
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

        private ObservableCollection<Section> GetSectionsCollection() => MainViewModel.Sections;

        internal void AddSection(Section section) => GetSectionsCollection().Add(section);

        internal void RemoveSection(Section section) => GetSectionsCollection().Remove(section);

        internal List<string> GetSectionsNames(IEnumerable<Section>? ignoredSections = null)
        {
            var sections = GetSectionsCollection().ToList();

            if (ignoredSections is not null)
                sections = [.. sections.Where(s => !ignoredSections.Contains(s))];

            return [.. sections.Select(s => s.Name)];
        }

        // TODO: Событие вызывается также при смене селекции в дочернем листбоксе. Подумать, как это можно обойти
        private void SectionsSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Helper.GetSectionFromTabItem((sender as TabControl)?.SelectedItem as TabItem) is not Section selectedSection)
                return;

            MainViewModel.SelectedSection = selectedSection;
        }
    }
}