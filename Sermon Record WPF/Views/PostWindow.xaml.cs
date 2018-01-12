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
            this.DataContext = this;
        }

        static AppPreferences appPreferences = ((App)Application.Current).Options;

        public List<string> ServiceList { get { return appPreferences.Services.Select(o => o.Name).ToList(); } }

        public string SermonService { get; set; }
        public string SermonTitle { get; set; }
        public string SermonSeries { get; set; }
        public string SermonPassage { get; set; }
        public string SermonSpeaker { get; set; }
        public string MP3CloudPath { get; set; }
        public string MP3LocalPath { get; set; }
        //ServiceList_Selected
    }
}
