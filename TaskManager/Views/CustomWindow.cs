using System.Windows;
using System.Windows.Input;

namespace TaskManager.Views
{
    public class CustomWindow : Window
    {
        public static readonly DependencyProperty ShowMinMaxButtonsProperty
            = DependencyProperty.Register(nameof(ShowMinMaxButtons), typeof(bool), typeof(CustomWindow));

        private static readonly ResourceDictionary _normalStyle;
        private static readonly ResourceDictionary _maximizedStyle;

        static CustomWindow()
        {
            _normalStyle = new()
            {
                Source = new("/Resources/CustomWindowResources/WindowNormalResources.xaml", UriKind.Relative),
            };

            _maximizedStyle = new()
            {
                Source = new("/Resources/CustomWindowResources/WindowMaximizeResources.xaml", UriKind.Relative),
            };

            Application.Current.Resources.MergedDictionaries.Add(_normalStyle);
        }

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

        private void OnWindowSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.WidthChanged)
            {
                var dictionaries = Application.Current.Resources.MergedDictionaries;

                if (WindowState == WindowState.Maximized)
                {
                    dictionaries.Remove(_normalStyle);
                    dictionaries.Add(_maximizedStyle);
                }
                else if (!dictionaries.Any(d => d == _normalStyle)) // Если уже установлен, ничего не делаем
                {
                    dictionaries.Remove(_maximizedStyle);
                    dictionaries.Add(_normalStyle);
                }
            }
        }

        // TODO: Сделать кастомные команды
        private void ExecutedMinimizeCommand(object sender, ExecutedRoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void ExecutedMaximizeCommand(object sender, ExecutedRoutedEventArgs e)
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
