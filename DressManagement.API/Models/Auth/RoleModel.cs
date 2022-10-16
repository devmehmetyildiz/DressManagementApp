using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DressManagement.API.Models.Auth
{
    public class RoleModel : BaseModel
    {
        public RoleModel()
        {
            Authories = new List<AuthoryModel>();
        }

        public string Name { get; set; }

        [NotMapped]
        public List<AuthoryModel> Authories { get; set; }
    }
}
