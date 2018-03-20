using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SermonRecord.OneButton.ViewModels
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string GivenNames { get; set; }
        public string FamilyName { get; set; }
        public string FullName => $"{GivenNames} {FamilyName}";
    }
}
