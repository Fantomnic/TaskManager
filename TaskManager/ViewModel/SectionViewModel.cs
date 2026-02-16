using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Model;
using TaskManager.View;
using TaskObject = TaskManager.Model.TaskObject;

namespace TaskManager.ViewModel
{
    internal class SectionViewModel : BaseViewModel
    {
        private TaskObject _selectedObject;
        private Section _section;

        public SectionViewModel(string name)
        {
            _section = new Section(name);

            Tasks =
                [
                    new() { Name = "Test 1" },
                    new() { Name = "Test 2" }
                ];
        }

        public TaskObject SelectedObject
        {
            get => _selectedObject;
            set
            {
                _selectedObject = value;
                OnPropertyChanged(nameof(SelectedObject));
            }
        }

        public string Name
        {
            get => _section.Name;
            set
            {
                _section.Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }



        public ObservableCollection<TaskObject> Tasks { get; set; }
    }
}
