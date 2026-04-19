using System.Runtime.Serialization;

namespace TaskManager.Model.BaseClasses
{
    [Serializable]
    public abstract class BaseObject : BaseSerializableObject
    {
        protected BaseObject(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            Name = info.GetString(nameof(Name));
        }

        public BaseObject() : base()
        {
            
        }

        public virtual string Name { get; set; }

        public override string ToString() => Name;

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue(nameof(Name), Name);
        }
    }
}
