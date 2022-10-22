using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DressManagement.WPF.Models.Auth
{
    public class RoleModel : BaseModel
    {
        public RoleModel()
        {
            Authories = new List<AuthoryModel>();
        }

        public string Name { get; set; }

        public List<AuthoryModel> Authories { get; set; }
    }
}
