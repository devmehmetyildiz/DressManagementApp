using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DressManagement.API.DataAccess.Repositories.Abstract.Settings;
using DressManagement.API.Models.Settings;
using Microsoft.EntityFrameworkCore;

namespace DressManagement.API.DataAccess.Repositories.Concrete.Settings
{
    public class SubcategoriesRepository : Repository<SubcategoriesModel>, ISubcategoriesRepository
    {
        public ApplicationDBContext dbcontext { get { return _context as ApplicationDBContext; } }
        private DbSet<SubcategoriesModel> _dbSet;
        public SubcategoriesRepository(ApplicationDBContext context) : base(context)
        {
            _dbSet = dbcontext.Set<SubcategoriesModel>();
        }

        public List<SubcategoriesModel> GetByGuids(List<string> guids)
        {
            if (guids.Count == 0)
            {
                return new List<SubcategoriesModel>();
            }
            string query = "";
            query += "select * from subcategories  where ConcurrencyStamp IN (";
            for (int i = 0; i < guids.Count; i++)
            {
                query += $"'{guids[i]}'";
                if (i != guids.Count - 1)
                    query += ",";
            }
            query += ")";
            return _dbSet.FromSqlRaw(query).ToList();
        }
    }
}