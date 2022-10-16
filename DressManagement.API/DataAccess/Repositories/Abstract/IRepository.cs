using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DressManagement.API.DataAccess.Repositories.Abstract
{
    public interface IRepository<Tentity> where Tentity : class
    {
        Tentity Getbyid(int id);

        List<Tentity> GetAll();

        List<Tentity> GetTop(int count);

        void Update(Tentity olditem, Tentity newitem);

        void Add(Tentity entity);

        void AddRange(List<Tentity> entities);

        void Remove(int id);

        void RemoveRange(List<Tentity> entities);

        T GetSingleRecord<T>(Expression<Func<T, bool>> predicate) where T : class;
        List<T> GetRecords<T>(Expression<Func<T, bool>> predicate) where T : class;

    }
}