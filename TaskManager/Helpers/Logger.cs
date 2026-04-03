using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TaskManager.Helpers
{
    internal class Logger
    {
        internal static void ShowErrorMessage(string message)
        {
            try
            {
                throw new InvalidOperationException(message);
            }
            catch
            {
                MessageBox.Show(message);
            }
        }

        internal static void ExecuteWithTryCatch(Action action)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
