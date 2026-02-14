using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TaskManager
{
    public static class Commands0
    {
        static Commands0()
        {
            DeleteSectionCommand = new RoutedUICommand("Удалить раздел", "DeleteSectionCommand", typeof(Commands0));
        }

        public static RoutedUICommand DeleteSectionCommand { get; set; }
    }
}
