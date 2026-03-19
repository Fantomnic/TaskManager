using System.Windows.Input;
using TaskManager.Helpers;
using TaskManager.Model;
using TaskManager.View;

namespace TaskManager.Commands
{
    public class ChangeSectionCommand : ICommand
    {
        private List<Section> _availableSections;

        public event EventHandler? CanExecuteChanged;

        // Для элементов списка не вызывается пересчёта, если объект уже в фокусе, поэтому нужно другое решение
        public bool CanExecute(object? parameter)
        {
            if (parameter is not TaskObject taskObject)
                return false;

            var taskSection = taskObject.AdditionalSection;
            _availableSections = GetSectionsForChanging(taskSection);

            return !Helper.IsBaseSection(taskSection) || _availableSections.Count > 0;
        }

        public void Execute(object? parameter)
        {
            if (parameter is not TaskObject taskObject)
                return;

            var taskSectionManagerWindow = new ChangeSectionWindow(_availableSections);

            if (taskSectionManagerWindow.ShowDialog() != true)
                return;

            taskObject.ChangeToSection(taskSectionManagerWindow.NewSection);
        }

        internal static List<Section> GetSectionsForChanging(Section? taskSection)
            => [.. Helper.MainViewModel.Sections.Where(s => !s.IsBaseSection && s != taskSection && s != Helper.MainViewModel.SelectedSection)];
    }
}
