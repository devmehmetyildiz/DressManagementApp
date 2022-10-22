using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DressManagement.WPF.Models.Settings
{
    public class UnitModel : BaseModel
    {
        private string name;

        public string Name
        {
            get => name; set { name = value; RaisePropertyChanged("Name"); }
        }
    }
}
