namespace TaskManager.Model
{
    internal sealed class BaseSection(string name) : Section(name)
    {
        internal override bool IsBaseSection => true;

        internal override void AddTask(TaskObject newTask)
        {
            Tasks.Add(newTask);
        }

        internal override bool RemoveTask(TaskObject task)
        {
            throw new InvalidOperationException("Нельзя удалить задачу из основного раздела");
        }
    }
}
