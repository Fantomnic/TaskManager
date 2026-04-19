using System.Windows;
using TaskManager.Helpers;
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
        private string _newSectionName;
        private string _newComment;
        private TaskPriorityBase _newDefaultPriority;
        private Enums.TaskType _newDefaultTaskType;

        internal SectionPropertyWindow(SectionViewModel sectionViewModel, bool isNew = false)
        {
            InitializeComponent();
            Title = isNew ? "Новый раздел"
                : sectionViewModel.IsMasterSection ? "Свойства основного раздела" : "Свойства раздела";
            DataContext = _sectionViewModel = sectionViewModel;
            InitializeFields();
        }

        private void InitializeFields()
        {
            sectionName.Text = _newSectionName = _sectionViewModel.Name;
            commentField.Text = _newComment = _sectionViewModel.Comment;
            priorityList.SelectedItem = _newDefaultPriority = _sectionViewModel.DefaultPriority;
            typeList.SelectedItem = _newDefaultTaskType = _sectionViewModel.DefaultTaskType;
        }

        private void OnCloseWithOK()
        {
            _newSectionName = sectionName.Text;
            _newComment = commentField.Text;
            _newDefaultPriority = (TaskPriorityBase)priorityList.SelectedItem;
            _newDefaultTaskType = (Enums.TaskType)typeList.SelectedItem;
        }

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

            OnCloseWithOK();

            return true;
        }

        internal void SaveToViewModel()
        {
            _sectionViewModel.Name = _newSectionName;
            _sectionViewModel.Comment = _newComment;
            _sectionViewModel.DefaultPriority = _newDefaultPriority;
            _sectionViewModel.DefaultTaskType = _newDefaultTaskType;
        }
    }
}
