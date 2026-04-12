using System.Windows;
using System.Windows.Controls;

namespace TaskManager.CustomControls
{
    public class CheckBoxWithItemStyle : CheckBox
    {
        public static readonly DependencyProperty TextBlockStyleProperty;

        static CheckBoxWithItemStyle()
        {
            TextBlockStyleProperty = DependencyProperty.Register(nameof(TextBlockStyle), typeof(Style), typeof(CheckBoxWithItemStyle));
        }

        public Style TextBlockStyle
        {
            get => (Style)GetValue(TextBlockStyleProperty);
            set => SetValue(TextBlockStyleProperty, value);
        }
    }
}
