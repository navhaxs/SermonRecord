//#define EXPERIMENTAL_ENABLE_CLICKTHROUGH

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
        /****************
          References
         ****************/
        public Recorder myrecorder { get { return ((App)Application.Current).Recorder; } }

        public bool IsDragging { get; private set; }

        /****************
          Variables
         ****************/
        public Storyboard animateBlink;
        private Point _startPoint;

        public OverlayWindow()
        {
            InitializeComponent();
            this.DataContext = this;

            this.Top = 0;
            this.Left = SystemParameters.PrimaryScreenWidth / 2 - this.Width / 2;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // set up animations
            animateBlink = this.FindResource("animateBlink") as Storyboard;
            var ow = ((App)Application.Current).overlayWnd;
            animateBlink.Begin();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                // MouseDrag anywhere on window
                //this.DragMove();
            }
            else
            {
            }
            _startPoint = e.GetPosition(this);
            System.Diagnostics.Debug.Print(e.ClickCount.ToString());
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);

            #if EXPERIMENTAL_ENABLE_CLICKTHROUGH
            {
                //Set the window style to noactivate.
                WindowInteropHelper helper = new WindowInteropHelper(this);
                SetWindowLong(helper.Handle, GWL_EXSTYLE,
                GetWindowLong(helper.Handle, GWL_EXSTYLE) | WS_EX_NOACTIVATE);
            }
            #endif
        }

        private void Window_MouseUp(object sender, MouseButtonEventArgs e)
        {
            // Focus main window
            if (_startPoint == e.GetPosition(this))
               ((App)Application.Current).mainWnd.Focus();

        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {

            if (e.LeftButton == MouseButtonState.Pressed)
            {
                // MouseDrag anywhere on window
                this.DragMove();
            }
        }


#if EXPERIMENTAL_ENABLE_CLICKTHROUGH
        private const int GWL_EXSTYLE = -20;
        private const int WS_EX_NOACTIVATE = 0x08000000;

        [DllImport("user32.dll")]
        public static extern IntPtr SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll")]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);
#endif
    }
}
