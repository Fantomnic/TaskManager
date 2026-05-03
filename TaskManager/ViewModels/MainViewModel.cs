using TaskManager.Helpers;
using TaskManager.Model;
using static TaskManager.Model.BaseClasses.BaseObject;

namespace TaskManager.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        internal MainViewModel()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        { }

        /// <summary>Список всех разделов трекера</summary>
        public List<SectionViewModel> SectionsViewModels { get; set; } = [];

        // Не null, т.к. заполняется при инициализации главного окна
        public SectionViewModel SelectedSectionViewModel { get; set; }

        internal MasterSectionViewModel MasterSectionViewModel => SectionsViewModels.OfType<MasterSectionViewModel>().First();

        /// <summary>Создать основной раздел (с моделью представления)</summary>
        internal MasterSectionViewModel CreateMasterSection()
        {
            var newSection = DataHelper.ModelData.CreateMasterSection();
            return CreateMasterSectionViewModel(newSection);
        }

        internal MasterSectionViewModel CreateMasterSectionViewModel(MasterSection section) => new(section);

        /// <summary>Создать неосновной раздел (с моделью представления)</summary>
        internal AdditionalSectionViewModel CreateAdditionalSection(string name)
        {
            var newSection = MainModel.CreateSection(name);
            return CreateAdditionalSectionViewModel(newSection);
        }

        internal AdditionalSectionViewModel CreateAdditionalSectionViewModel(AdditionalSection section) => new(section);

        /// <summary>Добавить раздел (с моделью представления)</summary>
        internal void AddSectionViewModel(SectionViewModel newSectionViewModel)
        {
            DataHelper.ModelData.AddSection(newSectionViewModel.Section);
            SectionsViewModels.Add(newSectionViewModel);
            newSectionViewModel.IsNew = false;
        }

        internal void ReplaceMasterSectionViewModel(MasterSectionViewModel newMasterSectionViewModel)
        {
            DataHelper.ModelData.ReplaceMasterSection((MasterSection)newMasterSectionViewModel.Section);
            SectionsViewModels.Remove(MasterSectionViewModel);
            SectionsViewModels.Add(newMasterSectionViewModel);
            newMasterSectionViewModel.IsNew = false;
        }

        internal void RemoveAllAdditionalSections()
        {
            var additionalSections = SectionsViewModels.OfType<AdditionalSectionViewModel>().ToList();

            foreach (var sectionViewModel in additionalSections)
                RemoveSectionViewModel(sectionViewModel);

            UIHelper.RemoveAllAdditionalTabItems();
        }

        /// <summary>Удалить раздел (с моделью представления), если он неосновной</summary>
        internal bool RemoveSectionViewModel(SectionViewModel sectionViewModel)
        {
            if (sectionViewModel is not AdditionalSectionViewModel additionalSectionViewModel)
                return false;

            if (!DataHelper.ModelData.RemoveSection(additionalSectionViewModel.Section))
                return false;

            var rootTasks = additionalSectionViewModel.AllRootTasksViewModels.ToList();

            foreach (var taskViewModel in rootTasks)
                taskViewModel.MoveToSection(Helper.MasterSectionViewModel);

            SectionsViewModels.Remove(sectionViewModel);

            return true;
        }

        internal SectionViewModel? FindSectionViewModel(Section section)
            => SectionsViewModels.FirstOrDefault(vm => vm.Section == section);

        internal List<string> GetSectionsNames(IEnumerable<Section>? ignoredSections = null)
        {
            var sections = DataHelper.ModelData.AllSections;

            if (ignoredSections is not null)
                sections = sections.FindAll(s => !ignoredSections.Contains(s, new BaseComparer()));

            return [.. sections.Select(s => s.Name)];
        }

        /// <summary>Возвращает модели представления неосновных разделов, в которые не входит переданная задача</summary>
        internal List<AdditionalSectionViewModel> GetSectionsViewModelsForChanging(TaskObject taskObject)
            => [.. SectionsViewModels.OfType<AdditionalSectionViewModel>().Where(vm => vm.Section != taskObject.AdditionalSection)];
    }
}
