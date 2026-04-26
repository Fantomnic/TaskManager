using System.Windows;

namespace TaskManager.Helpers
{
    // TODO: Пока не используется
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
