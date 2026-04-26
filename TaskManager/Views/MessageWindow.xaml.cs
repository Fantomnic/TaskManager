using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace TaskManager.Views
{
    /// <summary>
    /// Interaction logic for MessageWindow.xaml
    /// </summary>
    public partial class MessageWindow : WindowWithBottomButtons
    {
        public MessageWindow(string message, string title, BitmapSource? iconImage, bool isQuestion = false)
        {
            InitializeComponent();

            if (iconImage is null)
                icon.Visibility = Visibility.Collapsed;
            else
                icon.Source = iconImage;

            this.message.Text = message;
            Title = title;

            if (!isQuestion)
                return;

            okButton.Content = "Да";
            cancelButton.Visibility = Visibility.Visible;
        }
    }
}
