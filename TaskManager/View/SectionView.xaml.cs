using System.Windows;
using System.Windows.Controls;
using TaskManager.Helpers;
using TaskManager.Model;
using TaskManager.ViewModel;

namespace TaskManager.View
{
    /// <summary>
    /// Interaction logic for TasksControl.xaml
    /// </summary>
    public partial class SectionView : UserControl
    {
        private readonly SectionViewModel _sectionViewModel;

        internal SectionView(SectionViewModel sectionViewModel)
        {
            InitializeComponent();

            DataContext = _sectionViewModel = sectionViewModel;
        }

        private void ListBoxPreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            // Проверяем, что
            if (sender is ListBox listBox // Кликаем по списку
                && listBox.SelectedItem is TaskObject // Есть выбранная задача
                && ItemsControl.ContainerFromElement(listBox, e.OriginalSource as DependencyObject) is not ListBoxItem) // Кликаем не по элементу списка
            {
                _sectionViewModel.SelectedObject = null;
            }
        }
    }
}
