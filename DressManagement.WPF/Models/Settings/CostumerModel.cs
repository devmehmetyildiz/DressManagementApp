using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DressManagement.WPF.Models.Settings
{
    public class CostumerModel : BaseModel
    {
        private string name;
        private string phonenumber1;
        private string phonenumber2;
        private string city;
        private string town;
        private string address1;
        private string address2;

        public string Name { get => name; set { name = value; RaisePropertyChanged("Id"); } }
        public string Phonenumber1 { get => phonenumber1; set { phonenumber1 = value; RaisePropertyChanged("Phonenumber1"); } }
        public string Phonenumber2 { get => phonenumber2; set { phonenumber2 = value; RaisePropertyChanged("Phonenumber2"); } }
        public string City { get => city; set { city = value; RaisePropertyChanged("City"); } }
        public string Town { get => town; set { town = value; RaisePropertyChanged("Town"); } }
        public string Address1 { get => address1; set { address1 = value; RaisePropertyChanged("Address1"); } }
        public string Address2 { get => address2; set { address2 = value; RaisePropertyChanged("Address2"); } }
    }
}
