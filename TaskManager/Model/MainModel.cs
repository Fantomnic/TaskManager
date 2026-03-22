namespace TaskManager.Model
{
    internal class MainModel() //: INotifyPropertyChanged
    {
        internal BaseSection BaseSection { get; private set; }

        internal List<Section> AllSections { get; } = [];

        /// <summary>Создать раздел</summary>
        internal Section CreateSection(string name, bool baseSection = false)
        {
            if (baseSection && BaseSection is not null)
                throw new InvalidOperationException("Основной раздел уже создан");

            var newSection = baseSection ? new BaseSection(name) : new Section(name);
            return newSection;
        }

        /// <summary>Добавить раздел</summary>
        internal void AddSection(Section newSection)
        {
            if (newSection.IsBaseSection)
                BaseSection = (BaseSection)newSection;

            AllSections.Add(newSection);
        }

        /// <summary>Удалить основной раздел, если он неосновной</summary>
        internal bool RemoveSection(Section section)
            => !section.IsBaseSection && AllSections.Remove(section);

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
