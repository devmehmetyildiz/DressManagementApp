using DressManagement.API.DataAccess.Repositories.Abstract.Auth;
using DressManagement.API.Models.Auth;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DressManagement.API.DataAccess.Repositories.Concrete.Auth
{
    public class UsertoSaltRepository : Repository<UsertoSaltModel>, IUsertoSaltRepository
    {
        public ApplicationDBContext dbcontext { get { return _context as ApplicationDBContext; } }
        private DbSet<UsertoSaltModel> _dbSet;
        public UsertoSaltRepository(ApplicationDBContext context) : base(context)
        {
            _dbSet = dbcontext.Set<UsertoSaltModel>();
        }

        public string GetSaltByGuid(string UserGuid)
        {
            return _dbSet.FirstOrDefault(u => u.UserID == UserGuid).Salt;
        }
    }
}