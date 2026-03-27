using System.Windows.Controls;
using System.Windows.Input;
using TaskManager.ViewModel;

namespace TaskManager.View
{
    /// <summary>
    /// Interaction logic for MasterSectionView.xaml
    /// </summary>
    public partial class MasterSectionView : UserControl
    {
        // DataContext задаётся в MainView
        public MasterSectionView()
        {
            InitializeComponent();
        }

        private void ListBoxMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListBox listBox)
                listBox.UnselectAll();
        }

        private void TasksListContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            if (sender is ListBox listBox && listBox.SelectedItem is TaskObjectViewModel taskObjectViewModel)
                MasterSectionViewModel.RefreshChangeSectionEnabled(taskObjectViewModel);
        }
    }
}
