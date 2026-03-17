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
            if (!ValidateName())
                return;

            DialogResult = true;
            Close();
        }

        private void ButtonCancelClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private bool ValidateName()
        {
            string name = taskName.Text;

            if (String.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Наименование задачи не может быть пустым");
                return false;
            }

            //if (Helper.MainWindow.GetSectionsNames([taskName.Section]).Contains(name))
            //{
            //    MessageBox.Show($"Раздел \"{name}\" уже существует");
            //    return false;
            //}

            return true;
        }

        internal void OpenEditDescription() => taskProperty.OpenEditDescription();

        //internal void CloseEditDescription() => taskProperty.CloseEditDescription();
    }
}
