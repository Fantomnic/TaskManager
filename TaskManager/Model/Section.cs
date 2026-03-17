using System.Collections.ObjectModel;

namespace TaskManager.Model
{
    internal class Section : BaseObject
    {
        public Section(string name, bool baseSection)
        {
            Name = name;
            IsBaseSection = baseSection;
        }

        internal bool IsBaseSection { get; }

        internal ObservableCollection<TaskObject> Tasks { get; } = [];
    }
}
