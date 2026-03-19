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

        internal void AddTask(TaskObject newTask)
        {
            Tasks.Add(newTask);

            if (!IsBaseSection)
                newTask.AdditionalSection = this;
        }

        internal void RemoveTask(TaskObject task)
        {
            if (Tasks.Remove(task))
                task.AdditionalSection = null;
        }
    }
}
