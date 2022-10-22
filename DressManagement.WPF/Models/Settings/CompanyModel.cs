using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DressManagement.WPF.Models.Settings
{
    public class CompanyModel : BaseModel
    {
        private string name;
        private string city;
        private string town;
        private string address1;
        private string address2;
        private string companyPersonname;
        private string companyPersonrole;
        private string companyPersonPhone1;
        private string companyPersonPhone2;

        public string Name { get => name; set { name = value; RaisePropertyChanged("Name"); } }
        public string City { get => city; set { city = value; RaisePropertyChanged("City"); } }
        public string Town { get => town; set { town = value; RaisePropertyChanged("Town"); } }
        public string Address1 { get => address1; set { address1 = value; RaisePropertyChanged("Address1"); } }
        public string Address2 { get => address2; set { address2 = value; RaisePropertyChanged("Address2"); } }
        public string CompanyPersonname { get => companyPersonname; set { companyPersonname = value; RaisePropertyChanged("CompanyPersonname"); } }
        public string CompanyPersonrole { get => companyPersonrole; set { companyPersonrole = value; RaisePropertyChanged("CompanyPersonrole"); } }
        public string CompanyPersonPhone1 { get => companyPersonPhone1; set { companyPersonPhone1 = value; RaisePropertyChanged("CompanyPersonPhone1"); } }
        public string CompanyPersonPhone2 { get => companyPersonPhone2; set { companyPersonPhone2 = value; RaisePropertyChanged("CompanyPersonPhone2"); } }

    }
}
