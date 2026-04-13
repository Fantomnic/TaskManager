using System.Windows;
using System.Windows.Input;

namespace TaskManager.Views
{
    public class CustomWindow : Window
    {
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
            var closeCommandBinding = new CommandBinding
            {
                Command = ApplicationCommands.Close
            };

            closeCommandBinding.Executed += ExecutedCloseCommand;

            var openCommandBinding = new CommandBinding
            {
                Command = ApplicationCommands.Open,
            };

            openCommandBinding.Executed += ExecutedOpenCommand;

            CommandBindings.AddRange(new[] { closeCommandBinding, openCommandBinding });
            SizeChanged += OnWindowSizeChanged;
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
        private void ExecutedMinimizedCommand(object sender, ExecutedRoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void ExecutedOpenCommand(object sender, ExecutedRoutedEventArgs e)
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
