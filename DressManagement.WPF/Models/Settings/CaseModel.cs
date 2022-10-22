using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DressManagement.WPF.Models.Settings
{
    public class CaseModel : BaseModel
    {
        private string name;
        private bool isFinishcase;

        public string Name { get => name; set { name = value;RaisePropertyChanged("Name"); } }
        public bool IsFinishcase { get => isFinishcase; set { isFinishcase = value; RaisePropertyChanged("IsFinishcase"); } }
    }
}
