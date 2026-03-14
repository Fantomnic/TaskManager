using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TaskManager.Helpers;
using TaskManager.ViewModel;
using static TaskManager.Helpers.Enums;

namespace TaskManager.View
{
    /// <summary>
    /// Interaction logic for TaskObjectView.xaml
    /// </summary>
    public partial class TaskObjectView : UserControl
    {
        private string _startDescription;

        // Конструктор по умолчанию нужен для корректного отображения при использовании в xaml
        public TaskObjectView()
        {
            InitializeComponent();
        }

        // TODO: Скрыть контекстное меню для текстблоков
        internal TaskObjectView(TaskObjectViewModel taskObjectViewModel) : this()
        {
            DataContext = taskObjectViewModel;
        }

        private void EditDescription(object sender, RoutedEventArgs e)
        {
            if (descriptionField.IsReadOnly)
                OpenEditDescription();
            else
                CloseEditDescription();
        }

        private void CancelEditDescription(object sender, RoutedEventArgs e) => CloseEditDescription(true);

        private void OpenEditDescription()
        {
            _startDescription = descriptionField.Text;
            descriptionField.IsReadOnly = false;
            descriptionField.Background = Brushes.White;
            cancelButton.Visibility = Visibility.Visible;
            editButton.Content = "Сохранить";
        }

        private void CloseEditDescription(bool cancelChanges = false)
        {
            if (cancelChanges)
                descriptionField.Text = _startDescription;

            _startDescription = String.Empty;
            descriptionField.IsReadOnly = true;
            descriptionField.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xC8, 0xC8, 0xC8)); // Светло-серый
            cancelButton.Visibility = Visibility.Collapsed;
            editButton.Content = "Редактировать";
        }

        private void PrioritySelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var addedItems = e.AddedItems.OfType<TaskPriority>();

            if (addedItems.Any() && DataContext is TaskObjectViewModel taskObjectViewModel)
                taskObjectViewModel.SetPriority(addedItems.First());
        }

        private void StatusSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var addedItems = e.AddedItems.OfType<Enums.TaskStatus>();

            if (addedItems.Any() && DataContext is TaskObjectViewModel taskObjectViewModel)
                taskObjectViewModel.SetStatus(addedItems.First());
        }
    }
}
