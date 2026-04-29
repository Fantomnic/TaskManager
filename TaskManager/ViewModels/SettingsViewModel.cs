using TaskManager.Model;
using static TaskManager.Helpers.Enums;
using static TaskManager.Model.Settings;

namespace TaskManager.ViewModels
{
    internal class SettingsViewModel : BaseViewModel
    {
        private bool _setDefaultSectionName;
        private bool _setDefaultTaskName;
        private bool _incrementSectionName;
        private bool _incrementTaskName;
        private string _defaultSectionName;
        private string _defaultTaskName;
        private bool _confirmDeleteSection;
        private bool _confirmCompleteTask;
        private bool _autoRenewalTasks;
        internal static int _maxSectionLength;
        internal static int _maxTaskLength;
        private FontSet _fontSettings;
        private Themes _theme;

        internal SettingsViewModel()
        {
            _setDefaultSectionName = Settings.SetDefaultSectionName;
            _setDefaultTaskName = Settings.SetDefaultTaskName;
            _incrementSectionName = Settings.IncrementSectionName;
            _incrementTaskName = Settings.IncrementTaskName;
            _defaultSectionName = Settings.DefaultSectionName;
            _defaultTaskName = Settings.DefaultTaskName;
            _confirmDeleteSection = Settings.ConfirmDeleteSection;
            _confirmCompleteTask = Settings.ConfirmCompleteTask;
            _autoRenewalTasks = Settings.AutoRenewalTasks;
            _maxSectionLength = Settings.MaxSectionLength;
            _maxTaskLength = Settings.MaxTaskLength;
            _theme = Settings.Theme;

            var currentFont = Settings.FontSettings;
            var allFonts = Settings.AvailableFonts;
            _fontSettings = allFonts.FirstOrDefault(f => f.ID == currentFont.ID) ?? allFonts.First();
        }

        public bool SetDefaultSectionName
        {
            get => _setDefaultSectionName;
            set
            {
                _setDefaultSectionName = value;
                OnPropertyChanged(nameof(SetDefaultSectionName));
            }
        }

        public bool SetDefaultTaskName
        {
            get => _setDefaultTaskName;
            set
            {
                _setDefaultTaskName = value;
                OnPropertyChanged(nameof(SetDefaultTaskName));
            }
        }

        public bool IncrementSectionName
        {
            get => _incrementSectionName;
            set
            {
                _incrementSectionName = value;
                OnPropertyChanged(nameof(IncrementSectionName));
            }
        }

        public bool IncrementTaskName
        {
            get => _incrementTaskName;
            set
            {
                _incrementTaskName = value;
                OnPropertyChanged(nameof(IncrementTaskName));
            }
        }

        public string DefaultSectionName
        {
            get => _defaultSectionName;
            set
            {
                _defaultSectionName = value;
                OnPropertyChanged(nameof(DefaultSectionName));
            }
        }

        public string DefaultTaskName
        {
            get => _defaultTaskName;
            set
            {
                _defaultTaskName = value;
                OnPropertyChanged(nameof(DefaultTaskName));
            }
        }

        public bool ConfirmDeleteSection
        {
            get => _confirmDeleteSection;
            set
            {
                _confirmDeleteSection = value;
                OnPropertyChanged(nameof(ConfirmDeleteSection));
            }
        }

        public bool ConfirmCompleteTask
        {
            get => _confirmCompleteTask;
            set
            {
                _confirmCompleteTask = value;
                OnPropertyChanged(nameof(ConfirmCompleteTask));
            }
        }

        public bool AutoRenewalTasks
        {
            get => _autoRenewalTasks;
            set
            {
                _autoRenewalTasks = value;
                OnPropertyChanged(nameof(AutoRenewalTasks));
            }
        }

        public int MaxSectionLength
        {
            get => _maxSectionLength;
            set
            {
                _maxSectionLength = value;
                OnPropertyChanged(nameof(MaxSectionLength));
            }
        }

        public int MaxTaskLength
        {
            get => _maxTaskLength;
            set
            {
                _maxTaskLength = value;
                OnPropertyChanged(nameof(MaxTaskLength));
            }
        }

        public FontSet FontSettings
        {
            get => _fontSettings;
            set
            {
                _fontSettings = value;
                OnPropertyChanged(nameof(FontSettings));
            }
        }

        public Themes Theme
        {
            get => _theme;
            set
            {
                _theme = value;
                OnPropertyChanged(nameof(Theme));
            }
        }
    }
}
