using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TaskManager.Helpers;
using TaskManager.View;
using TaskManager.ViewModel;

namespace TaskManager.Commands
{
    internal class ShowSectionPropertyCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter) => true;

        public void Execute(object? parameter)
        {
            if (parameter is not SectionViewModel sectionViewModel)
                return;

            var windowProperty = new SectionPropertyWindow(sectionViewModel);

            windowProperty.ShowDialog();
        }
    }
}
