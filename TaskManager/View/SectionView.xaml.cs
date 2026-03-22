using System.Windows.Controls;
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

        private void ListBoxMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (sender is ListBox listBox)
                listBox.UnselectAll();
        }

        private void TasksListContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            if (sender is ListBox listBox && listBox.SelectedItem is TaskObjectViewModel taskObjectViewModel)
                SectionViewModel.RefreshChangeSectionEnabled(taskObjectViewModel);
        }
    }
}
