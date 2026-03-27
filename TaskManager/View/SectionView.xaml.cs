using System.Windows.Controls;
using TaskManager.ViewModel;

namespace TaskManager.View
{
    /// <summary>
    /// Interaction logic for TasksControl.xaml
    /// </summary>
    public partial class SectionView : UserControl
    {
        private readonly AdditionalSectionViewModel _sectionViewModel;

        internal SectionView(AdditionalSectionViewModel sectionViewModel)
        {
            InitializeComponent();

            DataContext = _sectionViewModel = sectionViewModel;
        }

        private void ListBoxMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (sender is ListBox listBox)
                listBox.UnselectAll();
        }
    }
}
