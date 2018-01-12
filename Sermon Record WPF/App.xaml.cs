using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Sermon_Record
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        public AppPreferences Options;
        public UTIL.Recorder Recorder;
        public UTIL.AudioDevice AudioDevice;

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            loadAppPrefs();
            Recorder = new UTIL.Recorder();
            AudioDevice = new UTIL.AudioDevice();
        }

        #region "Application preferences"
        const string CONFIG_FILE = "config.xml";

        private void loadAppPrefs()
        {
            if (!System.IO.File.Exists(CONFIG_FILE))
            {
                Options = new AppPreferences();
                return;
            }

            System.Xml.Serialization.XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(typeof(AppPreferences));
            System.IO.StreamReader file = new System.IO.StreamReader(CONFIG_FILE);
            Options = (AppPreferences)reader.Deserialize(file) ?? new AppPreferences();
            file.Close();
        }

        public void saveAppPrefs()
        {
            var writer = new System.Xml.Serialization.XmlSerializer(typeof(AppPreferences));
            var wfile = new System.IO.StreamWriter(CONFIG_FILE);
            writer.Serialize(wfile, Options);
            wfile.Close();
        }
        #endregion
    }
}
