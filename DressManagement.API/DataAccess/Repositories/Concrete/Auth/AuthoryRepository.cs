using DressManagement.API.DataAccess.Repositories.Abstract.Auth;
using DressManagement.API.Models.Auth;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DressManagement.API.DataAccess.Repositories.Concrete.Auth
{
    public class AuthoryRepository : Repository<AuthoryModel>, IAuthoryRepository
    {
        public ApplicationDBContext dbcontext { get { return _context as ApplicationDBContext; } }
        private DbSet<AuthoryModel> _dbSet;
        public AuthoryRepository(ApplicationDBContext context) : base(context)
        {
            _dbSet = dbcontext.Set<AuthoryModel>();
        }

        public AuthoryModel FindAuthoryByName(string yetkiName)
        {
            return _dbSet.FirstOrDefault(u => u.Name == yetkiName);
        }

        public AuthoryModel FindAuthoryBuGuid(string Guid)
        {
            return _dbSet.FirstOrDefault(u => u.ConcurrencyStamp == Guid);
        }

        public List<AuthoryModel> GetAuthoriesbyGuids(List<string> authoryguids)
        {
            if (authoryguids.Count == 0)
            {
                return new List<AuthoryModel>();
            }
            string query = "";
            query += "select * from Authories  where ConcurrencyStamp IN (";
            for (int i = 0; i < authoryguids.Count; i++)
            {
                query += $"'{authoryguids[i]}'";
                if (i != authoryguids.Count - 1)
                    query += ",";
            }
            query += ")";
            return _dbSet.FromSqlRaw(query).ToList(); ;
        }

        public bool CheckAuthByUsername(string username, string authoryname)
        {
            string query = "";
            query += "select Authories.* from Authories ";
            query += "left join RoletoAuthories on Authories.ConcurrencyStamp = RoletoAuthories.AuthoryID ";
            query += "left join Roles on Roles.ConcurrencyStamp = RoletoAuthories.RoleID ";
            query += "left join UsertoRoles on UsertoRoles.RoleID  = Roles.ConcurrencyStamp ";
            query += "left join Users on users.ConcurrencyStamp = UsertoRoles.UserID ";
            query += $"where Users.Username = '{username}' and Authories.Name = '{authoryname}'";

            var Response = dbcontext.Authories.FromSqlRaw(query).ToList();
            if (Response.Count == 0)
                return false;
            else
                return true;
        }
    }
}