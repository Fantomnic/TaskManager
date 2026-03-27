namespace TaskManager.Model
{
    internal class MainModel() //: INotifyPropertyChanged
    {
        internal MasterSection BaseSection { get; private set; }

        internal List<Section> AllSections { get; } = [];

        /// <summary>Создать основной раздел</summary>
        internal MasterSection CreateMasterSection()
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
            if (newSection.IsMasterSection)
                BaseSection = (MasterSection)newSection;

            AllSections.Add(newSection);
        }

        /// <summary>Удалить раздел, если он неосновной</summary>
        internal bool RemoveSection(Section section)
            => !section.IsMasterSection && AllSections.Remove(section);

        //private BaseSection _baseSection;

        //internal BaseSection BaseSection
        //{
        //    get => _baseSection;
        //    set
        //    {
        //        _baseSection = value;
        //        OnPropertyChanged(nameof(BaseSection));
        //    }
        //}

        //public event PropertyChangedEventHandler? PropertyChanged;

        //public void OnPropertyChanged([CallerMemberName] string prop = "")
        //    => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
