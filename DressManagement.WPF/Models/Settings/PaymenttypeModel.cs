using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DressManagement.WPF.Models.Settings
{
    public class PaymenttypeModel : BaseModel
    {
        private string name;
        private double taxvalue;
        private bool isSupertaxRequired;
        private double supertaxvalue;

        public string Name { get => name; set { name = value; RaisePropertyChanged("Id"); } }
        public double Taxvalue { get => taxvalue; set { taxvalue = value; RaisePropertyChanged("Taxvalue"); } }
        public bool IsSupertaxRequired { get => isSupertaxRequired; set { isSupertaxRequired = value; RaisePropertyChanged("IsSupertaxRequired"); } }
        public double Supertaxvalue { get => supertaxvalue; set { supertaxvalue = value; RaisePropertyChanged("Supertaxvalue"); } }
    }
}
