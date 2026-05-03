using System.Runtime.Serialization;
using TaskManager.Helpers.Exceptions;
using TaskManager.Resources;

namespace TaskManager.Model
{
    [DataContract]
    internal sealed class MasterSection : Section
    {
        internal MasterSection(string name) : base(name)
        {

        }

        internal override string FileName => GetFileName();

        internal override bool IsMasterSection => true;

        internal override bool AddTask(TaskObject newTask, bool throwOnError = false)
        {
            if (!base.AddTask(newTask, throwOnError))
                return false;

            if (newTask.IsNew)
                newTask.IsNew = false;

            return true;
        }

        internal override bool RemoveTask(TaskObject task)
        {
            if (task.AdditionalSection is not null)
                throw new WarningException("Нельзя удалить задачу, которая содержится в неосновном разделе");

            return base.RemoveTask(task);
        }

        internal static string GetFileName() => nameof(MasterSection) + Constants.SectionDataExtension;
    }
}
