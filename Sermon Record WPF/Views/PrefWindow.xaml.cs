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

namespace Sermon_Record
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

            comboboxRecordingDevice.Text = appPreferences.RecordingDevice;
            textboxSaveLocation.Text = appPreferences.SaveLocation;
            comboboxRecordingRate.Text = appPreferences.RecordingRate.ToString();
            comboboxRecordingDepth.Text = appPreferences.RecordingDepth.ToString();
            comboboxRecordingChannels.Text = appPreferences.RecordingChannels.ToString();
            textboxTempLocation.Text = appPreferences.TempLocation;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            appPreferences.RecordingDevice = comboboxRecordingDevice.Text;
            appPreferences.SaveLocation = textboxSaveLocation.Text;
            appPreferences.RecordingRate = Int32.Parse(comboboxRecordingRate.Text);
            appPreferences.RecordingDepth = Int32.Parse(comboboxRecordingDepth.Text);
            appPreferences.RecordingChannels = Int32.Parse(comboboxRecordingChannels.Text);
            appPreferences.TempLocation = textboxTempLocation.Text;
            ((App)Application.Current).saveAppPrefs();
        }
    }
}
