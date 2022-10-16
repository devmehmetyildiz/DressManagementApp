using DressManagement.API.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DressManagement.API.DataAccess.Repositories.Abstract.Auth
{
    public interface IUsertoSaltRepository : IRepository<UsertoSaltModel>
    {
        string GetSaltByGuid(string UserGuid);
    }
}