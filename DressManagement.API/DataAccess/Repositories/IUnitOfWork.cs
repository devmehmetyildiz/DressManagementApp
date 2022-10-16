using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DressManagement.API.DataAccess.Repositories.Abstract.Auth;
using DressManagement.API.DataAccess.Repositories.Abstract.Business;
using DressManagement.API.DataAccess.Repositories.Abstract.Settings;


namespace DressManagement.API.DataAccess.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        //Auth
        IAuthoryRepository AuthoryRepository { get; }
        IUserRepository UserRepository { get; }
        IRolesRepository RolesRepository { get; }
        IUsertoRoleRepository UsertoRoleRepository { get; }
        IRoletoAuthoryRepository RoletoAuthoryRepository { get; }
        IUsertoSaltRepository UsertoSaltRepository { get; }
        
        //Business
        IMovementRepository MovementRepository { get; }
        IProductRepository ProductRepository { get; }
        IPurchaseRepository PurchaseRepository { get; }
        ISalesRepository SalesRepository { get; }
        IStockRepository StockRepository { get; }

        //Settings
        IBodysizeRepository BodysizeRepository { get; }
        ICaseRepository CaseRepository { get; }
        ICategoriesRepository CategoriesRepository { get; }
        ICategoriestosubcategoriesRepository CategoriestosubcategoriesRepository { get; }
        ICompanyRepository CompanyRepository { get; }
        ICostumerRepository CostumerRepository { get; }
        IPaymenttypeRepository PaymenttypeRepository { get; }
        ISubcategoriesRepository SubcategoriesRepository { get; }
        IUnitRepository UnitRepository { get; }
        int Complate();
    }
}