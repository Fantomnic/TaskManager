using static TaskManager.Commands.ChangeStatusCommands;

namespace TaskManager.Commands
{
    // Прим.: Можно делать команды internal, помещать их экземпляры в DataContext и через привязку использовать в соответствующем представлении
    // Но чтобы можно было задавать их в xaml (как реализовано), нужен public
    public static class CommandsInstances
    {
        public static NewSectionCommand NewSectionCommand { get; } = new();

        public static DeleteSectionCommand DeleteSectionCommand { get; } = new();

        public static ShowSectionPropertyCommand ShowSectionPropertyCommand { get; } = new();

        public static NewTaskCommand NewTaskCommand { get; } = new();

        public static DeleteTaskCommand DeleteTaskCommand { get; } = new();

        public static ChangeSectionCommand ChangeSectionCommand { get; } = new();

        public static OpenSettingsCommand OpenSettingsCommand { get; } = new();

        public static AcceptTaskCommand AcceptTaskCommand { get; } = new();

        public static RejectTaskCommand RejectTaskCommand { get; } = new();

        public static DeferTaskCommand DeferTaskCommand { get; } = new();

        public static DoneTaskCommand DoneTaskCommand { get; } = new();

        public static CompleteTaskCommand CompleteTaskCommand { get; } = new();
    }
}
