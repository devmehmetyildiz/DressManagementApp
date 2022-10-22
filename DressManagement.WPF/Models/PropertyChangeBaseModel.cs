using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DressManagement.WPF.Models
{
    public class PropertyChangeBaseModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string propertyName)
        {
            var property = PropertyChanged;
            if (property != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
