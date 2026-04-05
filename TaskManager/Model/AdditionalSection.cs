namespace TaskManager.Model
{
    internal class AdditionalSection(string name) : Section(name)
    {
        internal override bool IsMasterSection => false;
    }
}
