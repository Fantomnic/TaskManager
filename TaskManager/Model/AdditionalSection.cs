namespace TaskManager.Model
{
    internal class AdditionalSection(string name) : Section(name)
    {
        internal override bool IsMasterSection => false;

        internal override void AddTask(TaskObject newTask, bool throwOnError = false)
        {
            base.AddTask(newTask, throwOnError);
            newTask.AdditionalSection = this;

            foreach (var child in newTask.Children)
                AddTask(child, throwOnError);
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
