using System.Globalization;
using System.Windows;
using System.Windows.Data;
using static TaskManager.Helpers.Enums;

namespace TaskManager.Helpers
{
    public class TaskTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => GetTypeString(value as TaskType?);

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => DependencyProperty.UnsetValue;

        public static string GetTypeString(TaskType? priority)
            => priority switch
            {
                TaskType.Once => "Одноразовая",
                TaskType.Regular => "Многоразовая",
                TaskType.LongTime => "Долгосрочная",
                _ => "Значение не определено"
            };
    }

    public class ThemeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => GetThemeString(value as Themes?);

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => DependencyProperty.UnsetValue;

        public static string GetThemeString(Themes? priority)
            => priority switch
            {
                Themes.Light => "Светлая",
                Themes.Dark => "Тёмная",
                _ => "Значение не определено"
            };
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
                5 => "Вообще огромный",
                _ => "Не придумано"
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => DependencyProperty.UnsetValue;
    }
}
