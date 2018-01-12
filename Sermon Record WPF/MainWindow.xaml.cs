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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Sermon_Record.UTIL;

namespace Sermon_Record
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public Recorder myrecorder { get {return ((App)Application.Current).Recorder; } }

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            double val = UTIL.AudioDevice.PeakValueDb;

            if (Double.IsNegativeInfinity(val)) {
                lblPeakValue.Value = lblPeakValue.Minimum;
            } else if (Double.IsPositiveInfinity(val)) {
                lblPeakValue.Value = lblPeakValue.Maximum;
            } else {
                lblPeakValue.Value = val + 66; // dirty hack
            }


            if (Double.IsNegativeInfinity(val))
            {
                //TODO: weak singal
                lblMicProblem.Visibility = Visibility.Visible;
            } else
            {
                lblMicProblem.Visibility = Visibility.Collapsed;
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            Storyboard animation;

            if (recordButton.IsChecked == false)
            {

                animation = this.FindResource("animateBackgroundReturn") as Storyboard;
                this.BeginStoryboard(animation);

                myrecorder.Stop();

                this.Visibility = Visibility.Hidden;
                var deleteme = new PostWindow();
                deleteme.ShowDialog();
                this.Visibility = Visibility.Visible;

            }
            else
            {

                animation = this.FindResource("animateBackground") as Storyboard;
                this.BeginStoryboard(animation);

                myrecorder.Record();
            }


        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ((App)Application.Current).AudioDevice.Open();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            // Warning: This will interrupt recording

            ((App)Application.Current).AudioDevice.Close();


            var p = new PrefWindow();
            p.ShowDialog();

            ((App)Application.Current).AudioDevice.Open();
        }
    }
}
