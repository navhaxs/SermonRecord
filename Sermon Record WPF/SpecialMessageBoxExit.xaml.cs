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

namespace SermonRecord
{
    /// <summary>
    /// Interaction logic for MessageBox.xaml
    /// </summary>
    public partial class SpecialMessageBoxExit : Window
    {
        public SpecialMessageBoxExit()
        {
            InitializeComponent();
        }

        bool result_is_exit = false;

        public bool ShowMessage(Window owner, string title, string caption, string message)
        {
            this.Owner = owner;
            this.Title = title;
            Caption.Text = caption;
            Message.Text = message;
            this.ShowDialog();
            return result_is_exit;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            result_is_exit = false;
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            result_is_exit = true;
            Close();
        }
    }
}
