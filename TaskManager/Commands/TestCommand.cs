using TaskManager.Helpers;

namespace TaskManager.Commands
{
    internal class TestCommand : BaseCommand
    {
        internal override void ExecuteImplement(object? parameter)
        {
            foreach (var sections in DataHelper.ModelData.AllSections)
                sections.Serialize(Enums.DataDirectory.Root);
        }
    }
}
