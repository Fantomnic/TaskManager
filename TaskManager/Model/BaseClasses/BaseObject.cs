using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.Serialization;
using TaskManager.Helpers;
using TaskManager.Resources;
using static TaskManager.Helpers.Enums;

namespace TaskManager.Model.BaseClasses
{
    [DataContract(IsReference = true)]
    public abstract class BaseObject
    {
        public BaseObject()
        {
            CreateGuid();
            CreationDate = DateTime.Now;
        }

        [DataMember]
        internal DateTime CreationDate { get; set; }

        [DataMember]
        protected internal Guid Guid { get; protected set; }

        internal virtual string FileName => Guid.ToString() + Constants.DataExtension;

        [DataMember]
        public virtual string Name { get; set; }

        public override string ToString() => Name;

        internal void Serialize(DataDirectory dataDirectory)
        {
            string targetDirectory = Helper.GetDataDirectory(dataDirectory);
            string fileName = Path.Combine(targetDirectory, FileName);

            List<Type> types = this is Section ? [typeof(TaskObject)] : [];

            var serialiser = new DataContractSerializer(GetType(), types);

            using (var stream = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                serialiser.WriteObject(stream, this);
            };
        }

        internal void CreateGuid()
        {
            GenereateGuidTarget targetType;

            if (this is TaskObject)
                targetType = GenereateGuidTarget.Task;
            else if (this is Section)
                targetType = GenereateGuidTarget.Section;
            else
                targetType = GenereateGuidTarget.None;

            Guid = Helper.GenereateGuid(targetType);
        }

        public static bool operator ==(BaseObject? baseObject1, BaseObject? baseObject2) => baseObject1?.Guid == baseObject2?.Guid;

        public static bool operator !=(BaseObject? baseObject1, BaseObject? baseObject2) => baseObject1?.Guid != baseObject2?.Guid;

        internal class BaseComparer : IEqualityComparer<BaseObject>
        {
            public bool Equals(BaseObject? obj1, BaseObject? obj2) => obj1?.Guid == obj2?.Guid;

            public int GetHashCode([DisallowNull] BaseObject obj) => obj.Guid.GetHashCode();
        }
    }
}
