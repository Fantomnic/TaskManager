using TaskManager.Helpers;
using TaskManager.Model;

namespace TaskManager.ViewModels
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

        /// <summary>Список всех разделов трекера</summary>
        public List<SectionViewModel> SectionsViewModels { get; set; } = [];

        // Не null, т.к. заполняется при инициализации главного окна
        public SectionViewModel SelectedSectionViewModel { get; set; }

        internal MasterSectionViewModel MasterSectionViewModel => SectionsViewModels.OfType<MasterSectionViewModel>().First();

        //public ListBox? TasksList => UIHelper.GetCurrentSectionView()?.tasksList;

        /// <summary>Создать основной раздел (с моделью представления)</summary>
        internal MasterSectionViewModel CreateMasterSection()
        {
            var newSection = ModelData.CreateMasterSection();
            return new(newSection);
        }

        /// <summary>Создать неосновной раздел (с моделью представления)</summary>
        internal static AdditionalSectionViewModel CreateSection(string name)
        {
            var newSection = MainModel.CreateSection(name);
            var newSectionViewModel = new AdditionalSectionViewModel(newSection);
            return newSectionViewModel;
        }

        /// <summary>Добавить раздел (с моделью представления)</summary>
        internal void AddSectionViewModel(SectionViewModel newSectionViewModel)
        {
            ModelData.AddSection(newSectionViewModel.Section);
            SectionsViewModels.Add(newSectionViewModel);
        }

        /// <summary>Удалить раздел (с моделью представления), если он неосновной</summary>
        internal bool RemoveSectionViewModel(SectionViewModel sectionViewModel)
        {
            if (sectionViewModel is not AdditionalSectionViewModel additionalSectionViewModel)
                return false;

            if (!ModelData.RemoveSection(additionalSectionViewModel.Section))
                return false;

            var rootTasks = additionalSectionViewModel.RootTasksViewModels.ToList();

            foreach (var taskViewModel in rootTasks)
                taskViewModel.MoveToSection(Helper.MasterSectionViewModel);

            SectionsViewModels.Remove(sectionViewModel);

            return true;
        }

        internal SectionViewModel? FindSectionViewModel(Section section)
            => SectionsViewModels.FirstOrDefault(vm => vm.Section == section);

        internal List<string> GetSectionsNames(IEnumerable<Section>? ignoredSections = null)
        {
            var sections = ModelData.AllSections;

            if (ignoredSections is not null)
                sections = sections.FindAll(s => !ignoredSections.Contains(s));

            return [.. sections.Select(s => s.Name)];
        }

        /// <summary>Возвращает модели представления неосновных разделов, в которые не входит переданная задача</summary>
        internal List<AdditionalSectionViewModel> GetSectionsViewModelsForChanging(TaskObject taskObject)
            => [.. SectionsViewModels.OfType<AdditionalSectionViewModel>().Where(vm => vm.Section != taskObject.AdditionalSection)];
    }
}
