using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DressManagement.API.Models.Settings
{
    public class PaymenttypeModel : BaseModel
    {
        public string Name { get; set; }
        public double Taxvalue { get; set; }
        public bool IsSupertaxRequired { get; set; }
        public double Supertaxvalue { get; set; }
    }
}
