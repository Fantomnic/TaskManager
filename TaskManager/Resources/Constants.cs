using WinFormsHelper;

namespace TaskManager.Resources
{
    public static class Constants
    {
        private static readonly int _workingAreaHeight;
        private static readonly int _workingAreaWidth;

        static Constants()
        {
            _workingAreaHeight = DisplayManager.GetWorkingAreaHeight();
            _workingAreaWidth = DisplayManager.GetWorkingAreaWidth();
        }

        #region Главное окно

        public const double MinHeightMainWindow = 450;

        public const double MinWidthMainWindow = 800;

        public static double StartHeightMainWindow => _workingAreaHeight / 2;

        public static double StartWidthMainWindow => _workingAreaWidth / 2;

        #endregion Главное окно

        #region Маленькое окно

        public const double MinHeightLittleWindow = 220;

        public const double MinWidthLittleWindow = 400;

        public static double StartHeightLittleWindow => _workingAreaHeight / 5;

        public static double StartWidthLittleWindow => _workingAreaWidth / 5;

        #endregion Маленькое окно

        #region Среднее окно

        public const double MinHeightMiddleWindow = 350;

        public const double MinWidthMiddleWindow = 600;

        public static double StartHeightMiddleWindow => _workingAreaHeight / 3;

        public static double StartWidthMiddleWindow => _workingAreaWidth / 3;

        #endregion Среднее окно
    }
}
