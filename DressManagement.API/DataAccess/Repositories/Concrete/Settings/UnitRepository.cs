using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DressManagement.API.DataAccess.Repositories.Abstract.Settings;
using DressManagement.API.Models.Settings;
using Microsoft.EntityFrameworkCore;

namespace DressManagement.API.DataAccess.Repositories.Concrete.Settings
{
    public class UnitRepository : Repository<UnitModel>, IUnitRepository
    {
        public ApplicationDBContext dbcontext { get { return _context as ApplicationDBContext; } }
        private DbSet<UnitModel> _dbSet;
        public UnitRepository(ApplicationDBContext context) : base(context)
        {
            _dbSet = dbcontext.Set<UnitModel>();
        }
    }
}