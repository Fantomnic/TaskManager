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

        /// <summary>Создать раздел (с моделью представления)</summary>
        internal SectionViewModel CreateSection(string name, bool daseSection = false)
        {
            var newSection = daseSection ? ModelData.CreateBaseSection(name) : ModelData.CreateSection(name);
            var newSectionViewModel = new SectionViewModel(newSection);
            SectionsViewModels.Add(newSectionViewModel);
            return newSectionViewModel;
        }

        /// <summary>Удалить раздел (с моделью представления), если он неосновной</summary>
        internal bool RemoveSection(Section section)
            => ModelData.RemoveSection(section)
                && Helper.FindSectionViewModel(section) is SectionViewModel sectionViewModel
                && SectionsViewModels.Remove(sectionViewModel);

        internal List<string> GetSectionsNames(IEnumerable<Section>? ignoredSections = null)
        {
            var sections = ModelData.AllSections;

            if (ignoredSections is not null)
                sections = sections.FindAll(s => !ignoredSections.Contains(s));

            return [.. sections.Select(s => s.Name)];
        }

        /// <summary>Возвращает модели представления неосновных разделов, в которые не входит переданная задача</summary>
        internal List<SectionViewModel> GetSectionsViewModelsForChanging(TaskObject taskObject)
            => [.. SectionsViewModels.Where(vm => !vm.IsBaseSection && vm.Section != taskObject.AdditionalSection)];
    }
}
