using System.Windows;
using TaskManager.Helpers;
using TaskManager.ViewModel;

namespace TaskManager.View
{
    /// <summary>
    /// Interaction logic for SectionPropertyWindow.xaml
    /// </summary>
    public partial class SectionPropertyWindow : WindowWithBottomButtons
    {
        private readonly SectionViewModel _sectionViewModel;

        internal SectionPropertyWindow(SectionViewModel sectionViewModel, Window? owner = null)
        {
            InitializeComponent();
            DataContext = _sectionViewModel = sectionViewModel;
        }

        protected override bool ValidateOK()
        {
            string name = sectionName.Text;

            if (String.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Укажите название раздела");
                return false;
            }

            if (Helper.MainViewModel.GetSectionsNames([_sectionViewModel.Section]).Contains(name))
            {
                MessageBox.Show($"Раздел \"{name}\" уже существует");
                return false;
            }

            return true;
        }
    }
}
