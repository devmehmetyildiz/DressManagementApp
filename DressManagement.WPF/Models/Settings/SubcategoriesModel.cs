using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DressManagement.WPF.Models.Settings
{
    public class SubcategoriesModel : BaseModel
    {
        private string name;
        private double salesvalue;

        public string Name { get => name; set { name = value; RaisePropertyChanged("Name"); } }
        public double Salesvalue { get => salesvalue; set { salesvalue = value; RaisePropertyChanged("Salesvalue"); } }
    }
}
