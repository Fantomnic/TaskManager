using System.Windows;
using TaskManager.Helpers;
using TaskManager.ViewModels;

namespace TaskManager.Views
{
    /// <summary>
    /// Interaction logic for SectionPropertyWindow.xaml
    /// </summary>
    public partial class SectionPropertyWindow : WindowWithBottomButtons
    {
        private readonly SectionViewModel _sectionViewModel;

        internal SectionPropertyWindow(SectionViewModel sectionViewModel)
        {
            InitializeComponent();
            DataContext = _sectionViewModel = sectionViewModel;
            sectionName.Text = NewSectionName = sectionViewModel.Name;
        }

        internal string NewSectionName { get; set; }

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

            NewSectionName = name;

            return true;
        }
    }
}
