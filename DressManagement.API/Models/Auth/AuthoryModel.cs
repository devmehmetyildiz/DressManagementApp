using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DressManagement.API.Models.Auth
{
    public class AuthoryModel
    {
        [Key]
        public int Id { get; set; }
        [StringLength(36)]
        public string Name { get; set; }
        [StringLength(36)]
        public string Group { get; set; }
        [StringLength(36)]
        public string ConcurrencyStamp { get; set; }
        [NotMapped]
        public bool IsAdded { get; set; }
    }
}
