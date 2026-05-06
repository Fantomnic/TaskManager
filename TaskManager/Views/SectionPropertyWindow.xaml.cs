using System.Windows;
using TaskManager.Helpers;
using TaskManager.Model;
using TaskManager.Model.TaskPriorities;
using TaskManager.ViewModels;

namespace TaskManager.Views
{
    /// <summary>
    /// Interaction logic for SectionPropertyWindow.xaml
    /// </summary>
    public partial class SectionPropertyWindow : WindowWithBottomButtons
    {
        private readonly SectionViewModel _sectionViewModel;

        internal SectionPropertyWindow(SectionViewModel sectionViewModel, bool isNew = false)
        {
            InitializeComponent();
            Title = isNew ? "Новый раздел"
                : sectionViewModel.IsMasterSection ? "Свойства основного раздела" : "Свойства раздела";
            DataContext = _sectionViewModel = sectionViewModel;
            InitializeFields();

            UIHelper.SetFocus(sectionName);
        }

        private void InitializeFields()
        {
            sectionName.Text = _sectionViewModel.Name;
            commentField.Text = _sectionViewModel.Comment;
            priorityList.SelectedItem = TaskPrioritiesInstances.GetVisiblePriority(_sectionViewModel.DefaultPriority);
            typeList.SelectedItem = _sectionViewModel.DefaultTaskType;

            endDateCounter.SetNewValue(_sectionViewModel.DefaultReleaseDays);
        }

        private void OnCloseWithOK()
        {
            _sectionViewModel.Name = sectionName.Text;
            _sectionViewModel.Comment = commentField.Text;
            _sectionViewModel.DefaultPriority = (TaskPriorityBase)priorityList.SelectedItem;
            _sectionViewModel.DefaultTaskType = (Enums.TaskType)typeList.SelectedItem;
            _sectionViewModel.DefaultReleaseDays = endDateCounter.Value;
        }

        protected override bool ValidateOK()
        {
            string name = sectionName.Text;

            if (String.IsNullOrWhiteSpace(name))
            {
                UIHelper.ShowMessage("Укажите название раздела", MessageBoxImage.Warning);
                return false;
            }

            if (name.Length > Settings.MaxSectionLength)
            {
                UIHelper.ShowMessage($"Наименование раздела не может превышать длину в {Helper.GetNSymbolsString(Settings.MaxSectionLength)}", MessageBoxImage.Warning);
                return false;
            }

            if (Helper.MainViewModel.GetSectionsNames([_sectionViewModel.Section]).Contains(name))
            {
                UIHelper.ShowMessage($"Раздел \"{name}\" уже существует", MessageBoxImage.Warning);
                return false;
            }

            OnCloseWithOK();

            return true;
        }
    }
}
