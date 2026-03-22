using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TaskManager.Model
{
    internal class MainModel() //: INotifyPropertyChanged
    {
        internal BaseSection BaseSection { get; private set; }

        internal List<Section> AllSections { get; } = [];

        internal void AddSection(Section section)
        {
            if (section is BaseSection baseSection)
                BaseSection = baseSection;

            AllSections.Add(section);
        }

        internal void DeleteSection(Section section)
        {
            if (!section.IsBaseSection)
                AllSections.Remove(section);
        }

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
