using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using TaskManager.Helpers;

namespace TaskManager.CustomControls
{
    /// <summary>
    /// Interaction logic for IntegerCounter.xaml
    /// </summary>
    public partial class IntegerCounter : UserControl
    {
        public static readonly DependencyProperty MaxValueProperty = DependencyProperty.Register(nameof(MaxValue), typeof(int), typeof(IntegerCounter));
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(nameof(Value), typeof(int), typeof(IntegerCounter));
        public static readonly DependencyProperty IsReadOnlyProperty = DependencyProperty.Register(nameof(IsReadOnly), typeof(bool), typeof(IntegerCounter));

        private bool _checkChangedText = true;
        private bool _borderIsFocused;

        public IntegerCounter()
        {
            InitializeComponent();

            var bindingReadOnly = new Binding
            {
                ElementName = "digitalText",
                Path = new PropertyPath("IsReadOnly")
            };

            SetBinding(IsReadOnlyProperty, bindingReadOnly);
        }

        public int MaxValue
        {
            get => (int)GetValue(MaxValueProperty);
            set => SetValue(MaxValueProperty, value);
        }

        public int Value
        {
            get => (int)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public bool IsReadOnly
        {
            get => (bool)GetValue(IsReadOnlyProperty);
            set => SetValue(IsReadOnlyProperty, value);
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (MaxValue == 0)
                MaxValue = 999;

            DigitalTextTextChangedCore(digitalText);
        }

        private void DigitalTextSizeChanged(object sender, SizeChangedEventArgs e)
        {
            double buttonHeight = digitalText.ActualHeight / 2;
            upButton.Height = buttonHeight;
            downButton.Height = buttonHeight;
        }

        private void DigitalTextTextChanged(object sender, TextChangedEventArgs e)
        {
            if (!_checkChangedText || MaxValue == 0)
            {
                _checkChangedText = true;
                return;
            }

            var textBox = (TextBox)sender;
            DigitalTextTextChangedCore(textBox);
        }

        private void DigitalTextTextChangedCore(TextBox textBox)
        {
            string newString = textBox.Text;

            if (String.IsNullOrWhiteSpace(newString))
            {
                SetNewValue(0);
                return;
            }

            if (!Int32.TryParse(textBox.Text, out int newInt))
                SetNewValue(Value);
            else if (newInt > MaxValue)
                SetNewValue(MaxValue);
            else
                SetNewValue(newInt, false);
        }

        private void UpClick(object sender, RoutedEventArgs e)
        {
            if (!Int32.TryParse(digitalText.Text, out int currentInt) || currentInt == MaxValue)
                return;

            SetNewValue(++currentInt);
        }

        private void DownClick(object sender, RoutedEventArgs e)
        {
            if (!Int32.TryParse(digitalText.Text, out int currentInt) || currentInt == 0)
                return;

            SetNewValue(--currentInt);
        }

        internal void SetNewValue(int newValue, bool changeText = true)
        {
            if (Value != newValue)
                Value = newValue;

            if (changeText)
            {
                _checkChangedText = false;
                digitalText.Text = newValue.ToString();
            }
        }

        private void BorderMouseEnter(object sender, MouseEventArgs e)
        {
            if (_borderIsFocused)
                return;

            string resourceName = IsReadOnly ? "FieldIsMouseOverBorderReadOnly" : "FieldIsMouseOverBorder";
            ((Border)sender).BorderBrush = Helper.GetResource<SolidColorBrush>(resourceName);
        }

        private void BorderMouseLeave(object sender, MouseEventArgs e)
        {
            if (_borderIsFocused)
                return;

            SetDefaultBorder((Border)sender);
        }

        private void BorderGotFocus(object sender, RoutedEventArgs e)
        {
            _borderIsFocused = true;

            string resourceName = IsReadOnly ? "FieldIsFocusedBorderReadOnly" : "FieldIsFocusedBorder";
            ((Border)sender).BorderBrush = Helper.GetResource<SolidColorBrush>(resourceName);
        }

        private void BorderLostFocus(object sender, RoutedEventArgs e)
        {
            _borderIsFocused = false;

            SetDefaultBorder((Border)sender);
        }

        private void SetDefaultBorder(Border border)
            => border.BorderBrush = Helper.GetResource<SolidColorBrush>("DefaultBorder");
    }
}
