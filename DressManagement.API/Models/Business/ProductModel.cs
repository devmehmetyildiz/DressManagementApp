using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DressManagement.API.Models.Business
{
    public class ProductModel
    {
        public string StockID { get; set; }
        public string Name { get; set; }
        public string BodysizeID { get; set; }
        public string ImageUri { get; set; }
        public string BarcodeID { get; set; }
        public double Purchaseprice { get; set; }
        public string UnitID { get; set; }

    }
}
