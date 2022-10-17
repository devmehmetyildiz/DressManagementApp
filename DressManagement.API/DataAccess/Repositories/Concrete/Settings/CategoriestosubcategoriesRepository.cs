using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DressManagement.API.DataAccess.Repositories.Abstract.Settings;
using DressManagement.API.Models.Settings;
using Microsoft.EntityFrameworkCore;

namespace DressManagement.API.DataAccess.Repositories.Concrete.Settings
{
    public class CategoriestosubcategoriesRepository : Repository<CategoriestosubcategoriesModel>, ICategoriestosubcategoriesRepository
    {
        public ApplicationDBContext dbcontext { get { return _context as ApplicationDBContext; } }
        private DbSet<CategoriestosubcategoriesModel> _dbSet;
        public CategoriestosubcategoriesRepository(ApplicationDBContext context) : base(context)
        {
            _dbSet = dbcontext.Set<CategoriestosubcategoriesModel>();
        }

        public void RemovebyGuids(List<CategoriestosubcategoriesModel> list)
        {
            foreach (var item in list)
            {
                _dbSet.Remove(item);
            }
        }
    }
}