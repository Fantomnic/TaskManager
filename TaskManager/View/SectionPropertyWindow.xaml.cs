using System.Windows;
using TaskManager.Helpers;
using TaskManager.ViewModel;

namespace TaskManager.View
{
    /// <summary>
    /// Interaction logic for SectionPropertyWindow.xaml
    /// </summary>
    public partial class SectionPropertyWindow : Window
    {
        private readonly SectionViewModel _sectionViewModel;

        internal SectionPropertyWindow(SectionViewModel sectionViewModel, Window? owner = null)
        {
            InitializeComponent();
            Owner = owner ?? Helper.MainWindow;
            DataContext = _sectionViewModel = sectionViewModel;
        }

        private void ButtonOKClick(object sender, RoutedEventArgs e)
        {
            if (!ValidateName())
                return;

            DialogResult = true;
            Close();
        }

        private void ButtonCancelClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private bool ValidateName()
        {
            string name = sectionName.Text;

            if (String.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Укажите название раздела");
                return false;
            }

            if (Helper.MainWindow.GetSectionsNames([_sectionViewModel.Section]).Contains(name))
            {
                MessageBox.Show($"Раздел \"{name}\" уже существует");
                return false;
            }

            return true;
        }
    }
}
