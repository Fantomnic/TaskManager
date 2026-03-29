using System.Windows.Controls;
using TaskManager.ViewModel;

namespace TaskManager.View
{
    /// <summary>
    /// Interaction logic for MasterSectionView.xaml
    /// </summary>
    public partial class MasterSectionView : SectionView
    {
        // DataContext задаётся в MainWindow (вызывается InitializeData)
        public MasterSectionView() : base()
        {
            InitializeComponent();
        }

        private void TasksListContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            if (sender is ListBox listBox && listBox.SelectedItem is TaskObjectViewModel taskObjectViewModel)
                MasterSectionViewModel.RefreshChangeSectionEnabled(taskObjectViewModel);
        }
    }
}
