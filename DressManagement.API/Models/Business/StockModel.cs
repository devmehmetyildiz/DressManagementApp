using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DressManagement.API.Models.Business
{
    public class StockModel : BaseModel
    {
        public StockModel()
        {
            Products = new List<ProductModel>();
        }
        public string StockgroupID { get; set; }
        public string CategoryID { get; set; }
        public string SubcategoryID { get; set; }
        public string CompanyID { get; set; }
        [NotMapped]
        public List<ProductModel> Products { get; set; }
        public double Totalamount { get; set; }
        public double Totalpurchaseamount { get; set; }
        public string CaseID { get; set; }
    }
}
