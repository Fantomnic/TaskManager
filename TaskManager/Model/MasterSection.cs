using System.Runtime.Serialization;

namespace TaskManager.Model
{
    [Serializable]
    internal sealed class MasterSection : Section
    {
        private MasterSection(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }

        internal MasterSection(string name) : base(name)
        {

        }

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

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}
