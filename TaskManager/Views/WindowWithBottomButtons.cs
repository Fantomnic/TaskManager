using System.Windows;
using TaskManager.Helpers;

namespace TaskManager.Views
{
    /// <summary>Класс, содержащий логику для нижних кнопок (ОК/Отмена и т.п.) в окнах</summary>
    public class WindowWithBottomButtons : CustomWindow
    {
        public WindowWithBottomButtons() : base()
        {
            Owner = UIHelper.MainWindow;
        }

        protected virtual void ButtonOKClick(object sender, RoutedEventArgs e)
        {
            if (!ValidateOK())
                return;

            DialogResult = true;
            Close();
        }

        protected virtual void ButtonCancelClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        protected virtual bool ValidateOK() => true;
    }
}
