using DressManagement.API.Models.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DressManagement.API.DataAccess.Repositories.Abstract.Settings
{
    public interface ICategoriestosubcategoriesRepository : IRepository<CategoriestosubcategoriesModel>
    {
        void RemovebyGuids(List<CategoriestosubcategoriesModel> list);
    }
}