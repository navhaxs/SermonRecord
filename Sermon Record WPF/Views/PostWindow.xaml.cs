using Sermon_Record.Models;
using Sermon_Record.UTIL;
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
    /// Interaction logic for PostWindow.xaml
    /// </summary>
    public partial class PostWindow : Window
    {
        public PostWindow()
        {
            InitializeComponent();
            mypostrecord = new PostRecord(this);
            this.DataContext = this;
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            if (myrecorder.FilePath == null || !System.IO.File.Exists(myrecorder.FilePath))
            {
                MessageBox.Show("Recording error. No data was captured :(");

                recording_is_unsaved = false;
                Close();
            }


            if (comboboxService.HasItems) comboboxService.SelectedIndex = 0;

            updateSaveLocations();

        }

        static AppPreferences appPreferences = ((App)Application.Current).Options;
        Recorder myrecorder = ((App)Application.Current).Recorder;
        public PostRecord mypostrecord { get; set; }

        public List<string> ServiceList { get { return appPreferences.Services.Select(o => o.Name).ToList(); } }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {

            System.Diagnostics.Process.Start("explorer.exe", "/select,\"" + myrecorder.FilePath + "\"");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // save
            progressbarExport.IsIndeterminate = true;

            this.Cursor = Cursors.Wait;

            if (mypostrecord.SaveMP3(new List<string>() { MP3CloudPathTextBox.Text, MP3LocalPathTextBox.Text }))
            {
                progressbarExport.IsIndeterminate = false;

                // Done msg
                var x = new SpecialMessageBoxExit();
                if (x.ShowMessage(this, "Success", "All done!", String.Format("The MP3 has been saved as \"{0}.mp3\".", myrecorder.FileName)) == true)
                {
                    // result_is_exit
                    ((App)Application.Current).Shutdown();
                }

                // actually delete the WAV
                System.IO.File.Delete(myrecorder.FilePath);

                recording_is_unsaved = false;

                Close();


            };

            progressbarExport.IsIndeterminate = false;
            this.Cursor = Cursors.Arrow;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public bool recording_is_unsaved = true;
        
        private void Hyperlink_Click_1(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Override default save location?\nNote: To make permanent changes to the save location, edit config.xml", "Edit save location", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                MP3CloudPathTextBox.IsEnabled = true;
                MP3LocalPathTextBox.IsEnabled = true;
                ManualEditSaveLocationLink.IsEnabled = false;
                ManualEditSaveLocationLink.Visibility = Visibility.Collapsed;
            }
        }

        private void comboboxService_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            updateSaveLocations();
        }

        private void comboboxService_DropDownClosed(object sender, EventArgs e)
        {
            updateSaveLocations();
        }

        void updateSaveLocations()
        {
            bool found = searchit();

            MP3CloudPathTextBox.IsEnabled = !found;
            MP3LocalPathTextBox.IsEnabled = !found;

            if (!found)
            {
                ManualEditSaveLocationLink.IsEnabled = false;
                ManualEditSaveLocationLink.Visibility = Visibility.Collapsed;
            } else
            {
                ManualEditSaveLocationLink.IsEnabled = true;
                ManualEditSaveLocationLink.Visibility = Visibility.Visible;
            }
        }

        bool searchit()
        {

            var target = comboboxService.Text;
            if (target == null) return false;

            var service = appPreferences.Services.Find(
                    s => (s.Name == target)
                );
            if (service == null) return false;

            MP3CloudPathTextBox.Text = service.DefaultCloudSaveLocation;
            MP3LocalPathTextBox.Text = service.DefaultMyPCSaveLocation;
            return true;
        }

        private void comboboxService_LostFocus(object sender, RoutedEventArgs e)
        {
            updateSaveLocations();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (recording_is_unsaved) {
                if (MessageBox.Show("Are you sure you want to discard the entire recording? It will be lost forever!", "Discard recording", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    System.IO.File.Delete(myrecorder.FilePath);

                } else {
                    e.Cancel = true;
                }
            }

        }

        private void comboboxService_KeyDown(object sender, KeyEventArgs e)
        {
            updateSaveLocations();

        }

        //C:\Program Files (x86)\Audacity\audacity.exe
        //ServiceList_Selected
    }
}
