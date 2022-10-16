using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DressManagement.API.Models.Settings
{
    public class CategoriesModel : BaseModel
    {
        public CategoriesModel()
        {
            Subcategories = new List<SubcategoriesModel>();
        }
        public string Name { get; set; }
        public List<SubcategoriesModel> Subcategories { get; set; }
    }
}
