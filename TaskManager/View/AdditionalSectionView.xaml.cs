using System.Windows.Controls;
using TaskManager.ViewModel;

namespace TaskManager.View
{
    /// <summary>
    /// Interaction logic for TasksControl.xaml
    /// </summary>
    public partial class AdditionalSectionView : SectionView
    {
        internal AdditionalSectionView(AdditionalSectionViewModel sectionViewModel) : base(sectionViewModel)
        {
            InitializeComponent();
        }
    }
}
