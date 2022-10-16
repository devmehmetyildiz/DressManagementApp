using DressManagement.API.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DressManagement.API.DataAccess.Repositories.Abstract.Auth
{
    public interface IAuthoryRepository : IRepository<AuthoryModel>
    {
        AuthoryModel FindAuthoryByName(string yetkiName);

        AuthoryModel FindAuthoryBuGuid(string Guid);

        List<AuthoryModel> GetAuthoriesbyGuids(List<string> authoryguids);
        bool CheckAuthByUsername(string username, string authoryname);
    }
}