using System.Collections.ObjectModel;
using System.Xml.Linq;
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

        /// <summary>Создать новую задачу без привязки к разделу</summary>
        internal static TaskObject CreateTask() => CreateTask(Settings.GetDefaultTaskName());

        /// <summary>Создать новую задачу без привязки к разделу</summary>
        internal static TaskObject CreateTask(string name) => new() { Name = name };

        internal virtual void AddTask(TaskObject newTask)
        {
            if (!Helper.GetAllTasks().Contains(newTask))
                Helper.ModelData.BaseSection.AddTask(newTask);

            Tasks.Add(newTask);

            newTask.AdditionalSection = this;
        }

        internal virtual bool RemoveTask(TaskObject task)
        {
            bool result;

            if (result = Tasks.Remove(task))
                task.AdditionalSection = null;

            return result;
        }
    }
}
