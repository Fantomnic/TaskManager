using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TaskManager.Views
{
    /// <summary>
    /// Interaction logic for MessageWindow.xaml
    /// </summary>
    public partial class MessageWindow : WindowWithBottomButtons
    {
        public MessageWindow(string message, string title, BitmapSource? iconImage)
        {
            InitializeComponent();

            if (iconImage is null)
                icon.Visibility = Visibility.Collapsed;
            else
                icon.Source = iconImage;

            this.message.Text = message;
            Title = title;
        }
    }
}
