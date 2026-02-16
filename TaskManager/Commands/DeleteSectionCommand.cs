using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TaskManager.View;

namespace TaskManager.Commands
{
    internal class DeleteSectionCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            // TODO: Добавить уведомление?
            if (parameter is not TabItem tabItem)
                return;

            var m = (MainWindow)Application.Current.MainWindow;

            m.sections.Items.Remove(tabItem);
        }
    }
}
