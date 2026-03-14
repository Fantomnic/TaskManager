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
    /// <summary>Модель представления свойств задачи</summary>
    internal class TaskObjectViewModel : BaseViewModel
    {
        private TaskObject _taskObject;

        internal TaskObjectViewModel()
        {
            
        }

        internal TaskObjectViewModel(TaskObject taskObject)
        {
            _taskObject = taskObject;
        }

        // Обработку null-значения можно сделать тут, а можно в свойствах привязки через TargetNullValue
        public string CreationDate
        {
            get => _taskObject?.CreationDate.ToString("dd.MM.yyyy");
        }
    }
}
