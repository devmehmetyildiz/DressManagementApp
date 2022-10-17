using DressManagement.API.DataAccess.Repositories.Abstract.Auth;
using DressManagement.API.DataAccess.Repositories.Abstract.Business;
using DressManagement.API.DataAccess.Repositories.Abstract.Settings;
using DressManagement.API.DataAccess.Repositories.Concrete.Auth;
using DressManagement.API.DataAccess.Repositories.Concrete.Business;
using DressManagement.API.DataAccess.Repositories.Concrete.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DressManagement.API.DataAccess.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDBContext _dBContext;
        public UnitOfWork(ApplicationDBContext dBContext)
        {
            _dBContext = dBContext;
            //Application

            //Auth
            AuthoryRepository = new AuthoryRepository(_dBContext);
            UserRepository = new UserRepository(_dBContext);
            RolesRepository = new RolesRepository(_dBContext);
            UsertoRoleRepository = new UsertoRoleRepository(_dBContext);
            RoletoAuthoryRepository = new RoletoAuthoryRepository(_dBContext);
            UsertoSaltRepository = new UsertoSaltRepository(_dBContext);
           
            MovementRepository = new MovementRepository(_dBContext);
            ProductRepository = new ProductRepository(_dBContext);
            PurchaseRepository = new PurchaseRepository(_dBContext);
            SalesRepository = new SalesRepository(_dBContext);
            StockRepository = new StockRepository(_dBContext);
           
            BodysizeRepository = new BodysizeRepository(_dBContext);
            CaseRepository = new CaseRepository(_dBContext);
            CategoriesRepository = new CategoriesRepository(_dBContext);
            CategoriestosubcategoriesRepository = new CategoriestosubcategoriesRepository(_dBContext);
            CompanyRepository = new CompanyRepository(_dBContext);
            CostumerRepository = new CostumerRepository(_dBContext);
            PaymenttypeRepository = new PaymenttypeRepository(_dBContext);
            SubcategoriesRepository = new SubcategoriesRepository(_dBContext);
            UnitRepository = new UnitRepository(_dBContext);
            //Settings
           
        }

        public IAuthoryRepository AuthoryRepository { get; private set; }

        public IUserRepository UserRepository { get; private set; }

        public IRolesRepository RolesRepository { get; private set; }

        public IUsertoRoleRepository UsertoRoleRepository { get; private set; }

        public IRoletoAuthoryRepository RoletoAuthoryRepository { get; private set; }

        public IUsertoSaltRepository UsertoSaltRepository { get; private set; }

        public IMovementRepository MovementRepository { get; private set; }

        public IProductRepository ProductRepository { get; private set; }

        public IPurchaseRepository PurchaseRepository { get; private set; }

        public ISalesRepository SalesRepository { get; private set; }

        public IStockRepository StockRepository { get; private set; }

        public IBodysizeRepository BodysizeRepository { get; private set; }

        public ICaseRepository CaseRepository { get; private set; }

        public ICategoriesRepository CategoriesRepository { get; private set; }

        public ICategoriestosubcategoriesRepository CategoriestosubcategoriesRepository { get; private set; }

        public ICompanyRepository CompanyRepository { get; private set; }

        public ICostumerRepository CostumerRepository { get; private set; }

        public IPaymenttypeRepository PaymenttypeRepository { get; private set; }

        public ISubcategoriesRepository SubcategoriesRepository { get; private set; }

        public IUnitRepository UnitRepository { get; private set; }

        public int Complate()
        {
            return _dBContext.SaveChanges();
        }

        public void Dispose()
        {
            _dBContext.Dispose();
        }
    }
}
