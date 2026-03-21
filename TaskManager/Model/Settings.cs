using TaskManager.ViewModel;

namespace TaskManager.Model
{
    internal static class Settings
    {
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
        }
    }
}
