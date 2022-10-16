using DressManagement.API.DataAccess.Repositories.Abstract.Auth;
using DressManagement.API.Models.Auth;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DressManagement.API.DataAccess.Repositories.Concrete.Auth
{
    public class UserRepository : Repository<UserModel>, IUserRepository
    {
        public ApplicationDBContext dbcontext { get { return _context as ApplicationDBContext; } }
        private DbSet<UserModel> _dbSet;
        public UserRepository(ApplicationDBContext context) : base(context)
        {
            _dbSet = dbcontext.Set<UserModel>();
        }

        public UserModel FindUserByName(string name)
        {
            return _dbSet.FirstOrDefault(u => u.NormalizedUsername == name.ToUpper());
        }

        public UserModel GetUsertByGuid(string guid)
        {
            return _dbSet.FirstOrDefault(u => u.ConcurrencyStamp == guid);
        }
    }
}