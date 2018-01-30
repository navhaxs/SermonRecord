using Sermon_Record.UTIL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Sermon_Record
{
    /// <summary>
    /// Interaction logic for OverkayWindow.xaml
    /// </summary>
    public partial class OverlayWindow : Window
    {

        public Recorder myrecorder { get { return ((App)Application.Current).Recorder; } }
        public Storyboard animateBlink;

        public OverlayWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            this.Top = 0;
            this.Left = SystemParameters.PrimaryScreenWidth / 2 - this.Width / 2;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            animateBlink = this.FindResource("animateBlink") as Storyboard;
            var ow = ((App)Application.Current).overlayWnd;
            animateBlink.Begin();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();

        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);

            //Set the window style to noactivate.
            WindowInteropHelper helper = new WindowInteropHelper(this);
            SetWindowLong(helper.Handle, GWL_EXSTYLE,
                GetWindowLong(helper.Handle, GWL_EXSTYLE) | WS_EX_NOACTIVATE);
        }

        private const int GWL_EXSTYLE = -20;
        private const int WS_EX_NOACTIVATE = 0x08000000;

        [DllImport("user32.dll")]
        public static extern IntPtr SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll")]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

    }
}
