using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Views;

namespace TaskManager.Commands
{
    public class ImportCommand : BaseCommand
    {
        internal override void ExecuteImplement(object? parameter)
        {
            var importWindow = new ImportWindow();
            importWindow.ShowDialog();
        }
    }
}
