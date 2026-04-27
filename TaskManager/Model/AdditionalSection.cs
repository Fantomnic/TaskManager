using System.Runtime.Serialization;

namespace TaskManager.Model
{
    [DataContract]
    internal class AdditionalSection : Section
    {
        internal AdditionalSection(string name) : base(name)
        {

        }

        internal override bool IsMasterSection => false;
    }
}
