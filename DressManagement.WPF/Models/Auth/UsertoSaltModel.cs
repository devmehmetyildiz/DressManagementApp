using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DressManagement.WPF.Models.Auth
{
    public class UsertoSaltModel
    {
        public int Id { get; set; }
        public string UserID { get; set; }
        public string Salt { get; set; }
    }
}
