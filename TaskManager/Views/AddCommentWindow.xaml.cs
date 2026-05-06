using System;
using System.Collections.Generic;
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
using TaskManager.ViewModels;

namespace TaskManager.Views
{
    /// <summary>
    /// Interaction logic for AddCommentWindow.xaml
    /// </summary>
    public partial class AddCommentWindow : WindowWithBottomButtons
    {
        internal AddCommentWindow(TaskObjectViewModel taskObjectViewModel)
        {
            InitializeComponent();

            UIHelper.SetFocus(commentField);
        }

        public string Comment { get; private set; }

        protected override bool ValidateOK()
        {
            Comment = commentField.Text.Trim();

            if (String.IsNullOrWhiteSpace(Comment))
            {
                UIHelper.ShowMessage("Нельзя добавить пустой комментарий", MessageBoxImage.Warning);
                return false;
            }

            return true;
        }
    }
}
