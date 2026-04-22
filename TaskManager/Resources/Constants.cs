using System.Windows;
using WinFormsHelper;

namespace TaskManager.Resources
{
    public static class Constants
    {
        public const double WindowButtonWidth = 45;
        //public static readonly double WindowIconWidth;

        public const string DataDirectoty = "Data";
        public const string TasksDirectoty = "Tasks";
        public const string SectionsDirectoty = "Sections";
        public const string DataExtension = ".dtmo";

        #region Текст

        public const double StandartBaseFont = 12;
        public const double StandartMenuFont = 18;
        internal static readonly string DashSeparator40 = new('-', 40);

        #endregion Текст

        public const double MaxWidthServiceButtonSpecial = 255;

        public const double MenuButtonHeight = 50;

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
            //WindowIconWidth = WindowButtonWidth * 3;
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

        #region Окно сообщения

        public const double HeightMessageWindow = 205;

        public const double WidthMessageWindow = 425;

        #endregion Окно сообщения

        public const double MinTasksListWidth = 250;

        public const double MinTaskPropertyWidth = 500;
    }
}
