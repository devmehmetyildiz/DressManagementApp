using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DressManagement.WPF.Models.Common
{
    public class LogModel : PropertyChangeBaseModel
    {
        private int id;
        private string jobname;
        private string jobvalue;
        private string message;

        public int Id { get => id; set { id = value; RaisePropertyChanged("Id"); } }
        public string Jobname { get => jobname; set { jobname = value; RaisePropertyChanged("Jobname"); } }
        public string Jobvalue { get => jobvalue; set { jobvalue = value; RaisePropertyChanged("Jobvalue"); } }
        public string Message { get => message; set { message = value; RaisePropertyChanged("Message"); } }
    }
}
