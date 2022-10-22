using DressManagement.API.DataAccess.Repositories.Abstract.Auth;
using DressManagement.API.Models.Auth;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DressManagement.API.DataAccess.Repositories.Concrete.Auth
{
    public class UsertoRoleRepository : Repository<UsertoRoleModel>, IUsertoRoleRepository
    {
        public ApplicationDBContext dbcontext { get { return _context as ApplicationDBContext; } }
        private DbSet<UsertoRoleModel> _dbSet;
        public UsertoRoleRepository(ApplicationDBContext context) : base(context)
        {
            _dbSet = dbcontext.Set<UsertoRoleModel>();
        }

        public List<string> GetRolesbyUser(string UserID)
        {
            return _dbSet.Where(u => u.UserID == UserID).Select(u => u.RoleID).ToList();
        }

        public void AddRolestoUser(UsertoRoleModel model)
        {
            string query = $"INSERT INTO UsertoRoles (UserID, RoleID) VALUES ('{model.UserID}','{model.RoleID}')";
            var result = dbcontext.Database.ExecuteSqlRaw(query);
        }

        public void RemoveRolefromUser(string UserID)
        {
            string query = $"DELETE FROM UsertoRoles WHERE UserID = '{UserID}'";
            var result = dbcontext.Database.ExecuteSqlRaw(query);
        }
    }
}