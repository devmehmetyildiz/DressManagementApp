using DressManagement.API.DataAccess;
using DressManagement.API.DataAccess.Repositories;
using DressManagement.API.Models.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DressManagement.API.Controllers
{
    [Route("api")]
    [ApiController]
    public class SettingController : ControllerBase
    {
        private IConfiguration _configuration;
        private readonly ILogger<SettingController> _logger;
        private readonly ApplicationDBContext _context;
        UnitOfWork unitOfWork;

        public SettingController(IConfiguration configuration, ILogger<SettingController> logger, ApplicationDBContext context)
        {
            _configuration = configuration;
            _logger = logger;
            _context = context;
            unitOfWork = new UnitOfWork(context);
        }

        #region Bodysize

        [HttpGet]
        [Route("Bodysize/GetAll")]
        public IActionResult GetAllBodySizes()
        {
            var data = unitOfWork.BodysizeRepository.GetRecords<BodysizeModel>(u => u.IsActive).ToList();
            if (data.Count == 0)
                return NotFound();
            return Ok(data);
        }

        [HttpPost]
        [Route("Bodysize/Add")]
        public IActionResult AddBodysize(BodysizeModel model)
        {
            var username = GetUsername();
            model.CreatedUser = username;
            model.IsActive = true;
            model.CreateTime = DateTime.Now;
            model.ConcurrencyStamp = Guid.NewGuid().ToString();
            unitOfWork.BodysizeRepository.Add(model);
            unitOfWork.Complate();
            return Ok();
        }

        [HttpPut]
        [Route("Bodysize/Update")]
        public IActionResult UpdateBoydsize(BodysizeModel model)
        {
            model.UpdatedUser = GetUsername();
            model.UpdateTime = DateTime.Now;
            unitOfWork.BodysizeRepository.Update(unitOfWork.BodysizeRepository.GetSingleRecord<BodysizeModel>(u => u.ConcurrencyStamp == model.ConcurrencyStamp), model);
            unitOfWork.Complate();
            return Ok();
        }

        [HttpDelete]
        [Route("Bodysize/Delete")]
        public IActionResult DeleteBodysize(BodysizeModel model)
        {
            model.DeleteUser = GetUsername();
            model.DeleteTime = DateTime.Now;
            model.IsActive = false;
            unitOfWork.BodysizeRepository.Update(unitOfWork.BodysizeRepository.GetSingleRecord<BodysizeModel>(u => u.ConcurrencyStamp == model.ConcurrencyStamp), model);
            unitOfWork.Complate();
            return Ok();
        }

        #endregion

        #region Case

        [HttpGet]
        [Route("Case/GetAll")]
        public IActionResult GetCases()
        {
            var data = unitOfWork.CaseRepository.GetRecords<CaseModel>(u => u.IsActive).ToList();
            if (data.Count == 0)
                return NotFound();
            return Ok(data);
        }

        [HttpPost]
        [Route("Case/Add")]
        public IActionResult AddCase(CaseModel model)
        {
            var username = GetUsername();
            model.CreatedUser = username;
            model.IsActive = true;
            model.CreateTime = DateTime.Now;
            model.ConcurrencyStamp = Guid.NewGuid().ToString();
            unitOfWork.CaseRepository.Add(model);
            unitOfWork.Complate();
            return Ok();
        }

        [HttpPut]
        [Route("Case/Update")]
        public IActionResult UpdateCase(CaseModel model)
        {
            model.UpdatedUser = GetUsername();
            model.UpdateTime = DateTime.Now;
            unitOfWork.CaseRepository.Update(unitOfWork.CaseRepository.GetSingleRecord<CaseModel>(u => u.ConcurrencyStamp == model.ConcurrencyStamp), model);
            unitOfWork.Complate();
            return Ok();
        }

        [HttpDelete]
        [Route("Case/Delete")]
        public IActionResult DeleteCase(CaseModel model)
        {
            model.DeleteUser = GetUsername();
            model.DeleteTime = DateTime.Now;
            model.IsActive = false;
            unitOfWork.CaseRepository.Update(unitOfWork.CaseRepository.GetSingleRecord<CaseModel>(u => u.ConcurrencyStamp == model.ConcurrencyStamp), model);
            unitOfWork.Complate();
            return Ok();
        }

        #endregion

        #region Categories

        [HttpGet]
        [Route("Categories/GetAll")]
        public IActionResult GetCategories()
        {
            var datas = unitOfWork.CaseRepository.GetRecords<CategoriesModel>(u => u.IsActive).ToList();
            foreach (var data in datas)
            {
                List<string> guids = unitOfWork.CategoriestosubcategoriesRepository.GetRecords<CategoriestosubcategoriesModel>(u => u.CategoryID == data.ConcurrencyStamp).Select(u => u.SubcategoryID).ToList();
                data.Subcategories = unitOfWork.SubcategoriesRepository.GetByGuids(guids);
            }
            if (datas.Count == 0)
                return NotFound();
            return Ok(datas);
        }

        [HttpPost]
        [Route("Categories/Add")]
        public IActionResult AddCategories(CategoriesModel model)
        {
            var username = GetUsername();
            model.CreatedUser = username;
            model.IsActive = true;
            model.CreateTime = DateTime.Now;
            model.ConcurrencyStamp = Guid.NewGuid().ToString();
            unitOfWork.SubcategoriesRepository.AddRange(model.Subcategories);
            unitOfWork.CategoriesRepository.Add(model);
            unitOfWork.Complate();
            return Ok();
        }

        [HttpPut]
        [Route("Categories/Update")]
        public IActionResult UpdateCategories(CategoriesModel model)
        {
            var oldmodel = unitOfWork.CategoriesRepository.GetSingleRecord<CategoriesModel>(u => u.ConcurrencyStamp == model.ConcurrencyStamp);
            model.UpdatedUser = GetUsername();
            model.UpdateTime = DateTime.Now;
            List<CategoriestosubcategoriesModel> list = new List<CategoriestosubcategoriesModel>();
            foreach (var data in oldmodel.Subcategories)
            {
                list.Add(new CategoriestosubcategoriesModel
                {
                    CategoryID = model.ConcurrencyStamp,
                    SubcategoryID = data.ConcurrencyStamp
                });
            }
            unitOfWork.CategoriestosubcategoriesRepository.RemovebyGuids(list);
            unitOfWork.SubcategoriesRepository.AddRange(model.Subcategories);
            unitOfWork.CategoriesRepository.Update(oldmodel, model);
            unitOfWork.Complate();
            return Ok();
        }

        [HttpDelete]
        [Route("Categories/Delete")]
        public IActionResult Deletecategories(CategoriesModel model)
        {
            model.DeleteUser = GetUsername();
            model.DeleteTime = DateTime.Now;
            model.IsActive = false;
            List<CategoriestosubcategoriesModel> list = new List<CategoriestosubcategoriesModel>();
            foreach (var data in model.Subcategories)
            {
                list.Add(new CategoriestosubcategoriesModel
                {
                    CategoryID = model.ConcurrencyStamp,
                    SubcategoryID = data.ConcurrencyStamp
                });
            }
            unitOfWork.CategoriesRepository.Update(unitOfWork.CategoriesRepository.GetSingleRecord<CategoriesModel>(u => u.ConcurrencyStamp == model.ConcurrencyStamp), model);
            unitOfWork.Complate();
            return Ok();
        }

        #endregion

        #region Company

        [HttpGet]
        [Route("Company/GetAll")]
        public IActionResult GetCompanies()
        {
            var data = unitOfWork.CompanyRepository.GetRecords<CompanyModel>(u => u.IsActive).ToList();
            if (data.Count == 0)
                return NotFound();
            return Ok(data);
        }

        [HttpPost]
        [Route("Company/Add")]
        public IActionResult AddCompany(CompanyModel model)
        {
            var username = GetUsername();
            model.CreatedUser = username;
            model.IsActive = true;
            model.CreateTime = DateTime.Now;
            model.ConcurrencyStamp = Guid.NewGuid().ToString();
            unitOfWork.CompanyRepository.Add(model);
            unitOfWork.Complate();
            return Ok();
        }

        [HttpPut]
        [Route("Company/Update")]
        public IActionResult UpdateCompany(CompanyModel model)
        {
            model.UpdatedUser = GetUsername();
            model.UpdateTime = DateTime.Now;
            unitOfWork.CompanyRepository.Update(unitOfWork.CompanyRepository.GetSingleRecord<CompanyModel>(u => u.ConcurrencyStamp == model.ConcurrencyStamp), model);
            unitOfWork.Complate();
            return Ok();
        }

        [HttpDelete]
        [Route("Company/Delete")]
        public IActionResult DeleteCompany(CompanyModel model)
        {
            model.DeleteUser = GetUsername();
            model.DeleteTime = DateTime.Now;
            model.IsActive = false;
            unitOfWork.CompanyRepository.Update(unitOfWork.CompanyRepository.GetSingleRecord<CompanyModel>(u => u.ConcurrencyStamp == model.ConcurrencyStamp), model);
            unitOfWork.Complate();
            return Ok();
        }
        #endregion

        #region Costumer

        [HttpGet]
        [Route("Costumer/GetAll")]
        public IActionResult GetCostumers()
        {
            var data = unitOfWork.CostumerRepository.GetRecords<CostumerModel>(u => u.IsActive).ToList();
            if (data.Count == 0)
                return NotFound();
            return Ok(data);
        }

        [HttpPost]
        [Route("Costumer/Add")]
        public IActionResult AddCostumer(CostumerModel model)
        {
            var username = GetUsername();
            model.CreatedUser = username;
            model.IsActive = true;
            model.CreateTime = DateTime.Now;
            model.ConcurrencyStamp = Guid.NewGuid().ToString();
            unitOfWork.CostumerRepository.Add(model);
            unitOfWork.Complate();
            return Ok();
        }

        [HttpPut]
        [Route("Costumer/Update")]
        public IActionResult UpdateCostumer(CostumerModel model)
        {
            model.UpdatedUser = GetUsername();
            model.UpdateTime = DateTime.Now;
            unitOfWork.CostumerRepository.Update(unitOfWork.CostumerRepository.GetSingleRecord<CostumerModel>(u => u.ConcurrencyStamp == model.ConcurrencyStamp), model);
            unitOfWork.Complate();
            return Ok();
        }

        [HttpDelete]
        [Route("Costumer/Delete")]
        public IActionResult DeleteCostumer(CostumerModel model)
        {
            model.DeleteUser = GetUsername();
            model.DeleteTime = DateTime.Now;
            model.IsActive = false;
            unitOfWork.CostumerRepository.Update(unitOfWork.CostumerRepository.GetSingleRecord<CostumerModel>(u => u.ConcurrencyStamp == model.ConcurrencyStamp), model);
            unitOfWork.Complate();
            return Ok();
        }
        #endregion

        #region Paymenttpe

        [HttpGet]
        [Route("Paymenttpe/GetAll")]
        public IActionResult GetPaymenttypes()
        {
            var data = unitOfWork.PaymenttypeRepository.GetRecords<PaymenttypeModel>(u => u.IsActive).ToList();
            if (data.Count == 0)
                return NotFound();
            return Ok(data);
        }

        [HttpPost]
        [Route("Paymenttpe/Add")]
        public IActionResult AddPaymenttpe(PaymenttypeModel model)
        {
            var username = GetUsername();
            model.CreatedUser = username;
            model.IsActive = true;
            model.CreateTime = DateTime.Now;
            model.ConcurrencyStamp = Guid.NewGuid().ToString();
            unitOfWork.PaymenttypeRepository.Add(model);
            unitOfWork.Complate();
            return Ok();
        }

        [HttpPut]
        [Route("Paymenttpe/Update")]
        public IActionResult Updatepaymenttype(PaymenttypeModel model)
        {
            model.UpdatedUser = GetUsername();
            model.UpdateTime = DateTime.Now;
            unitOfWork.PaymenttypeRepository.Update(unitOfWork.PaymenttypeRepository.GetSingleRecord<PaymenttypeModel>(u => u.ConcurrencyStamp == model.ConcurrencyStamp), model);
            unitOfWork.Complate();
            return Ok();
        }

        [HttpDelete]
        [Route("Paymenttpe/Delete")]
        public IActionResult DeletePaymenttpe(PaymenttypeModel model)
        {
            model.DeleteUser = GetUsername();
            model.DeleteTime = DateTime.Now;
            model.IsActive = false;
            unitOfWork.PaymenttypeRepository.Update(unitOfWork.PaymenttypeRepository.GetSingleRecord<PaymenttypeModel>(u => u.ConcurrencyStamp == model.ConcurrencyStamp), model);
            unitOfWork.Complate();
            return Ok();
        }
        #endregion

        #region Subcategories

        [HttpGet]
        [Route("Subcategories/GetAll")]
        public IActionResult GetSubcategories()
        {
            var data = unitOfWork.SubcategoriesRepository.GetRecords<SubcategoriesModel>(u => u.IsActive).ToList();
            if (data.Count == 0)
                return NotFound();
            return Ok(data);
        }

        [HttpPost]
        [Route("Subcategories/Add")]
        public IActionResult AddSubcategories(SubcategoriesModel model)
        { 
            var username = GetUsername();
            model.CreatedUser = username;
            model.IsActive = true;
            model.CreateTime = DateTime.Now;
            model.ConcurrencyStamp = Guid.NewGuid().ToString();
            unitOfWork.SubcategoriesRepository.Add(model);
            unitOfWork.Complate();
            return Ok();
        }

        [HttpPut]
        [Route("Subcategories/Update")]
        public IActionResult UpdateSubcategories(SubcategoriesModel model)
        {
            model.UpdatedUser = GetUsername();
            model.UpdateTime = DateTime.Now;
            unitOfWork.SubcategoriesRepository.Update(unitOfWork.SubcategoriesRepository.GetSingleRecord<SubcategoriesModel>(u => u.ConcurrencyStamp == model.ConcurrencyStamp), model);
            unitOfWork.Complate();
            return Ok();
        }

        [HttpDelete]
        [Route("Subcategories/Delete")]
        public IActionResult DeleteSubcategories(SubcategoriesModel model)
        {
            model.DeleteUser = GetUsername();
            model.DeleteTime = DateTime.Now;
            model.IsActive = false;
            unitOfWork.SubcategoriesRepository.Update(unitOfWork.SubcategoriesRepository.GetSingleRecord<SubcategoriesModel>(u => u.ConcurrencyStamp == model.ConcurrencyStamp), model);
            unitOfWork.Complate();
            return Ok();
        }
        #endregion

        #region Unit

        [HttpGet]
        [Route("Unit/GetAll")]
        public IActionResult GetUnits()
        {
            var data = unitOfWork.UnitRepository.GetRecords<UnitModel>(u => u.IsActive).ToList();
            if (data.Count == 0)
                return NotFound();
            return Ok(data);
        }

        [HttpPost]
        [Route("Unit/Add")]
        public IActionResult AddUnit(UnitModel model)
        {
            var username = GetUsername();
            model.CreatedUser = username;
            model.IsActive = true;
            model.CreateTime = DateTime.Now;
            model.ConcurrencyStamp = Guid.NewGuid().ToString();
            unitOfWork.UnitRepository.Add(model);
            unitOfWork.Complate();
            return Ok();
        }

        [HttpPut]
        [Route("Unit/Update")]
        public IActionResult UpdateUnit(UnitModel model)
        {
            model.UpdatedUser = GetUsername();
            model.UpdateTime = DateTime.Now;
            unitOfWork.UnitRepository.Update(unitOfWork.UnitRepository.GetSingleRecord<UnitModel>(u => u.ConcurrencyStamp == model.ConcurrencyStamp), model);
            unitOfWork.Complate();
            return Ok();
        }

        [HttpDelete]
        [Route("Unit/Delete")]
        public IActionResult DeleteUnit(UnitModel model)
        {
            model.DeleteUser = GetUsername();
            model.DeleteTime = DateTime.Now;
            model.IsActive = false;
            unitOfWork.UnitRepository.Update(unitOfWork.UnitRepository.GetSingleRecord<UnitModel>(u => u.ConcurrencyStamp == model.ConcurrencyStamp), model);
            unitOfWork.Complate();
            return Ok();
        }
        #endregion

        private string GetUsername()
        {
            return (this.User.Identity as ClaimsIdentity).FindFirst(ClaimTypes.Name)?.Value;
        }
    }
}
