namespace TaskManager.Model
{
    public abstract class BaseObject
    {
        private Guid _guid;

        public virtual string Name { get; set; }

        public override string ToString() => Name;
    }
}
