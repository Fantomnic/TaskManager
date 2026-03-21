using System.Collections.ObjectModel;
using TaskManager.Helpers;

namespace TaskManager.Model
{
    internal class Section : BaseObject
    {
        public Section(string name)
        {
            Name = name;
        }

        internal virtual bool IsBaseSection => false;

        internal ObservableCollection<TaskObject> Tasks { get; } = [];

        internal virtual void AddTask(TaskObject newTask)
        {
            if (!Helper.GetAllTasks().Contains(newTask))
                Helper.BaseSection.AddTask(newTask);

            Tasks.Add(newTask);

            newTask.AdditionalSection = this;
        }

        internal void RemoveTask(TaskObject task)
        {
            if (Tasks.Remove(task))
                task.AdditionalSection = null;
        }
    }
}
