using System.Collections.ObjectModel;
using TaskManager.Model;

namespace TaskManager.ViewModel
{
    internal class MainViewModel : BaseViewModel
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        internal MainViewModel()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        {
            
        }

        // TODO: Вообще, ObservableCollection тут не нужно, т.к. не выводится в интерфейс
        /// <summary>Список всех разделов трекера</summary>
        public ObservableCollection<Section> Sections { get; set; } = [];

        // Не null, т.к. заполняется при инициализации главного окна
        public Section SelectedSection { get; set; }

        internal BaseSection BaseSection => (BaseSection)Sections.First(s => s.IsBaseSection);

        internal void AddSection(Section section) => Sections.Add(section);

        internal void RemoveSection(Section section) => Sections.Remove(section);

        internal List<string> GetSectionsNames(IEnumerable<Section>? ignoredSections = null)
        {
            var sections = Sections.ToList();

            if (ignoredSections is not null)
                sections = sections.FindAll(s => !ignoredSections.Contains(s));

            return [.. sections.Select(s => s.Name)];
        }

        /// <summary>Возвращает неосновные разделы, в которые не входит переданная задача</summary>
        internal List<Section> GetSectionsForChanging(Section? taskSection)
            => [.. Sections.Where(s => !s.IsBaseSection && s != taskSection)];
    }
}
