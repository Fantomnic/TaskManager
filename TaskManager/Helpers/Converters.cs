using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using static TaskManager.Helpers.Enums;

namespace TaskManager.Helpers
{
    public class PriorityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => GetPriorityString(value as TaskPriority?);

        // TODO: изучить, когда используется
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => DependencyProperty.UnsetValue;

        public static string GetPriorityString(TaskPriority? priority)
            => priority switch
            {
                TaskPriority.Low => "Низкий",
                TaskPriority.Middle => "Средний",
                TaskPriority.High => "Высокий",
                _ => "Значение не определено"
            };
    }

    public class StatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => GetStatusString(value as Enums.TaskStatus?);

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => DependencyProperty.UnsetValue;

        public static string GetStatusString(Enums.TaskStatus? status)
            => status switch
            {
                Enums.TaskStatus.None => "Ожидает принятия",
                Enums.TaskStatus.Begining => "Текущее",
                Enums.TaskStatus.Completed => "Выполнено",
                Enums.TaskStatus.Deferred => "Отложено",
                Enums.TaskStatus.Rejected => "Отклонено",
                _ => "Значение не определено"
            };
    }
}
