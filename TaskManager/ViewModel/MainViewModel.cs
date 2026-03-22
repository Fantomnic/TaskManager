using System.Collections.ObjectModel;
using System.Windows.Controls;
using TaskManager.Helpers;
using TaskManager.Model;

namespace TaskManager.ViewModel
{
    internal class MainViewModel : BaseViewModel
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        internal MainViewModel()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        {
            ModelData = new();
        }

        internal MainModel ModelData { get; }

        // TODO: Вообще, ObservableCollection тут не нужно, т.к. не выводится в интерфейс
        /// <summary>Список всех разделов трекера</summary>
        public ObservableCollection<SectionViewModel> SectionsViewModels { get; set; } = [];

        // Не null, т.к. заполняется при инициализации главного окна
        public SectionViewModel SelectedSectionViewModel { get; set; }

        internal SectionViewModel BaseSectionViewModel => SectionsViewModels.First(s => s.IsBaseSection);

        public ListBox? TasksList => UIHelper.GetCurrentSectionView()?.tasksList;

        internal void AddSection(SectionViewModel sectionViewModel)
        {
            SectionsViewModels.Add(sectionViewModel);
            ModelData.AddSection(sectionViewModel.Section);
        }

        internal void DeleteSection(SectionViewModel sectionViewModel)
        {
            SectionsViewModels.Remove(sectionViewModel);
            ModelData.DeleteSection(sectionViewModel.Section);
        }

        internal List<string> GetSectionsNames(IEnumerable<SectionViewModel>? ignoredSections = null)
        {
            var sections = SectionsViewModels.ToList();

            if (ignoredSections is not null)
                sections = sections.FindAll(s => !ignoredSections.Contains(s));

            return [.. sections.Select(s => s.Name)];
        }

        /// <summary>Возвращает неосновные разделы, в которые не входит переданная задача</summary>
        internal List<SectionViewModel> GetSectionsViewModelsForChanging(SectionViewModel? taskSectionViewModel)
            => [.. SectionsViewModels.Where(s => !s.IsBaseSection && s != taskSectionViewModel)];
    }
}
