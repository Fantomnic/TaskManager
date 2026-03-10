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
        // public конструктор нужен для корректного отображения при использовании в xaml
        public TaskObjectView()
        {
            InitializeComponent();
        }

        internal TaskObjectView(TaskObjectViewModel taskObjectViewModel)
        {
            InitializeComponent();

            DataContext = taskObjectViewModel;
        }

        private void EditDescription(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;

            bool isReadonlyNew = !descriptionField.IsReadOnly;

            descriptionField.IsReadOnly = isReadonlyNew;

            descriptionField.Background = isReadonlyNew
                ? new SolidColorBrush(Color.FromArgb(0xFF, 0xC8, 0xC8, 0xC8))
                : Brushes.White;

            button.Content = isReadonlyNew ? "Редактировать" : "Сохранить";
        }
    }
}
