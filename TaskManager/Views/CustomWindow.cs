using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace TaskManager.Views
{
    public class CustomWindow : Window
    {
        public static readonly DependencyProperty ShowMinMaxButtonsProperty
            = DependencyProperty.Register(nameof(ShowMinMaxButtons), typeof(bool), typeof(CustomWindow));

        public static readonly DependencyProperty MaximizeButtonsDataProperty
            = DependencyProperty.Register(nameof(MaximizeButtonsData), typeof(PathGeometry), typeof(CustomWindow));

        private static readonly PathGeometry _toMaximizePathData = new()
        {
            Figures =
                [
                    new(new(0,0),
                        [
                            new LineSegment(new(0,9), true),
                            new LineSegment(new(9,9), true),
                            new LineSegment(new(9,0), true),
                        ],
                        true),
                ],
        };

        private static readonly PathGeometry _toNormalPathData = new()
        {
            Figures =
                [
                    new(new(0,2),
                        [
                            new LineSegment(new(0,10), true),
                            new LineSegment(new(8,10), true),
                            new LineSegment(new(8,2), true),
                        ],
                        true),
                    new(new(2,2),
                        [
                            new LineSegment(new(2,0), true),
                            new LineSegment(new(10,0), true),
                            new LineSegment(new(10,8), true),
                            new LineSegment(new(8,8), true),
                        ],
                        false)
                ],
        };

        private static readonly Thickness _maximizeThickness = new(5, 0, 7, 7);
        private static readonly Thickness _normalThickness = new(0);

        public CustomWindow() : base()
        {
            ShowMinMaxButtons = true;

            var closeCommandBinding = new CommandBinding
            {
                Command = SystemCommands.CloseWindowCommand,
            };

            closeCommandBinding.Executed += ExecutedCloseCommand;

            var maximizeCommandBinding = new CommandBinding
            {
                Command = SystemCommands.MaximizeWindowCommand,
            };

            maximizeCommandBinding.Executed += ExecutedMaximizeCommand;

            var minimizeCommandBinding = new CommandBinding
            {
                Command = SystemCommands.MinimizeWindowCommand,
            };

            minimizeCommandBinding.Executed += ExecutedMinimizeCommand;

            CommandBindings.AddRange(new[] { closeCommandBinding, maximizeCommandBinding, minimizeCommandBinding });
            SizeChanged += OnWindowSizeChanged;
        }

        public bool ShowMinMaxButtons
        {
            get => (bool)GetValue(ShowMinMaxButtonsProperty);
            set => SetValue(ShowMinMaxButtonsProperty, value);
        }

        public PathGeometry MaximizeButtonsData
        {
            get => (PathGeometry)GetValue(MaximizeButtonsDataProperty);
            set => SetValue(MaximizeButtonsDataProperty, value);
        }

        private void OnWindowSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (!e.WidthChanged || !ShowMinMaxButtons)
                return;

            if (WindowState == WindowState.Maximized)
            {
                Margin = _maximizeThickness;
                MaximizeButtonsData = _toNormalPathData;
                return;
            }

            Margin = _normalThickness;
            MaximizeButtonsData = _toMaximizePathData;
        }

        private void ExecutedMinimizeCommand(object sender, ExecutedRoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void ExecutedMaximizeCommand(object sender, ExecutedRoutedEventArgs e)
        {
            MaximizeCore();
        }

        protected void MaximizeCore()
        {
            if (WindowState == WindowState.Maximized)
                WindowState = WindowState.Normal;
            else
                WindowState = WindowState.Maximized;
        }

        private void ExecutedCloseCommand(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }
    }
}
