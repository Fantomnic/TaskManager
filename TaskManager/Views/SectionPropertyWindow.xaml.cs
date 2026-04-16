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
                UIHelper.ShowMessage("Укажите название раздела", MessageBoxImage.Warning);
                return false;
            }

            if (Helper.MainViewModel.GetSectionsNames([_sectionViewModel.Section]).Contains(name))
            {
                UIHelper.ShowMessage($"Раздел \"{name}\" уже существует", MessageBoxImage.Warning);
                return false;
            }

            NewSectionName = name;

            return true;
        }
    }
}
