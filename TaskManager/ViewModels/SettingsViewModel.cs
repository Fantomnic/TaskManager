using TaskManager.Model;
using static TaskManager.Helpers.Enums;
using static TaskManager.Model.Settings;

namespace TaskManager.ViewModels
{
    internal class SettingsViewModel : BaseViewModel
    {
        private bool? _setDefaultSectionName;
        private bool? _setDefaultTaskName;
        private bool? _incrementSectionName;
        private bool? _incrementTaskName;
        private string _defaultSectionName;
        private string _defaultTaskName;
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
            _theme = Settings.Theme;

            var currentFont = Settings.FontSettings;
            var allFonts = Settings.AvailableFonts;
            _fontSettings = allFonts.FirstOrDefault(f => f.ID == currentFont.ID) ?? allFonts.First();
        }

        public bool? SetDefaultSectionName
        {
            get => _setDefaultSectionName;
            set
            {
                _setDefaultSectionName = value;
                OnPropertyChanged(nameof(SetDefaultSectionName));
            }
        }

        public bool? SetDefaultTaskName
        {
            get => _setDefaultTaskName;
            set
            {
                _setDefaultTaskName = value;
                OnPropertyChanged(nameof(SetDefaultTaskName));
            }
        }

        public bool? IncrementSectionName
        {
            get => _incrementSectionName;
            set
            {
                _incrementSectionName = value;
                OnPropertyChanged(nameof(IncrementSectionName));
            }
        }

        public bool? IncrementTaskName
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
