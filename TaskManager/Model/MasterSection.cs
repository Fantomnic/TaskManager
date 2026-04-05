namespace TaskManager.Model
{
    internal sealed class MasterSection(string name) : Section(name)
    {
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
            throw new InvalidOperationException("Нельзя удалить задачу из основного раздела");
        }
    }
}
