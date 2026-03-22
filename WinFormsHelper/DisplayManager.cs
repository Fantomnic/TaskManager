using System.Windows.Forms;

namespace WinFormsHelper
{
    // Вынес в отдельный проект с поддержкой System.Windows.Forms.dll, чтобы не возникало конфликтов с классами WPF
    public static class DisplayManager
    {
        /// <summary>Получить высоту экрана (в пикселях)</summary>
        public static int GetScreenHeight() => Screen.PrimaryScreen.Bounds.Height;

        /// <summary>Получить ширину экрана (в пикселях)</summary>
        public static int GetScreenWidth() => Screen.PrimaryScreen.Bounds.Width;

        /// <summary>Получить высоту рабочей области (в пикселях)</summary>
        public static int GetWorkingAreaHeight() => Screen.PrimaryScreen.WorkingArea.Height;

        /// <summary>Получить ширину рабочей области экрана (в пикселях)</summary>
        public static int GetWorkingAreaWidth() => Screen.PrimaryScreen.WorkingArea.Width;
    }
}
