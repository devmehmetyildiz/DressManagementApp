using DressManagement.API.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DressManagement.API.DataAccess.Repositories.Abstract.Auth
{
    public interface IRoletoAuthoryRepository : IRepository<RoletoAuthoryModel>
    {
        void AddAuthorytoRole(RoletoAuthoryModel model);

        void DeleteAuthoriesbyRole(string RoleGuid);

        List<string> GetAuthoriesByRole(string RoleId);
    }
}