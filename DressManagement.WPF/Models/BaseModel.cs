using DressManagement.WPF.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace DressManagement.WPF.Models
{
    public class BaseModel : PropertyChangeBaseModel
    {
        private int id;
        private string concurrencyStamp;
        private string createdUser;
        private string updatedUser;
        private string deleteUser;
        private DateTime? createTime;
        private DateTime? updateTime;
        private DateTime deleteTime;
        private bool isActive;

        public int Id { get => id; set { id = value; RaisePropertyChanged("Id"); } }
        public string ConcurrencyStamp { get => concurrencyStamp; set { concurrencyStamp = value; RaisePropertyChanged("ConcurrencyStamp"); } }
        public string CreatedUser { get => createdUser; set { createdUser = value; RaisePropertyChanged("CreatedUser"); } }
        public string UpdatedUser { get => updatedUser; set { updatedUser = value; RaisePropertyChanged("UpdatedUser"); } }
        public string DeleteUser { get => deleteUser; set { deleteUser = value; RaisePropertyChanged("DeleteUser"); } }
        public DateTime? CreateTime { get => createTime; set { createTime = value; RaisePropertyChanged("CreateTime"); } }
        public DateTime? UpdateTime { get => updateTime; set { updateTime = value; RaisePropertyChanged("UpdateTime"); } }
        public DateTime DeleteTime { get => deleteTime; set { deleteTime = value; RaisePropertyChanged("DeleteTime"); } }
        public bool IsActive { get => isActive; set { isActive = value; RaisePropertyChanged("IsActive"); } }

    }
}
