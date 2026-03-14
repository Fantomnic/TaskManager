using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Helpers;
using TaskManager.Model;
using static TaskManager.Helpers.Enums;

namespace TaskManager.ViewModel
{
    /// <summary>Модель представления свойств задачи</summary>
    internal class TaskObjectViewModel : BaseViewModel
    {
        private TaskObject _taskObject;

        // TODO: можно поместить куда-нибудь в глобальную статику
        static TaskObjectViewModel()
        {
            PriorityList = GetEnumValues<TaskPriority>();
            StatusList = GetEnumValues<Enums.TaskStatus>();
        }

        internal TaskObjectViewModel(TaskObject taskObject)
        {
            _taskObject = taskObject;
        }

        public static IEnumerable<TaskPriority> PriorityList { get; private set; }

        public static IEnumerable<Enums.TaskStatus> StatusList { get; private set; }

        // Обработку null-значения можно сделать тут, а можно в свойствах привязки через TargetNullValue
        public string CreationDate => _taskObject?.CreationDate.ToString("dd.MM.yyyy");

        internal void SetPriority(TaskPriority newPriority)
        {
            if (_taskObject is not null)
                _taskObject.Priority = newPriority;
        }

        internal void SetStatus(Enums.TaskStatus newStatus)
        {
            if (_taskObject is not null)
                _taskObject.Status = newStatus;
        }
    }
}
