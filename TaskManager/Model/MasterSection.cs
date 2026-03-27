namespace TaskManager.Model
{
    internal sealed class MasterSection(string name) : Section(name)
    {
        internal override bool IsMasterSection => true;

        internal override void AddTask(TaskObject newTask)
        {
            base.AddTask(newTask);

            if (newTask.IsNew)
                newTask.IsNew = false;
        }

        internal override bool RemoveTask(TaskObject task)
        {
            throw new InvalidOperationException("Нельзя удалить задачу из основного раздела");
        }
    }
}
