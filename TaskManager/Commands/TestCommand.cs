using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Commands
{
    internal class TestCommand : BaseCommand
    {
        internal override void ExecuteImplement(object? parameter)
        {
            var t = AppDomain.CurrentDomain.BaseDirectory;
        }
    }
}
