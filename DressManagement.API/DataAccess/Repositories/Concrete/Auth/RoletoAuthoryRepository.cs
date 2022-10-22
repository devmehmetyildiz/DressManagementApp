using DressManagement.API.DataAccess.Repositories.Abstract.Auth;
using DressManagement.API.Models.Auth;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DressManagement.API.DataAccess.Repositories.Concrete.Auth
{
    public class RoletoAuthoryRepository : Repository<RoletoAuthoryModel>, IRoletoAuthoryRepository
    {
        public ApplicationDBContext dbcontext { get { return _context as ApplicationDBContext; } }
        private DbSet<RoletoAuthoryModel> _dbSet;
        public RoletoAuthoryRepository(ApplicationDBContext context) : base(context)
        {
            _dbSet = dbcontext.Set<RoletoAuthoryModel>();
        }

        public void AddAuthorytoRole(RoletoAuthoryModel model)
        {
            string query = $"INSERT INTO RoletoAuthories (`RoleID`, `AuthoryID`) VALUES ('{model.RoleID}','{model.AuthoryID}')";
            var result = dbcontext.Database.ExecuteSqlRaw(query);
        }



        public List<string> GetAuthoriesByRole(string RoleId)
        {
            return _dbSet.Where(u => u.RoleID == RoleId).Select(u => u.AuthoryID).ToList();
        }

        public void DeleteAuthoriesbyRole(string RoleGuid)
        {
            string query = $"DELETE FROM RoletoAuthories WHERE `RoleID` = '{RoleGuid}'";
            var result = dbcontext.Database.ExecuteSqlRaw(query);
        }
    }
}