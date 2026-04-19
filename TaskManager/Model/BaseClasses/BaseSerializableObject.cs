using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using TaskManager.Helpers;
using TaskManager.Resources;
using static TaskManager.Helpers.Enums;

namespace TaskManager.Model.BaseClasses
{
    [Serializable]
    public abstract class BaseSerializableObject : ISerializable
    {
        public BaseSerializableObject()
        {
            Guid = Guid.NewGuid();
        }

        protected BaseSerializableObject(SerializationInfo info, StreamingContext context)
        {
            Guid = (Guid)info.GetValue(nameof(Guid), typeof(Guid));
        }

        protected internal Guid Guid { get; protected set; }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(Guid), Guid);
        }

        internal void Serialize(DataDirectory dataDirectory)
        {
            string targetDirectory = Helper.GetDataDirectory(dataDirectory);
            string fileName = Path.Combine(targetDirectory, Guid + Constants.DataExtension);


#pragma warning disable SYSLIB0011 // Type or member is obsolete
            var serialiser = new BinaryFormatter();
#pragma warning restore SYSLIB0011 // Type or member is obsolete

            using (var stream = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                serialiser.Serialize(stream, this);
            };
        }

        public static bool operator ==(BaseSerializableObject baseObject1, BaseSerializableObject baseObject2) => baseObject1?.Guid == baseObject2?.Guid;

        public static bool operator !=(BaseSerializableObject baseObject1, BaseSerializableObject baseObject2) => baseObject1?.Guid != baseObject2?.Guid;
    }
}
