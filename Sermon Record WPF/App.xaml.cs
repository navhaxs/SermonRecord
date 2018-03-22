using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using SermonRecord.UTIL;

namespace SermonRecord
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        public AppPreferences Options;
        public UTIL.Recorder Recorder;
        public UTIL.AudioDevice AudioDevice;
        public OverlayWindow overlayWnd;
        public MainWindow mainWnd;

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            loadAppPrefs();
            Recorder = new UTIL.Recorder();
            AudioDevice = new UTIL.AudioDevice();        
            overlayWnd = new OverlayWindow();
        }

        #region "Application preferences"
        const string CONFIG_FILE = "config.xml";

        private void loadAppPrefs()
        {
            if (!File.Exists(CONFIG_FILE))
            {
                Options = new AppPreferences();
            } else {
                System.Xml.Serialization.XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(typeof(AppPreferences));
                StreamReader file = new StreamReader(CONFIG_FILE);
                Options = (AppPreferences)reader.Deserialize(file) ?? new AppPreferences();
                file.Close();
            }
            if (Options.Services == null) Options.Services = new List<Service> {
                new Service { Name = "Kingsgrove 11am" }
            };
            if (!Directory.Exists(Options.TempLocation)) Options.TempLocation = Path.GetTempPath();
        }

        public void saveAppPrefs()
        {
            var writer = new System.Xml.Serialization.XmlSerializer(typeof(AppPreferences));
            var wfile = new StreamWriter(CONFIG_FILE);
            writer.Serialize(wfile, Options);
            wfile.Close();
        }
        #endregion

        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            CrashHelpMe.CreateCrashReport(e.Exception);
        }
    }
}
