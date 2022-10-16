using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DressManagement.API.Models.Settings
{
    public class CaseModel : BaseModel
    {
        public string Name { get; set; }
        public bool IsFinishcase { get; set; }
    }
}
