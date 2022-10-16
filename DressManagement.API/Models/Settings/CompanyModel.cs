using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DressManagement.API.Models.Settings
{
    public class CompanyModel : BaseModel
    {
        public string Name { get; set; }
        public string City { get; set; }
        public string Town { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string CompanyPersonname { get; set; }
        public string CompanyPersonrole { get; set; }
        public string CompanyPersonPhone1 { get; set; }
        public string CompanyPersonPhone2 { get; set; }

    }
}
