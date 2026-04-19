using System.Windows;
using TaskManager.Helpers;
using TaskManager.Model.BaseClasses;
using TaskManager.Model.TaskPriorities;
using TaskManager.Model.TaskStatuses;
using TaskManager.Resources;
using TaskManager.ViewModels;
using static TaskManager.Helpers.Enums;

namespace TaskManager.Model
{
    internal static class Settings
    {
        static Settings()
        {
            FontSettings = new(0, 0, 0, 0, 0, 100);

            AvailableFonts =
                [
                    new(1, 14, 1, 20, 25, 225),
                    new(14/Constants.StandartBaseFont, 16, 20/Constants.StandartMenuFont, 23, 27, 250),
                    new(16/Constants.StandartBaseFont, 18, 23/Constants.StandartMenuFont, 26, 30, 280),
                    new(18/Constants.StandartBaseFont, 20, 26/Constants.StandartMenuFont, 30, 33, 325),
                ];

            FontSettings.CopyFrom(AvailableFonts[1]);
        }

        public static SettingsInstanse Instanse { get; set; } = new();

        public static Themes Theme { get; set; } = Themes.Light;

        public static List<FontSet> AvailableFonts { get; set; }

        public static FontSet FontSettings { get; }

        internal static bool? SetDefaultSectionName { get; set; } = true;

        internal static bool? SetDefaultTaskName { get; set; } = true;

        internal static bool? IncrementSectionName { get; set; } = true;

        internal static bool? IncrementTaskName { get; set; } = true;

        internal static string DefaultSectionName { get; set; } = "Новый раздел";

        internal static string DefaultTaskName { get; set; } = "Новая задача";

        internal static void FillFromViewModel(SettingsViewModel settingsViewModel)
        {
            SetDefaultSectionName = settingsViewModel.SetDefaultSectionName;
            SetDefaultTaskName = settingsViewModel.SetDefaultTaskName;
            IncrementSectionName = settingsViewModel.IncrementSectionName;
            IncrementTaskName = settingsViewModel.IncrementTaskName;
            DefaultSectionName = settingsViewModel.DefaultSectionName;
            DefaultTaskName = settingsViewModel.DefaultTaskName;
            FontSettings.CopyFrom(settingsViewModel.FontSettings);
            ChangeTheme(settingsViewModel.Theme);
            UIHelper.MainWindow.SetMenuColumnWidth(FontSettings.MinMenuWidth);
        }

        internal static string GetDefaultTaskName()
        {
            if (SetDefaultTaskName != true)
                return String.Empty;

            string result = DefaultTaskName;

            if (IncrementTaskName == true)
            {
                var existingNames = Helper.GetAllTasks().Select(s => s.Name);
                result = Helper.GetStringWithCounter(result, existingNames);
            }

            return result;
        }

        private static void ChangeTheme(Themes newTheme)
        {
            if (Theme == newTheme)
                return;

            var dictionaries = Application.Current.Resources.MergedDictionaries;
            var currentDictionaryPath = GetResourceDictionaryPath(Theme);

            if (dictionaries.FirstOrDefault(d => d.Source == currentDictionaryPath) is not ResourceDictionary currentDictionaryTheme)
                return;

            var newDictionaryPath = GetResourceDictionaryPath(newTheme);
            var newDictionaryTheme = new ResourceDictionary() { Source = newDictionaryPath };

            dictionaries.Remove(currentDictionaryTheme);
            dictionaries.Add(newDictionaryTheme);

            Theme = newTheme;

            ResetColors();
        }

        private static Uri GetResourceDictionaryPath(Themes theme)
        {
            string name = theme switch
            {
                Themes.Light => "/Resources/Themes/LightTheme.xaml",
                Themes.Dark => "/Resources/Themes/DarkTheme.xaml",
                _ => throw new NotImplementedException()
            };

            return new(name, UriKind.Relative);
        }

        private static void ResetColors()
        {
            TaskStatusesInstances.ResetBackgrounds();
            TaskPrioritiesInstances.ResetForegrounds();
        }

        // Т.к. элементы привязаны к свойствам, нужно уведомлять интерфейс, что значения поменялись
        // При этом само свойство FontSettings не меняем, т.к. оно не имеет механизма оповещения
        public class FontSet(double baseFontCoefficient,
            double titleFont,
            double menuFontCoefficient,
            double menuCommandsFont,
            double buttonAreaHeight,
            double minMenuWidth) : BaseNotifyObject
        {
            private static int _count;
            private double _minMenuWidth = minMenuWidth;
            private double _baseFontCoefficient = baseFontCoefficient;
            private double _baseFont = Constants.StandartBaseFont * baseFontCoefficient;
            private double _titleFont = titleFont;
            private double _menuFontCoefficient = menuFontCoefficient;
            private double _menuTextsFont = Constants.StandartMenuFont * menuFontCoefficient;
            private double _menuCommandsFont = menuCommandsFont;
            private double _buttonAreaHeight = buttonAreaHeight;

            // ID изменяем, чтобы корректно заполнять текущий элемент в списке
            public int ID { get; private set; } = _count++;

            public double MinMenuWidth
            {
                get => _minMenuWidth;
                set
                {
                    _minMenuWidth = value;
                    OnPropertyChanged(nameof(MinMenuWidth));
                }
            }

            public double BaseFontCoefficient
            {
                get => _baseFontCoefficient;
                set
                {
                    _baseFontCoefficient = value;
                    OnPropertyChanged(nameof(BaseFontCoefficient));
                }
            }

            public double BaseFont
            {
                get => _baseFont;
                set
                {
                    _baseFont = value;
                    OnPropertyChanged(nameof(BaseFont));
                }
            }

            public double TitleFont
            {
                get => _titleFont;
                set
                {
                    _titleFont = value;
                    OnPropertyChanged(nameof(TitleFont));
                }
            }

            public double MenuFontCoefficient
            {
                get => _menuFontCoefficient;
                set
                {
                    _menuFontCoefficient = value;
                    OnPropertyChanged(nameof(MenuFontCoefficient));
                }
            }

            public double MenuTextsFont
            {
                get => _menuTextsFont;
                set
                {
                    _menuTextsFont = value;
                    OnPropertyChanged(nameof(MenuTextsFont));
                }
            }

            public double MenuCommandsFont
            {
                get => _menuCommandsFont;
                set
                {
                    _menuCommandsFont = value;
                    OnPropertyChanged(nameof(MenuCommandsFont));
                }
            }

            public double ButtonAreaHeight
            {
                get => _buttonAreaHeight;
                set
                {
                    _buttonAreaHeight = value;
                    OnPropertyChanged(nameof(ButtonAreaHeight));
                }
            }

            internal void CopyFrom(FontSet newFont)
            {
                BaseFontCoefficient = newFont.BaseFontCoefficient;
                BaseFont = newFont.BaseFont;
                TitleFont = newFont.TitleFont;
                ButtonAreaHeight = newFont.ButtonAreaHeight;
                MenuFontCoefficient = newFont.MenuFontCoefficient;
                MenuTextsFont = newFont.MenuTextsFont;
                MenuCommandsFont = newFont.MenuCommandsFont;
                ID = newFont.ID;
                MinMenuWidth = newFont.MinMenuWidth;
            }
        }

        public class SettingsInstanse() : BaseNotifyObject
        {
            private bool _showTodayTasks;

            public bool ShowTodayTasks
            {
                get => _showTodayTasks;
                set
                {
                    _showTodayTasks = value;
                    OnPropertyChanged(nameof(ShowTodayTasks));
                }
            }
        }
    }
}
