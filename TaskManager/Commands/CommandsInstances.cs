using static TaskManager.Commands.ChangeStatusCommands;

namespace TaskManager.Commands
{
    // Прим.: Можно делать команды internal, помещать их экземпляры в DataContext и через привязку использовать в соответствующем представлении
    // Но чтобы можно было задавать их в xaml (как реализовано), нужен public
    public static class CommandsInstances
    {
        public static NewSectionCommand NewSectionCommand { get; } = new NewSectionCommand();

        public static DeleteSectionCommand DeleteSectionCommand { get; } = new DeleteSectionCommand();

        public static ShowSectionPropertyCommand ShowSectionPropertyCommand { get; } = new ShowSectionPropertyCommand();

        public static NewTaskCommand NewTaskCommand { get; } = new NewTaskCommand();

        public static DeleteTaskCommand DeleteTaskCommand { get; } = new DeleteTaskCommand();

        public static ChangeSectionCommand ChangeSectionCommand { get; } = new ChangeSectionCommand();

        public static OpenSettingsCommand OpenSettingsCommand { get; } = new OpenSettingsCommand();

        public static AcceptTaskCommand AcceptTaskCommand { get; } = new AcceptTaskCommand();
    }
}
