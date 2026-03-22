namespace TaskManager.Model
{
    internal class MainModel() //: INotifyPropertyChanged
    {
        internal BaseSection BaseSection { get; private set; }

        internal List<Section> AllSections { get; } = [];

        /// <summary>Создать основной раздел</summary>
        internal BaseSection CreateBaseSection(string name)
        {
            if (BaseSection is not null)
                throw new InvalidOperationException("Основной раздел уже создан");

            var baseSection = new BaseSection(name);
            BaseSection = baseSection;
            AllSections.Add(baseSection);
            return baseSection;
        }

        /// <summary>Создать неосновной раздел</summary>
        internal Section CreateSection(string name)
        {
            var newSection = new Section(name);
            AllSections.Add(newSection);
            return newSection;
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
