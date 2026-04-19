namespace TaskManager.Model
{
    internal class MainModel
    {
        internal MasterSection BaseSection { get; private set; }

        internal List<Section> AllSections { get; } = [];

        /// <summary>Создать основной раздел</summary>
        internal MasterSection CreateMasterSection(bool throwOnError = true)
        {
            if (BaseSection is not null)
                throw new InvalidOperationException("Основной раздел уже создан");

            return new("Все");
        }

        /// <summary>Создать неосновной раздел</summary>
        internal static AdditionalSection CreateSection(string name) => new(name);

        /// <summary>Добавить раздел</summary>
        internal void AddSection(Section newSection)
        {
            if (AllSections.Contains(newSection))
                throw new InvalidOperationException($"Раздел {newSection} уже добавлен");

            if (newSection.IsMasterSection)
                BaseSection = (MasterSection)newSection;

            AllSections.Add(newSection);
        }

        /// <summary>Удалить раздел, если он неосновной</summary>
        internal bool RemoveSection(Section section)
            => !section.IsMasterSection && AllSections.Remove(section);
    }
}
