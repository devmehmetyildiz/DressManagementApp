using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DressManagement.WPF.Models.Settings
{
    public class CategoriesModel : BaseModel
    {
        private string name;
        private List<SubcategoriesModel> subcategories;

        public CategoriesModel()
        {
            Subcategories = new List<SubcategoriesModel>();
        }
        public string Name { get => name; set { name = value; RaisePropertyChanged("Name"); } }
        public List<SubcategoriesModel> Subcategories { get => subcategories; set { subcategories = value; RaisePropertyChanged("Subcategories"); } }
    }
}
