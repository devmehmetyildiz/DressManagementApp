using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DressManagement.API.DataAccess.Repositories.Abstract.Settings;
using DressManagement.API.Models.Settings;
using Microsoft.EntityFrameworkCore;

namespace DressManagement.API.DataAccess.Repositories.Concrete.Settings
{
    public class BodysizeRepository : Repository<BodysizeModel>, IBodysizeRepository
    {
        public ApplicationDBContext dbcontext { get { return _context as ApplicationDBContext; } }
        private DbSet<BodysizeModel> _dbSet;
        public BodysizeRepository(ApplicationDBContext context) : base(context)
        {
            _dbSet = dbcontext.Set<BodysizeModel>();
        }
    }