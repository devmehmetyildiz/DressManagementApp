using DressManagement.API.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DressManagement.API.DataAccess.Repositories.Abstract.Auth
{
    public interface IUserRepository : IRepository<UserModel>
    {
        UserModel FindUserByName(string name);

        UserModel GetUsertByGuid(string guid);

        //UsersModel FindUserByPassword(string password);
    }
}