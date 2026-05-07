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
            AvailableFonts =
                [
                    new(1, 14, 1, 20, 25, 225),
                    new(14/Constants.StandartBaseFont, 16, 20/Constants.StandartMenuFont, 23, 27, 250),
                    new(16/Constants.StandartBaseFont, 18, 23/Constants.StandartMenuFont, 26, 30, 280),
                    new(18/Constants.StandartBaseFont, 20, 26/Constants.StandartMenuFont, 30, 33, 325),
                ];

            FontSettings.CopyFrom(AvailableFonts[1]);
        }

        internal static Properties.Settings AppSettings => Properties.Settings.Default;

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

        internal static bool ConfirmDeleteTask { get; set; }

        internal static bool ConfirmCompleteTask { get; set; }

        internal static bool AutoRenewalTasks { get; set; }

        internal static int MaxSectionLength { get; set; }

        internal static int MaxTaskLength { get; set; }

        internal static int PrioritiesSetID { get; set; }

        internal static void FillFromViewModel(SettingsViewModel settingsViewModel)
        {
            SetDefaultSectionName = settingsViewModel.SetDefaultSectionName;
            SetDefaultTaskName = settingsViewModel.SetDefaultTaskName;
            IncrementSectionName = settingsViewModel.IncrementSectionName;
            IncrementTaskName = settingsViewModel.IncrementTaskName;
            DefaultSectionName = settingsViewModel.DefaultSectionName;
            DefaultTaskName = settingsViewModel.DefaultTaskName;
            ConfirmDeleteSection = settingsViewModel.ConfirmDeleteSection;
            ConfirmDeleteTask = settingsViewModel.ConfirmDeleteTask;
            ConfirmCompleteTask = settingsViewModel.ConfirmCompleteTask;
            AutoRenewalTasks = settingsViewModel.AutoRenewalTasks;
            MaxSectionLength = settingsViewModel.MaxSectionLength;
            MaxTaskLength = settingsViewModel.MaxTaskLength;

            if (PrioritiesSetID != settingsViewModel.PrioritiesSetID)
            {
                PrioritiesSetID = settingsViewModel.PrioritiesSetID;
                TaskPrioritiesInstances.ResetPriorities(PrioritiesSetID);
            }

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
            ConfirmDeleteTask = true;
            ConfirmCompleteTask = true;
            AutoRenewalTasks = false;
            MaxSectionLength = 100;
            MaxTaskLength = 100;

            if (PrioritiesSetID != 0)
            {
                PrioritiesSetID = 0;
                TaskPrioritiesInstances.ResetPriorities(PrioritiesSetID);
            }

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
            int fontSettingsID = AppSettings.FontSettingsID;

            if (AvailableFonts.FirstOrDefault(f => f.ID == fontSettingsID) is FontSet fontSettings)
                FontSettings.CopyFrom(fontSettings);

            int themeID = AppSettings.ThemeID;

            if (Enum.IsDefined(typeof(Themes), themeID))
                ChangeTheme((Themes)themeID);

            SetDefaultSectionName = AppSettings.SetDefaultSectionName;
            SetDefaultTaskName = AppSettings.SetDefaultTaskName;
            IncrementSectionName = AppSettings.IncrementSectionName;
            IncrementTaskName = AppSettings.IncrementTaskName;
            DefaultSectionName = AppSettings.DefaultSectionName;
            DefaultTaskName = AppSettings.DefaultTaskName;
            ConfirmDeleteSection = AppSettings.ConfirmDeleteSection;
            ConfirmDeleteTask = AppSettings.ConfirmDeleteTask;
            ConfirmCompleteTask = AppSettings.ConfirmCompleteTask;
            AutoRenewalTasks = AppSettings.AutoRenewalTasks;
            MaxSectionLength = AppSettings.MaxSectionLength;
            MaxTaskLength = AppSettings.MaxTaskLength;

            PrioritiesSetID = AppSettings.PrioritiesSetID;
            TaskPrioritiesInstances.ResetPriorities(PrioritiesSetID);

            Helper.MasterSectionViewModel.MidnightUpdateTasks();

            TaskStatusesInstances.BeginingStatus.TaskVisible = AppSettings.BeginingStatusVisible;
            TaskStatusesInstances.CompletedStatus.TaskVisible = AppSettings.CompletedStatusVisible;
            TaskStatusesInstances.DeferredStatus.TaskVisible = AppSettings.DeferredStatusVisible;
            TaskStatusesInstances.RejectedStatus.TaskVisible = AppSettings.RejectedStatusVisible;
            TaskStatusesInstances.DoneStatus.TaskVisible = AppSettings.DoneStatusVisible;

            Instanse.IndicateByStatus = AppSettings.IndicateByStatus;
            Instanse.IndicateByPriority = AppSettings.IndicateByPriority;
            Instanse.NoneIndicate = AppSettings.NoneIndicate;

            Instanse.SortByStatus = AppSettings.SortByStatus;
            Instanse.SortByPriority = AppSettings.SortByPriority;
            Instanse.SortByName = AppSettings.SortByName;
            Instanse.SortByEndDate = AppSettings.SortByEndDate;
            Instanse.SortByStartDate = AppSettings.SortByStartDate;
            Instanse.DescendingSort = AppSettings.DescendingSort;

            Instanse.ShowTodayTasks = AppSettings.ShowTodayTasks;

            RefreshSectionVisibleCore();
        }

        internal static void SaveToConfig()
        {
            AppSettings.FontSettingsID = FontSettings.ID;
            AppSettings.ThemeID = (int)Theme;

            AppSettings.SetDefaultSectionName = SetDefaultSectionName;
            AppSettings.SetDefaultTaskName = SetDefaultTaskName;
            AppSettings.IncrementSectionName = IncrementSectionName;
            AppSettings.IncrementTaskName = IncrementTaskName;
            AppSettings.DefaultSectionName = DefaultSectionName;
            AppSettings.DefaultTaskName = DefaultTaskName;
            AppSettings.ConfirmDeleteSection = ConfirmDeleteSection;
            AppSettings.ConfirmDeleteTask = ConfirmDeleteTask;
            AppSettings.ConfirmCompleteTask = ConfirmCompleteTask;
            AppSettings.AutoRenewalTasks = AutoRenewalTasks;
            AppSettings.MaxSectionLength = MaxSectionLength;
            AppSettings.MaxTaskLength = MaxTaskLength;
            AppSettings.PrioritiesSetID = PrioritiesSetID;

            AppSettings.BeginingStatusVisible = TaskStatusesInstances.BeginingStatus.TaskVisible;
            AppSettings.CompletedStatusVisible = TaskStatusesInstances.CompletedStatus.TaskVisible;
            AppSettings.DeferredStatusVisible = TaskStatusesInstances.DeferredStatus.TaskVisible;
            AppSettings.RejectedStatusVisible = TaskStatusesInstances.RejectedStatus.TaskVisible;
            AppSettings.DoneStatusVisible = TaskStatusesInstances.DoneStatus.TaskVisible;

            AppSettings.IndicateByStatus = Instanse.IndicateByStatus;
            AppSettings.IndicateByPriority = Instanse.IndicateByPriority;
            AppSettings.NoneIndicate = Instanse.NoneIndicate;

            AppSettings.SortByStatus = Instanse.SortByStatus;
            AppSettings.SortByPriority = Instanse.SortByPriority;
            AppSettings.SortByName = Instanse.SortByName;
            AppSettings.SortByEndDate = Instanse.SortByEndDate;
            AppSettings.SortByStartDate = Instanse.SortByStartDate;
            AppSettings.DescendingSort = Instanse.DescendingSort;

            AppSettings.ShowTodayTasks = Instanse.ShowTodayTasks;

            AppSettings.Save();
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
            internal static bool NotResetIndication { get; set; }

            public bool IndicateByStatus
            {
                get => _indicateByStatus;
                set
                {
                    if (NotResetIndication)
                        return;

                    _indicateByStatus = value;
                    OnPropertyChanged(nameof(IndicateByStatus));
                }
            }

            public bool IndicateByPriority
            {
                get => _indicateByPriority;
                set
                {
                    if (NotResetIndication)
                        return;

                    _indicateByPriority = value;
                    OnPropertyChanged(nameof(IndicateByPriority));
                }
            }

            public bool NoneIndicate
            {
                get => _noneIndicate;
                set
                {
                    if (NotResetIndication)
                        return;

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
