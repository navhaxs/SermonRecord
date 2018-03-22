using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for PrefWindow.xaml
    /// </summary>
    public partial class PrefWindow : Window
    {
        static AppPreferences appPreferences = ((App)Application.Current).Options;

        public PrefWindow()
        {
            InitializeComponent();
            comboboxRecordingDevice.ItemsSource = UTIL.AudioDevice.GetAvailableDevices();

            // TODO: Model
            comboboxRecordingDevice.Text = appPreferences.RecordingDevice;
            comboboxRecordingRate.Text = appPreferences.RecordingRate.ToString();
            comboboxRecordingDepth.Text = appPreferences.RecordingDepth.ToString();
            comboboxRecordingChannels.Text = appPreferences.RecordingChannels.ToString();
            autoMinimise.IsChecked = appPreferences.AutoMinimise;
            textboxTempLocation.Text = appPreferences.TempLocation;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!Directory.Exists(appPreferences.TempLocation))
            {
                MessageBox.Show("Buffer location does not exist. Using default.");
                //textboxTempLocation.Text = System.IO.Path.GetTempPath();
            }

            appPreferences.RecordingDevice = comboboxRecordingDevice.Text;
            appPreferences.RecordingRate = Int32.Parse(comboboxRecordingRate.Text);
            appPreferences.RecordingDepth = Int32.Parse(comboboxRecordingDepth.Text);
            appPreferences.RecordingChannels = Int32.Parse(comboboxRecordingChannels.Text);
            appPreferences.AutoMinimise = autoMinimise.IsChecked == true;
            appPreferences.TempLocation = textboxTempLocation.Text;
            ((App)Application.Current).saveAppPrefs();
            Close();
        }

    }
}
