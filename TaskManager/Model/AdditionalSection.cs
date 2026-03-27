namespace TaskManager.Model
{
    internal class AdditionalSection(string name) : Section(name)
    {
        internal override bool IsMasterSection => false;

        internal override void AddTask(TaskObject newTask)
        {
            base.AddTask(newTask);
            newTask.AdditionalSection = this;
        }

        internal override bool RemoveTask(TaskObject task)
        {
            bool result;

            if (result = base.RemoveTask(task))
                task.AdditionalSection = null;

            return result;
        }
    }
}
