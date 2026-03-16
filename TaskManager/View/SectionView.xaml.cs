using System.Windows.Controls;
using TaskManager.ViewModel;

namespace TaskManager.View
{
    /// <summary>
    /// Interaction logic for TasksControl.xaml
    /// </summary>
    public partial class SectionView : UserControl
    {
        internal SectionView(SectionViewModel sectionViewModel)
        {
            InitializeComponent();

            DataContext = sectionViewModel;
        }
    }
}
