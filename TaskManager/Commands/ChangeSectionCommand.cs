using System.Windows.Input;
using TaskManager.Helpers;
using TaskManager.Model;
using TaskManager.View;

namespace TaskManager.Commands
{
    public class ChangeSectionCommand : ICommand
    {
        private Section _taskSection;
        private List<Section> _availableSections;

        public event EventHandler? CanExecuteChanged;

        // Для элементов списка не вызывается пересчёта, если объект уже в фокусе, поэтому нужно другое решение
        public bool CanExecute(object? parameter)
        {
            if (parameter is not TaskObject)
                return false;

            _taskSection = Helper.MainViewModel.SelectedSection;
            _availableSections = GetSectionsForChanging(_taskSection);

            return !Helper.IsBaseSection(_taskSection) || _availableSections.Count > 0;
        }

        public void Execute(object? parameter)
        {
            if (parameter is not TaskObject taskObject)
                return;

            var taskSectionManagerWindow = new ChangeSectionWindow(_taskSection, _availableSections);

            if (taskSectionManagerWindow.ShowDialog() != true)
                return;

            taskObject.ChangeToSection(taskSectionManagerWindow.NewSection);
        }

        internal static List<Section> GetSectionsForChanging(Section currentSection)
            => [.. Helper.MainViewModel.Sections.Where(s => !s.IsBaseSection && s != currentSection)];
    }
}
