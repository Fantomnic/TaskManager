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
        private static Properties.Settings _appSettings = Properties.Settings.Default;

        static Settings()
        {
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

        public static FontSet FontSettings { get; } = new(0, 0, 0, 0, 0, 100);

        internal static bool SetDefaultSectionName { get; set; }

        internal static bool SetDefaultTaskName { get; set; }

        internal static bool IncrementSectionName { get; set; }

        internal static bool IncrementTaskName { get; set; }

        internal static string DefaultSectionName { get; set; }

        internal static string DefaultTaskName { get; set; }

        internal static bool ConfirmDeleteSection { get; set; }

        internal static bool ConfirmCompleteTask { get; set; }

        internal static bool AutoRenewalTasks { get; set; }

        internal static int MaxSectionLength { get; set; }

        internal static int MaxTaskLength { get; set; }

        internal static void FillFromViewModel(SettingsViewModel settingsViewModel)
        {
            SetDefaultSectionName = settingsViewModel.SetDefaultSectionName;
            SetDefaultTaskName = settingsViewModel.SetDefaultTaskName;
            IncrementSectionName = settingsViewModel.IncrementSectionName;
            IncrementTaskName = settingsViewModel.IncrementTaskName;
            DefaultSectionName = settingsViewModel.DefaultSectionName;
            DefaultTaskName = settingsViewModel.DefaultTaskName;
            ConfirmDeleteSection = settingsViewModel.ConfirmDeleteSection;
            ConfirmCompleteTask = settingsViewModel.ConfirmCompleteTask;
            AutoRenewalTasks = settingsViewModel.AutoRenewalTasks;
            MaxSectionLength = settingsViewModel.MaxSectionLength;
            MaxTaskLength = settingsViewModel.MaxTaskLength;

            if (AutoRenewalTasks)
                Helper.MasterSectionViewModel.MidnightUpdateTasks();

            FontSettings.CopyFrom(settingsViewModel.FontSettings);
            ChangeTheme(settingsViewModel.Theme);
        }

        internal static void ResetToDefault()
        {
            FontSettings.CopyFrom(AvailableFonts[1]);

            ChangeTheme(Themes.Light);

            SetDefaultSectionName = true;
            SetDefaultTaskName = true;
            IncrementSectionName = true;
            IncrementTaskName = true;
            DefaultSectionName = "Новый раздел";
            DefaultTaskName = "Новая задача";
            ConfirmDeleteSection = true;
            ConfirmCompleteTask = true;
            AutoRenewalTasks = false;
            MaxSectionLength = 50;
            MaxTaskLength = 50;

            TaskStatusesInstances.BeginingStatus.TaskVisible = true;
            TaskStatusesInstances.CompletedStatus.TaskVisible = true;
            TaskStatusesInstances.DeferredStatus.TaskVisible = true;
            TaskStatusesInstances.RejectedStatus.TaskVisible = true;
            TaskStatusesInstances.DoneStatus.TaskVisible = true;

            Instanse.NoneIndicate = true;
            Instanse.SortByStartDate = true;
            Instanse.DescendingSort = false;
            Instanse.ShowTodayTasks = false;

            RefreshSectionVisibleCore();
        }

        internal static void FillFromConfig()
        {
            int fontSettingsID = _appSettings.FontSettingsID;

            if (AvailableFonts.FirstOrDefault(f => f.ID == fontSettingsID) is FontSet fontSettings)
                FontSettings.CopyFrom(fontSettings);

            int themeID = _appSettings.ThemeID;

            if (Enum.IsDefined(typeof(Themes), themeID))
                ChangeTheme((Themes)themeID);

            SetDefaultSectionName = _appSettings.SetDefaultSectionName;
            SetDefaultTaskName = _appSettings.SetDefaultTaskName;
            IncrementSectionName = _appSettings.IncrementSectionName;
            IncrementTaskName = _appSettings.IncrementTaskName;
            DefaultSectionName = _appSettings.DefaultSectionName;
            DefaultTaskName = _appSettings.DefaultTaskName;
            ConfirmDeleteSection = _appSettings.ConfirmDeleteSection;
            ConfirmCompleteTask = _appSettings.ConfirmCompleteTask;
            AutoRenewalTasks = _appSettings.AutoRenewalTasks;
            MaxSectionLength = _appSettings.MaxSectionLength;
            MaxTaskLength = _appSettings.MaxTaskLength;

            TaskStatusesInstances.BeginingStatus.TaskVisible = _appSettings.BeginingStatusVisible;
            TaskStatusesInstances.CompletedStatus.TaskVisible = _appSettings.CompletedStatusVisible;
            TaskStatusesInstances.DeferredStatus.TaskVisible = _appSettings.DeferredStatusVisible;
            TaskStatusesInstances.RejectedStatus.TaskVisible = _appSettings.RejectedStatusVisible;
            TaskStatusesInstances.DoneStatus.TaskVisible = _appSettings.DoneStatusVisible;

            Instanse.IndicateByStatus = _appSettings.IndicateByStatus;
            Instanse.IndicateByPriority = _appSettings.IndicateByPriority;
            Instanse.NoneIndicate = _appSettings.NoneIndicate;

            Instanse.SortByStatus = _appSettings.SortByStatus;
            Instanse.SortByPriority = _appSettings.SortByPriority;
            Instanse.SortByName = _appSettings.SortByName;
            Instanse.SortByEndDate = _appSettings.SortByEndDate;
            Instanse.SortByStartDate = _appSettings.SortByStartDate;
            Instanse.DescendingSort = _appSettings.DescendingSort;

            Instanse.ShowTodayTasks = _appSettings.ShowTodayTasks;

            RefreshSectionVisibleCore();
        }

        internal static void SaveToConfig()
        {
            _appSettings.FontSettingsID = FontSettings.ID;
            _appSettings.ThemeID = (int)Theme;

            _appSettings.SetDefaultSectionName = SetDefaultSectionName;
            _appSettings.SetDefaultTaskName = SetDefaultTaskName;
            _appSettings.IncrementSectionName = IncrementSectionName;
            _appSettings.IncrementTaskName = IncrementTaskName;
            _appSettings.DefaultSectionName = DefaultSectionName;
            _appSettings.DefaultTaskName = DefaultTaskName;
            _appSettings.ConfirmDeleteSection = ConfirmDeleteSection;
            _appSettings.ConfirmCompleteTask = ConfirmCompleteTask;
            _appSettings.AutoRenewalTasks = AutoRenewalTasks;
            _appSettings.MaxSectionLength = MaxSectionLength;
            _appSettings.MaxTaskLength = MaxTaskLength;

            _appSettings.BeginingStatusVisible = TaskStatusesInstances.BeginingStatus.TaskVisible;
            _appSettings.CompletedStatusVisible = TaskStatusesInstances.CompletedStatus.TaskVisible;
            _appSettings.DeferredStatusVisible = TaskStatusesInstances.DeferredStatus.TaskVisible;
            _appSettings.RejectedStatusVisible = TaskStatusesInstances.RejectedStatus.TaskVisible;
            _appSettings.DoneStatusVisible = TaskStatusesInstances.DoneStatus.TaskVisible;

            _appSettings.IndicateByStatus = Instanse.IndicateByStatus;
            _appSettings.IndicateByPriority = Instanse.IndicateByPriority;
            _appSettings.NoneIndicate = Instanse.NoneIndicate;

            _appSettings.SortByStatus = Instanse.SortByStatus;
            _appSettings.SortByPriority = Instanse.SortByPriority;
            _appSettings.SortByName = Instanse.SortByName;
            _appSettings.SortByEndDate = Instanse.SortByEndDate;
            _appSettings.SortByStartDate = Instanse.SortByStartDate;
            _appSettings.DescendingSort = Instanse.DescendingSort;

            _appSettings.ShowTodayTasks = Instanse.ShowTodayTasks;

            _appSettings.Save();
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
                UIHelper.MainWindow.SetMenuColumnWidth(MinMenuWidth);
            }
        }

        public class SettingsInstanse() : BaseNotifyObject
        {
            private bool _indicateByStatus;
            private bool _indicateByPriority;
            private bool _noneIndicate;

            private bool _sortByStatus;
            private bool _sortByPriority;
            private bool _sortByName;
            private bool _sortByEndDate;
            private bool _sortByStartDate;
            private bool _descendingSort;

            private bool _showTodayTasks;

            #region Индикация

            public bool IndicateByStatus
            {
                get => _indicateByStatus;
                set
                {
                    _indicateByStatus = value;
                    OnPropertyChanged(nameof(IndicateByStatus));
                }
            }

            public bool IndicateByPriority
            {
                get => _indicateByPriority;
                set
                {
                    _indicateByPriority = value;
                    OnPropertyChanged(nameof(IndicateByPriority));
                }
            }

            public bool NoneIndicate
            {
                get => _noneIndicate;
                set
                {
                    _noneIndicate = value;
                    OnPropertyChanged(nameof(NoneIndicate));
                }
            }

            #endregion Индикация

            #region Сортировка

            public bool SortByStatus
            {
                get => _sortByStatus;
                set
                {
                    _sortByStatus = value;
                    OnPropertyChanged(nameof(SortByStatus));

                    if (!_sortByStatus)
                        RefreshSectionVisible();
                }
            }

            public bool SortByPriority
            {
                get => _sortByPriority;
                set
                {
                    _sortByPriority = value;
                    OnPropertyChanged(nameof(SortByPriority));

                    if (!_sortByPriority)
                        RefreshSectionVisible();
                }
            }

            public bool SortByName
            {
                get => _sortByName;
                set
                {
                    _sortByName = value;
                    OnPropertyChanged(nameof(SortByName));

                    if (!_sortByName)
                        RefreshSectionVisible();
                }
            }

            public bool SortByEndDate
            {
                get => _sortByEndDate;
                set
                {
                    _sortByEndDate = value;
                    OnPropertyChanged(nameof(SortByEndDate));

                    if (!_sortByEndDate)
                        RefreshSectionVisible();
                }
            }

            public bool SortByStartDate
            {
                get => _sortByStartDate;
                set
                {
                    _sortByStartDate = value;
                    OnPropertyChanged(nameof(SortByStartDate));

                    if (!_sortByStartDate)
                        RefreshSectionVisible();
                }
            }

            public bool DescendingSort
            {
                get => _descendingSort;
                set
                {
                    _descendingSort = value;
                    OnPropertyChanged(nameof(DescendingSort));
                    RefreshSectionVisible();
                }
            }

            #endregion Сортировка

            public bool ShowTodayTasks
            {
                get => _showTodayTasks;
                set
                {
                    _showTodayTasks = value;
                    OnPropertyChanged(nameof(ShowTodayTasks));
                    RefreshSectionVisible();
                }
            }

            private void RefreshSectionVisible()
            {
                if (!UIHelper.MainWindow.IsLoaded)
                    return;

                RefreshSectionVisibleCore();
            }
        }

        private static void RefreshSectionVisibleCore() => Helper.MainViewModel.SelectedSectionViewModel.RefreshVisibleTaskViewModels();
    }
}
