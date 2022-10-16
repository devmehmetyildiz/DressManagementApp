using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DressManagement.API.Models.Auth
{
    public class UserModel : BaseModel
    {
        public UserModel()
        {
            Roles = new List<RoleModel>();
        }

        [StringLength(85)]
        public string Username { get; set; }
        [StringLength(85)]
        public string NormalizedUsername { get; set; }
        [StringLength(85)]
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        [StringLength(85)]
        public string PasswordHash { get; set; }
        public int AccessFailedCount { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        [StringLength(85)]
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        [StringLength(85)]
        public string Town { get; set; }
        [StringLength(85)]
        public string City { get; set; }
        public string Address { get; set; }
        [StringLength(85)]
        public string Language { get; set; }
        public int UserID { get; set; }
        [NotMapped]
        public List<RoleModel> Roles { get; set; }
    }
}
