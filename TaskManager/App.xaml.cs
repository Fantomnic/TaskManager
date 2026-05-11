using System.Diagnostics;
using System.Windows;
using System.Windows.Threading;
using TaskManager.Helpers;
using TaskManager.Helpers.Exceptions;

namespace TaskManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var processes = Process.GetProcesses();

            if (processes.Count(p => p.ProcessName == "TaskManager") > 1)
            {
                Shutdown();
                return;
            }

            Current.DispatcherUnhandledException += CurrentDispatcherUnhandledException;
            base.OnStartup(e);
        }

        private void CurrentDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            var exception = e.Exception;

            if (exception is WarningException)
                UIHelper.ShowMessage(exception.Message, MessageBoxImage.Warning);
            else
                UIHelper.ShowMessage(exception.Message, MessageBoxImage.Error, "Необработанное исключение");

            if (DataHelper.DataIsLoaded)
                e.Handled = true; // Предотвращает стандартное завершение приложения
        }

        protected override void OnExit(ExitEventArgs e)
        {
            if (!DataHelper.DataIsSaved)
            {
                try
                {
                    DataHelper.SaveData(Enums.DataDirectory.BackupWithDate, false);
                }
                catch
                {
                    // Игнорируем
                }
            }

            base.OnExit(e);
        }
    }
}
