using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DressManagement.API.DataAccess.Repositories.Abstract.Business;
using DressManagement.API.Models.Business;
using Microsoft.EntityFrameworkCore;

namespace DressManagement.API.DataAccess.Repositories.Concrete.Business
{
    public class PurchaseRepository : Repository<PurchaseModel>, IPurchaseRepository
    {
        public ApplicationDBContext dbcontext { get { return _context as ApplicationDBContext; } }
        private DbSet<PurchaseModel> _dbSet;
        public PurchaseRepository(ApplicationDBContext context) : base(context)
        {
            _dbSet = dbcontext.Set<PurchaseModel>();
        }
    }
}