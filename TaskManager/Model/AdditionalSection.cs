using System.Runtime.Serialization;

namespace TaskManager.Model
{
    [Serializable]
    internal class AdditionalSection : Section
    {
        protected AdditionalSection(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }

        internal AdditionalSection(string name) : base(name)
        {

        }


        internal override bool IsMasterSection => false;

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}
