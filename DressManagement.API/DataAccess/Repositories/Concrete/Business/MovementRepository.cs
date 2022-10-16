using DressManagement.API.DataAccess.Repositories.Abstract.Business;
using DressManagement.API.Models.Business;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DressManagement.API.DataAccess.Repositories.Concrete.Business
{
    public class MovementRepository : Repository<MovementModel>, IMovementRepository
    {
        public ApplicationDBContext dbcontext { get { return _context as ApplicationDBContext; } }
        private DbSet<MovementModel> _dbSet;
        public MovementRepository(ApplicationDBContext context) : base(context)
        {
            _dbSet = dbcontext.Set<MovementModel>();
        }
    }
}