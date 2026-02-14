using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TaskManager.Commands;

namespace TaskManager.ViewModel
{
    internal class MainViewModel
    {
        public MainViewModel()
        {
            NewSectionCommand = new NewSectionCommand();
        }

        public NewSectionCommand NewSectionCommand { get; set; }
    }
}
