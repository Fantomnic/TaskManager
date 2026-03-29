using System.Windows.Controls;
using System.Windows.Input;
using TaskManager.ViewModel;

namespace TaskManager.View
{
    /// <summary>Общий класс для представлений разделов</summary>
    public abstract class SectionView : UserControl
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        internal SectionView()

        {

        }

        internal SectionView(SectionViewModel sectionViewModel)
        {
            InitializeData(sectionViewModel);
        }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        internal SectionViewModel SectionViewModel { get; private set; }

        internal void InitializeData(SectionViewModel masterSectionViewModel)
        {
            DataContext = SectionViewModel = masterSectionViewModel;
        }

        protected void ListBoxMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListBox listBox)
                listBox.UnselectAll();
        }
    }
}