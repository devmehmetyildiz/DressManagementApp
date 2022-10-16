using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DressManagement.API.DataAccess.Repositories.Abstract.Business;
using DressManagement.API.Models.Business;
using Microsoft.EntityFrameworkCore;

namespace DressManagement.API.DataAccess.Repositories.Concrete.Business
{
    public class StockRepository : Repository<StockModel>, IStockRepository
    {
        public ApplicationDBContext dbcontext { get { return _context as ApplicationDBContext; } }
        private DbSet<StockModel> _dbSet;
        public StockRepository(ApplicationDBContext context) : base(context)
        {
            _dbSet = dbcontext.Set<StockModel>();
        }
    }
}