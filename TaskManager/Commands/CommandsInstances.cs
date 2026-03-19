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
        }

        public static NewSectionCommand NewSectionCommand { get; set; }

        public static DeleteSectionCommand DeleteSectionCommand { get; set; }

        public static ShowSectionPropertyCommand ShowSectionPropertyCommand { get; set; }

        public static NewTaskCommand NewTaskCommand { get; set; }

        public static DeleteTaskCommand DeleteTaskCommand { get; set; }

        public static ChangeSectionCommand ChangeSectionCommand { get; set; }
    }
}
