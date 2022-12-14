using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DressManagement.API.DataAccess.Repositories.Abstract.Business;
using DressManagement.API.Models.Business;
using Microsoft.EntityFrameworkCore;

namespace DressManagement.API.DataAccess.Repositories.Concrete.Business
{
    public class SalesRepository : Repository<SalesModel>, ISalesRepository
    {
        public ApplicationDBContext dbcontext { get { return _context as ApplicationDBContext; } }
        private DbSet<SalesModel> _dbSet;
        public SalesRepository(ApplicationDBContext context) : base(context)
        {
            _dbSet = dbcontext.Set<SalesModel>();
        }
    }
}