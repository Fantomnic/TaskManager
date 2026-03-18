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

        private void ListBoxMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (sender is ListBox listBox)
                listBox.UnselectAll();
        }
    }
}
