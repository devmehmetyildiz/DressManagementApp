using DressManagement.API.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DressManagement.API.DataAccess.Repositories.Abstract.Auth
{
    public interface IUsertoRoleRepository : IRepository<UsertoRoleModel>
    {
        void AddRolestoUser(UsertoRoleModel model);
        void RemoveRolefromUser(string UserID);
        List<string> GetRolesbyUser(string UserID);
    }
}