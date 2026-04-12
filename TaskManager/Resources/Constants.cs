using System.Windows;
using WinFormsHelper;

namespace TaskManager.Resources
{
    public static class Constants
    {
        public const double WindowButtonWidth = 44;

        #region Текст

        public const double StandartBaseFont = 12;
        internal static readonly string DashSeparator40 = new('-', 40);

        #endregion Текст

        public const double DescriptonWidth = 150;
        public static readonly GridLength DescriptonWidthGridLength = new(DescriptonWidth);

        public const double FieldAreaHeight = 35;

        public static readonly Thickness UniformMarginBase = new(5);

        private static readonly int _workingAreaHeight;
        private static readonly int _workingAreaWidth;

        static Constants()
        {
            _workingAreaHeight = DisplayManager.GetWorkingAreaHeight();
            _workingAreaWidth = DisplayManager.GetWorkingAreaWidth();
        }

        #region Главное окно

        public const double MinHeightMainWindow = 450;

        public const double MinWidthMainWindow = 850;

        public static double StartHeightMainWindow => _workingAreaHeight / 2;

        public static double StartWidthMainWindow => _workingAreaWidth / 2;

        #endregion Главное окно

        #region Маленькое окно

        public const double MinHeightLittleWindow = 375;

        public const double MinWidthLittleWindow = 450;

        public static double StartHeightLittleWindow => _workingAreaHeight / 4;

        public static double StartWidthLittleWindow => _workingAreaWidth / 4;

        #endregion Маленькое окно

        #region Среднее окно

        public const double MinHeightMiddleWindow = 400;

        public const double MinWidthMiddleWindow = 600;

        public static double StartHeightMiddleWindow => _workingAreaHeight / 2.5;

        public static double StartWidthMiddleWindow => _workingAreaWidth / 2.5;

        #endregion Среднее окно

        public const double MinTasksListWidth = 250;

        public const double MinTaskPropertyWidth = 500;
    }
}
