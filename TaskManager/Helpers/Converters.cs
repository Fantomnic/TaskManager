using System.Globalization;
using System.Windows;
using System.Windows.Data;
using TaskManager.Model.TaskStatuses;
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
            => (value as TaskStatusBase)?.DisplayName ?? "null";

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => DependencyProperty.UnsetValue;
    }

    public class FontIDConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not int id)
                return "Ошибка отображения";

            return id switch
            {
                1 => "Малый",
                2 => "Средний",
                3 => "Побольше",
                4 => "Большой жесть",
                5 => "Огромный ААА",
                _ => "Не придумано"
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => DependencyProperty.UnsetValue;
    }
}
