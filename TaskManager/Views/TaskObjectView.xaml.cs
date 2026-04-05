using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TaskManager.Resources;
using TaskManager.ViewModels;

namespace TaskManager.Views
{
    /// <summary>
    /// Interaction logic for TaskObjectView.xaml
    /// </summary>
    public partial class TaskObjectView : UserControl
    {
        private string _startDescription;
        private TaskObjectViewModel _taskObjectViewModel;

        // Конструктор по умолчанию нужен для корректного отображения при использовании в xaml
        public TaskObjectView()
        {
            InitializeComponent();
        }

        // TODO: Скрыть контекстное меню для текстблоков
        internal TaskObjectView(TaskObjectViewModel taskObjectViewModel) : this()
        {
            DataContext = _taskObjectViewModel = taskObjectViewModel;
        }

        private void EditDescription(object sender, RoutedEventArgs e)
        {
            if (descriptionField.IsReadOnly)
                OpenEditDescription();
            else
                CloseEditDescription();
        }

        private void CancelEditDescription(object sender, RoutedEventArgs e) => CloseEditDescription(true);

        internal void OpenEditDescription()
        {
            if (!descriptionField.IsReadOnly)
                return;

            _startDescription = descriptionField.Text;
            descriptionField.IsReadOnly = false;
            descriptionField.Background = Brushes.White;
            cancelButton.Visibility = Visibility.Visible;
            editButton.Content = "Сохранить";
        }

        internal void CloseEditDescription(bool cancelChanges = false)
        {
            if (descriptionField.IsReadOnly)
                return;

            if (cancelChanges)
                descriptionField.Text = _startDescription;

            _startDescription = String.Empty;
            descriptionField.IsReadOnly = true;
            descriptionField.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xC8, 0xC8, 0xC8)); // Светло-серый
            cancelButton.Visibility = Visibility.Collapsed;
            editButton.Content = "Редактировать";
        }

        private void AddComment(object sender, RoutedEventArgs e)
        {
            var newCommentWindow = new AddCommentWindow(_taskObjectViewModel);

            if (newCommentWindow.ShowDialog() != true)
                return;

            commentsField.Text += $"[{DateTime.Now}]" + Environment.NewLine
                + newCommentWindow.Comment + Environment.NewLine
                + Constants.DashSeparator40 + Environment.NewLine;
        }
    }
}
