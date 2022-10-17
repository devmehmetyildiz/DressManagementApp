using DressManagement.API.Models.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DressManagement.API.DataAccess.Repositories.Abstract.Settings
{
    public interface ISubcategoriesRepository : IRepository<SubcategoriesModel>
    {
        List<SubcategoriesModel> GetByGuids(List<string> guids);

    }
}