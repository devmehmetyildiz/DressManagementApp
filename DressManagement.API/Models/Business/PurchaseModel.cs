using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DressManagement.API.Models.Business
{
    public class PurchaseModel : BaseModel
    {
        public string StockID{ get; set; }

        public DateTime? ArrivalDate { get; set; }

        [NotMapped]
        public StockModel Stock { get; set; }
    }
}
