using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DressManagement.API.Models
{
    public static class Constants
    {
        public enum Readtype
        {
            Manueltype = 0,
            Barcodetype = 1
        }

        public enum Salestype
        {
            Postpaid = 0 ,
            Unpaid = 1
        }
    }
}
