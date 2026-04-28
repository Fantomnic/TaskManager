using System.Windows.Threading;

namespace TaskManager.Helpers
{
    internal static class UpdateDateTimer
    {
        internal static void Start()
        {
            var timer = new DispatcherTimer();

            var nextMidnight = DateTime.Now.Date.AddDays(1);

            if (CheckMainDay())
            {
                timer.Interval = TimeSpan.FromMinutes(5);
                timer.Tick += new EventHandler(TimerWaitingTick);
            }
            else
            {
                timer.Interval = TimeSpan.FromSeconds(5);
                timer.Tick += new EventHandler(TimerUpdateTick);
            }

            timer.Start();

            // Значение true, если время меньше, чем 23:55
            bool CheckMainDay()
            {
                var almostTomorrow = DateTime.Now.Date.AddDays(1).AddMinutes(-5);
                return DateTime.Now < almostTomorrow;
            }

            // Каждые 5 минут проверяем, что время ещё не пришло
            void TimerWaitingTick(object? sender, EventArgs e)
            {
                if (CheckMainDay())
                    return;

                // Если время пришло, запускаем 5-секундный таймер
                timer.Stop();
                Start();
            }

            void TimerUpdateTick(object? sender, EventArgs e)
            {
                if (DateTime.Now < nextMidnight)
                    return;

                timer.Stop();

                try
                {
                    Helper.MasterSectionViewModel.MidnightUpdateTasks();
                }
                catch
                {
                    throw;
                }
                finally
                {
                    // Запускаем новый день
                    Start();
                }
            }
        }
    }
}
