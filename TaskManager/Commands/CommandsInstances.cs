namespace TaskManager.Commands
{
    // Прим.: Можно делать команды internal, помещать их экземпляры в DataContext и через привязку использовать в соответствующем представлении
    // Но чтобы можно было задавать их в xaml (как реализовано), нужен public
    public class CommandsInstances
    {
        static CommandsInstances()
        {
            DeleteSectionCommand = new DeleteSectionCommand();
            NewSectionCommand = new NewSectionCommand();
            ShowSectionPropertyCommand = new ShowSectionPropertyCommand();
            NewTaskCommand = new NewTaskCommand();
            DeleteTaskCommand = new DeleteTaskCommand();
            ChangeSectionCommand = new ChangeSectionCommand();
            OpenSettingsCommand = new OpenSettingsCommand();
        }

        public static NewSectionCommand NewSectionCommand { get; }

        public static DeleteSectionCommand DeleteSectionCommand { get; }

        public static ShowSectionPropertyCommand ShowSectionPropertyCommand { get; }

        public static NewTaskCommand NewTaskCommand { get; }

        public static DeleteTaskCommand DeleteTaskCommand { get; }

        public static ChangeSectionCommand ChangeSectionCommand { get; }

        public static OpenSettingsCommand OpenSettingsCommand { get; }
    }
}
