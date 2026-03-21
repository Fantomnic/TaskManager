using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Model
{
    internal sealed class BaseSection(string name) : Section(name)
    {
        internal override bool IsBaseSection => true;

        internal override void AddTask(TaskObject newTask)
        {
            Tasks.Add(newTask);
        }
    }
}
