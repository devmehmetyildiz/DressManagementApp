using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DressManagement.API.Models.Settings
{
    public class CategoriestosubcategoriesModel
    {
        [Key]
        public int Id { get; set; }
        [StringLength(36)]
        public string CategoryID { get; set; }
        [StringLength(36)]
        public string SubcategoryID { get; set; }
    }
}
