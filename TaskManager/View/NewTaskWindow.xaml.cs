using System.Windows;
using TaskManager.Helpers;
using TaskManager.ViewModel;

namespace TaskManager.View
{
    /// <summary>
    /// Interaction logic for NewTaskWindow.xaml
    /// </summary>
    public partial class NewTaskWindow : Window
    {
        internal NewTaskWindow(TaskObjectViewModel taskObjectViewModel)
        {
            InitializeComponent();
            Owner = Helper.MainWindow;
            DataContext = taskObjectViewModel;
        }

        private void ButtonOKClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void ButtonCancelClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
