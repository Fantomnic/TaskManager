using System.Windows;
using TaskManager.Helpers;
using TaskManager.ViewModel;

namespace TaskManager.View
{
    /// <summary>
    /// Interaction logic for NewTaskWindow.xaml
    /// </summary>
    public partial class NewTaskWindow : WindowWithBottomButtons
    {
        internal NewTaskWindow()
        {
            InitializeComponent();
            var newTaskObjectViewModel = SectionViewModel.CreateTask();
            DataContext = NewTaskObjectViewModel = newTaskObjectViewModel;
        }

        internal TaskObjectViewModel NewTaskObjectViewModel { get; }

        protected override bool ValidateOK()
        {
            string name = taskName.Text;

            if (String.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Наименование задачи не может быть пустым");
                return false;
            }

            if (Helper.GetAllTasks().Select(t => t.Name).Contains(name))
            {
                MessageBox.Show($"Задача с наименованием \"{name}\" уже существует");
                return false;
            }

            return true;
        }

        internal void OpenEditDescription() => taskProperty.OpenEditDescription();
    }
}
