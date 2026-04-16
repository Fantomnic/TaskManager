using System.Windows;
using System.Windows.Threading;
using TaskManager.Helpers;

namespace TaskManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Current.DispatcherUnhandledException += CurrentDispatcherUnhandledException;
            base.OnStartup(e);
        }

        private void CurrentDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            UIHelper.ShowMessage(e.Exception.Message, MessageBoxImage.Error);
            e.Handled = true; // Предотвращает стандартное завершение приложения
        }
    }
}
