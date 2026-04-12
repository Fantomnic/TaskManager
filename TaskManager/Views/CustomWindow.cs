using System.Windows;
using System.Windows.Input;

namespace TaskManager.Views
{
    public class CustomWindow : Window
    {
        public CustomWindow() : base()
        {
            var commandBinding = new CommandBinding
            {
                Command = ApplicationCommands.Close
            };

            commandBinding.Executed += ExecutedCloseCommand;

            CommandBindings.Add(commandBinding);
        }

        public void ExecutedCloseCommand(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }
    }
}
