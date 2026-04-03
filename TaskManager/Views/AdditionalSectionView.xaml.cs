using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TaskManager.ViewModels;

namespace TaskManager.Views
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

        // Прим.: у TreeView SelectedItem не имеет сеттера, поэтому нельзя создать привязку к нему
        // Альтернативный вариант: https://stackoverflow.com/questions/1000040/data-binding-to-selecteditem-in-a-wpf-treeview
        private void StretchingTreeViewSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            SectionViewModel.SelectedTaskViewModel = (TaskObjectViewModel)e.NewValue;
        }

        protected override void TasksContainerMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is StretchingTreeView { SelectedItem: not null } && e.OriginalSource is Grid)
                SectionViewModel.SelectedTaskViewModel = null;
        }
    }
}
