using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
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

            DataContext = new MainViewModel();
        }

        private void menu_Click(object sender, RoutedEventArgs e) => StartMenuAnimation();

        private void menu_MouseLeave(object sender, MouseEventArgs e) => StartMenuAnimation(true);

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

            menu.BeginAnimation(Button.WidthProperty, menuAnimation);
        }

        private void newSection_Click(object sender, RoutedEventArgs e)
        {
            var windowProperty = new SectionPropertyWindow
            {
                Owner = this
            };

            if (windowProperty.ShowDialog() != true)
                return;

            string name = windowProperty.sectionName.Text;

            var sectionViewModel = new SectionViewModel(name);

            var textBlock = new TextBlock()
            {
                DataContext = sectionViewModel,
                //Text = "New",
                ContextMenu = CreateSectionContextMenu(),
            };

            var binding = new Binding
            {
                Path = new PropertyPath(nameof(sectionViewModel.Name))
            };

            textBlock.SetBinding(TextBlock.TextProperty, binding);

            var newItem = new TabItem()
            {
                DataContext = sectionViewModel,
                Header = textBlock,
                Content = new SectionView(),
            };

            FillSectionCommandsBindings(newItem);

            sections.Items.Add(newItem);
            newItem.Focus();
        }

        private ContextMenu CreateSectionContextMenu()
        {
            var menu = new ContextMenu();
            menu.Items.Add(new MenuItem() { Command = Commands0.DeleteSectionCommand });
            menu.Items.Add(new MenuItem() { Header = "Свойства" });
            return menu;
        }

        private void FillSectionCommandsBindings(TabItem section)
        {
            var deleteBinding = new CommandBinding()
            {
                Command = Commands0.DeleteSectionCommand,
            };

            deleteBinding.Executed += CommandBinding_Executed;
            deleteBinding.CanExecute += CommandBinding_CanExecute;
            section.CommandBindings.Add(deleteBinding);
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var tabItem = (TabItem)sender;

            sections.Items.Remove(tabItem);
        }

        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
    }
}