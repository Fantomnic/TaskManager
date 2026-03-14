using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TaskManager.ViewModel;

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
        internal TaskObjectView(TaskObjectViewModel taskObjectViewModel)
        {
            InitializeComponent();

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
    }
}
