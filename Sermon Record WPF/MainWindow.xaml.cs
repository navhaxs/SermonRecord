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
using SermonRecord.UTIL;
using static SermonRecord.UTIL.Recorder;

namespace SermonRecord
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public Recorder myrecorder { get { return ((App)Application.Current).Recorder; } }

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(1);
            timer.Tick += timer_Tick;
            timer.Start();


        }

        public string RecordButtonToolTip { get; set; } = "Start recording";

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ((App)Application.Current).AudioDevice.Open();
            //((App)Application.Current).overlayWnd.Owner = this;
            ((App)Application.Current).overlayWnd.Show();
            ((App)Application.Current).mainWnd = this;
        }

        //int tmr = 0;

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

                if (!myrecorder.IsRecording) recordButton.IsEnabled = false;
            } else
            {
                lblMicProblem.Visibility = Visibility.Collapsed;
                recordButton.IsEnabled = true;
            }

            //tmr++;
            //if (tmr % 200 == 0)
            //{
            //    BufferFileSizeText.Text = myrecorder.FileSizeF();
            //}
        }

        // I don't know why you're giving me that look
        public void updateUI()
        {
            switch (myrecorder.MyRecordingState)
            {
                case RecorderState.Recording:
                    RecordButtonToolTip = "Stop recording";
                    break;
                case RecorderState.Stopped:
                    RecordButtonToolTip = "Start recording";
                    break;
                case RecorderState.Done:
                    RecordButtonToolTip = "Finish and save MP3";
                    break;
                case RecorderState.Paused:
                    RecordButtonToolTip = "Resume recording";
                    break;
                default:
                    break;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            Storyboard animation;

            if (recordButton.IsChecked == false)
            {

                {
                    animation = this.FindResource("animateBackgroundReturn") as Storyboard;
                    this.BeginStoryboard(animation);
                }

                myrecorder.Stop();

                //((App)Application.Current).overlayWnd.Hide();
                this.Visibility = Visibility.Hidden;

                try
                {
                    var p = new PostWindow();
                    p.ShowDialog();

                    myrecorder.Reset();

                    var x = new System.Windows.Shell.TaskbarItemInfo();
                    x.ProgressState = System.Windows.Shell.TaskbarItemProgressState.Normal;
                    this.TaskbarItemInfo = x;

                    this.Visibility = Visibility.Visible;
                    ((App)Application.Current).overlayWnd.Show();

                } catch (Exception ex)
                {
                    CrashHelpMe.CreateCrashReport(ex);

                    {
                        try
                        {
                            if (System.IO.File.Exists(myrecorder.FilePath))
                            {

                                var emergency_wav = System.IO.Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop), myrecorder.FileName + ".wav");
                                System.IO.File.Copy(myrecorder.FilePath, emergency_wav);

                                var x1 = new SpecialMessageBox();
                                x1.ShowMessage(this, "Unexpected error", "Due to an error, the MP3 may not have been saved.", String.Format("The app ran into a problem. As a precaution, the recording WAV file has been copied to your Desktop. You can continue editing the WAV file in Audacity, etc. from there.\n{0}", ex.Message));

                                System.Diagnostics.Process.Start("explorer.exe", "/select,\"" + emergency_wav + "\"");

                            }
                            else
                            {
                                var x1 = new SpecialMessageBox();
                                x1.ShowMessage(this, "Unexpected error", "Due to an error, the recording seems to have failed.", String.Format("The app ran into a problem. The WAV file may have failed to record.\n{0}", ex.Message));
                                System.Diagnostics.Process.Start("explorer.exe", "/select,\"" + ((App)Application.Current).Options.TempLocation + "\"");

                            }

                        }
                        catch (Exception ex2)
                        {
                            var x1 = new SpecialMessageBox();
                            x1.ShowMessage(this, "Unexpected error", "Due to an error, the MP3 may not have been saved.", String.Format("The app ran into a problem:\n{0}", ex2.Message));

                            System.Diagnostics.Process.Start("explorer.exe", "/select,\"" + myrecorder.FilePath + "\"");
                        }

                    }
                    Close();
                }


            }
            else
            {
                {
                    animation = this.FindResource("animateBackground") as Storyboard;
                    this.BeginStoryboard(animation);
                }

                {
                    
                    Dispatcher.Invoke(DispatcherPriority.Normal, (Action)delegate () {
                        OverlayWindow ow = ((App)Application.Current).overlayWnd;
                        ow.animateBlink.Stop(); });
                    
                }
                
                myrecorder.Record();

                var x = new System.Windows.Shell.TaskbarItemInfo();
                x.ProgressState = System.Windows.Shell.TaskbarItemProgressState.Error;
                x.ProgressValue = 1;
                this.TaskbarItemInfo = x;

                if (((App)Application.Current).Options.AutoMinimise)
                    this.WindowState = WindowState.Minimized;
            }

            updateUI();
        }


        private void Setup_Button_Click(object sender, RoutedEventArgs e)
        {
            // Warning: This will interrupt recording
            // So only allow setup when Stopped

            ((App)Application.Current).AudioDevice.Close();

            var p = new PrefWindow();
            p.Owner = this;
            p.ShowDialog();

            ((App)Application.Current).AudioDevice.Open();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (myrecorder.IsRecording)
            {
                if (MessageBox.Show("Are you sure you want to stop and discard the entire recording? It will be lost forever!", "Discard recording", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    myrecorder.Stop();

                    System.IO.File.Delete(myrecorder.FilePath);
                }
                else
                {
                    e.Cancel = true;
                }
            }

            ((App)Application.Current).overlayWnd.Close();

        }

        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            Topmost = true;
        }

        private void pinBtn_Unchecked(object sender, RoutedEventArgs e)
        {
            Topmost = false;
        }

    }
}
