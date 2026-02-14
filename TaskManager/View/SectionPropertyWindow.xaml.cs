using System;
using System.Collections.Generic;
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

namespace TaskManager.View
{
    /// <summary>
    /// Interaction logic for SectionPropertyWindow.xaml
    /// </summary>
    public partial class SectionPropertyWindow : Window
    {
        public SectionPropertyWindow()
        {
            InitializeComponent();
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateName())
                return;

            DialogResult = true;
            Close();
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private bool ValidateName()
        {
            string name = sectionName.Text;

            if (String.IsNullOrEmpty(name))
            {
                MessageBox.Show("Укажите название раздела");
                return false;
            }

            if (name == "Test")
            {
                MessageBox.Show($"Раздел \"{name}\" уже существует");
                return false;
            }

            return true;
        }
    }
}
