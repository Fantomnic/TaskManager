using System.Windows.Controls;
using System.Windows.Input;
using TaskManager.ViewModel;

namespace TaskManager.View
{
    /// <summary>Общий класс для представлений разделов</summary>
    public abstract class SectionView : UserControl
    {
        internal SectionView()
        {

        }

        internal SectionView(SectionViewModel sectionViewModel)
        {
            DataContext = sectionViewModel;
        }

        protected void ListBoxMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListBox listBox)
                listBox.UnselectAll();
        }
    }
}