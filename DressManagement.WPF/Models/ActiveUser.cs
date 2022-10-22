using DressManagement.WPF.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DressManagement.WPF.Models
{
    public static class ActiveUser
    {
        public static string Username { get; set; }
        public static string Password { get; set; }
        public static List<string> Authories { get; set; }
        public static List<LogModel> Logs { get; set; }
    }
}
