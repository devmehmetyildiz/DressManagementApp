using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DressManagement.WPF.Models.Auth
{
    public class AuthoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Group { get; set; }
        public string ConcurrencyStamp { get; set; }
        public bool IsAdded { get; set; }
    }
}
