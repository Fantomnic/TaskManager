using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Model;

namespace TaskManager.ViewModel
{
    internal class TaskObjectViewModel : BaseViewModel
    {
        private TaskObject _taskObject;

        //internal TaskObjectViewModel(TaskObject taskObject)
        //{
        //    _taskObject = taskObject;
        //}

        public TaskObject TaskObject
        {
            get => _taskObject;
            set
            {
                _taskObject = value;
            }
        }

        public string CreationDate
        {
            get => _taskObject.CreationDate.ToString("dd.mm.yyyy");
        }
    }
}
