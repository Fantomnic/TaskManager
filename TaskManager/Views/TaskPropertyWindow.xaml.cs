using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TaskManager.Helpers;
using TaskManager.Model.TaskPriorities;
using TaskManager.ViewModels;

namespace TaskManager.Views
{
    /// <summary>
    /// Interaction logic for TaskPropertyWindow.xaml
    /// </summary>
    public partial class TaskPropertyWindow : WindowWithBottomButtons
    {
        private TaskObjectViewModel _taskObjectViewModel;

        internal TaskPropertyWindow(TaskObjectViewModel taskObjectViewModel)
        {
            InitializeComponent();
            DataContext = _taskObjectViewModel = taskObjectViewModel;
            InitializeFields();

            UIHelper.SetFocus(taskName);
        }

        private void InitializeFields()
        {
            taskName.Text = _taskObjectViewModel.Name;
        }

        private void OnCloseWithOK()
        {
            _taskObjectViewModel.Name = taskName.Text;
        }

        protected override bool ValidateOK()
        {
            string name = taskName.Text;
            bool result;

            if (result = Helper.CheckSaveTaskName(name))
                OnCloseWithOK();

            return result;
        }
    }
}
