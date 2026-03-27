using System.Collections.ObjectModel;
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

        internal MasterSectionViewModel BaseSectionViewModel => SectionsViewModels.OfType<MasterSectionViewModel>().First();

        //public ListBox? TasksList => UIHelper.GetCurrentSectionView()?.tasksList;

        /// <summary>Создать основной раздел (с моделью представления)</summary>
        internal MasterSectionViewModel CreateMasterSection()
        {
            var newSection = ModelData.CreateMasterSection();
            return new(newSection);
        }

        /// <summary>Создать неосновной раздел (с моделью представления)</summary>
        internal AdditionalSectionViewModel CreateSection(string name)
        {
            var newSection = ModelData.CreateSection(name);
            var newSectionViewModel = new AdditionalSectionViewModel(newSection);
            return newSectionViewModel;
        }

        /// <summary>Добавить раздел (с моделью представления)</summary>
        internal void AddSection(Section newSection, SectionViewModel? newSectionViewModel)
        {
            ModelData.AddSection(newSection);

            newSectionViewModel ??= new AdditionalSectionViewModel(newSection);
            SectionsViewModels.Add(newSectionViewModel);
        }

        /// <summary>Удалить раздел (с моделью представления), если он неосновной</summary>
        internal bool RemoveSection(Section section)
            => ModelData.RemoveSection(section)
                && Helper.FindSectionViewModel(section) is AdditionalSectionViewModel sectionViewModel
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
            => [.. SectionsViewModels.Where(vm => !vm.IsMasterSection && vm.Section != taskObject.AdditionalSection)];
    }
}
