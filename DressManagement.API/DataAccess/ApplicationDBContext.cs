using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DressManagement.API.Models.Auth;
using DressManagement.API.Models.Business;
using DressManagement.API.Models.Settings;

namespace DressManagement.API.DataAccess
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }

        #region Auth
        public DbSet<AuthoryModel> Authories { get; set; }
        public DbSet<RoleModel> Roles { get; set; }
        public DbSet<RoletoAuthoryModel> RoletoAuthories { get; set; }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<UsertoRoleModel> UsertoRoles { get; set; }
        public DbSet<UsertoSaltModel> UsertoSalt { get; set; }
        #endregion

        #region Business
        public DbSet<MovementModel> Movements { get; set; }
        public DbSet<ProductModel> Products { get; set; }
        public DbSet<PurchaseModel> Purchases { get; set; }
        public DbSet<SalesModel> Sales { get; set; }
        public DbSet<StockModel> Stocks { get; set; }
        #endregion

        #region Settings
        public DbSet<BodysizeModel> Bodysizes { get; set; }
        public DbSet<CaseModel> Cases { get; set; }
        public DbSet<CategoriesModel>  Categories { get; set; }
        public DbSet<CategoriestosubcategoriesModel> CategoriestoSubcategories { get; set; }
        public DbSet<CompanyModel> Companies { get; set; }
        public DbSet<CostumerModel> Costumers { get; set; }
        public DbSet<PaymenttypeModel> Paymenttypes { get; set; }
        public DbSet<SubcategoriesModel> Subcategories { get; set; }
        public DbSet<UnitModel> Units { get; set; }
        #endregion
    }
}
