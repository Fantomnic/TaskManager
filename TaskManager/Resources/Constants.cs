using WinFormsHelper;

namespace TaskManager.Resources
{
    public static class Constants
    {
        #region Эталонные размеры

        public const double StandartBaseFont = 12;

        #endregion Эталонные размеры

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

        public const double MinHeightLittleWindow = 325;

        public const double MinWidthLittleWindow = 450;

        public static double StartHeightLittleWindow => _workingAreaHeight / 14;

        public static double StartWidthLittleWindow => _workingAreaWidth / 14;

        #endregion Маленькое окно

        #region Среднее окно

        public const double MinHeightMiddleWindow = 350;

        public const double MinWidthMiddleWindow = 600;

        public static double StartHeightMiddleWindow => _workingAreaHeight / 2.5;

        public static double StartWidthMiddleWindow => _workingAreaWidth / 2.5;

        #endregion Среднее окно
    }
}
