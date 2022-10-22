using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DressManagement.WPF.Models.Auth
{
    public class UserModel : BaseModel
    {
        public UserModel()
        {
            Roles = new List<RoleModel>();
        }

        public string Username { get; set; }
        public string NormalizedUsername { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public int AccessFailedCount { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public string Town { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Language { get; set; }
        public int UserID { get; set; }
        public List<RoleModel> Roles { get; set; }
    }
}
