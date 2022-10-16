using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DressManagement.API.Models.Auth
{
    public class RoletoAuthoryModel
    {
        [Key]
        public int Id { get; set; }
        [StringLength(85)]
        public string RoleID { get; set; }
        [StringLength(85)]
        public string AuthoryID { get; set; }
    }
}
