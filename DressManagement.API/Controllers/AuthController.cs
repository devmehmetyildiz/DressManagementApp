using DressManagement.API.DataAccess;
using DressManagement.API.DataAccess.Repositories;
using DressManagement.API.Models.Auth;
using DressManagement.API.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DressManagement.API.Controllers
{
    [Route("api")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IConfiguration _configuration;
        private readonly ILogger<AuthController> _logger;
        private readonly ApplicationDBContext _context;
        UnitOfWork unitOfWork;

        public AuthController(IConfiguration configuration, ILogger<AuthController> logger, ApplicationDBContext context)
        {
            _configuration = configuration;
            _logger = logger;
            _context = context;
            unitOfWork = new UnitOfWork(context);
        }

        #region Auth login system 

        [AllowAnonymous]
        [HttpGet]
        [Route("Auth/Test")]
        public IActionResult Test()
        {
            return Ok("OK");
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("DBTest")]
        public IActionResult DBTest()
        {
            return Ok($"Aktif Kullanıcı Sayısı = {unitOfWork.UserRepository.GetAll().Count}");
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (unitOfWork.UserRepository.GetAll().Count > 0)
            {
                return BadRequest(new ResponseModel { Status = "ERROR", Massage = "Admin Kullanıcı Oluşturuldu. Bu fonksiyon geçersiz" });
            }
            var userExist = unitOfWork.UserRepository.FindUserByName(model.Username);
            if (userExist != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseModel { Status = "Error", Massage = "Bu Kullanıcı Adı Daha Önce Alındı" });
            var userGuid = Guid.NewGuid().ToString();
            var RoleGuid = Guid.NewGuid().ToString();
            var salt = CryptographyProcessor.CreateSalt(30);
            unitOfWork.UsertoSaltRepository.Add(new UsertoSaltModel { Salt = salt, UserID = userGuid });
            UserModel user = new UserModel()
            {
                Id = 0,
                Username = model.Username,
                NormalizedUsername = model.Username.ToUpper(),
                ConcurrencyStamp = userGuid,
                Email = model.Email,
                AccessFailedCount = 0,
                IsActive = true,
                CreatedUser = "System",
                CreateTime = DateTime.Now,
                PasswordHash = CryptographyProcessor.GenerateHash(model.Password, salt),
                PhoneNumber = "",
            };
            unitOfWork.UserRepository.Add(user);
            ConfigureRoles();
            unitOfWork.RolesRepository.Add(new RoleModel { Id = 0, ConcurrencyStamp = RoleGuid, CreatedUser = "System", CreateTime = DateTime.Now, IsActive = true, Name = "Admin" });
            unitOfWork.RoletoAuthoryRepository.AddAuthorytoRole(new RoletoAuthoryModel { Id = 0, RoleID = RoleGuid, AuthoryID = unitOfWork.AuthoryRepository.FindAuthoryByName(UserAuthory.Admin).ConcurrencyStamp });
            unitOfWork.RoletoAuthoryRepository.AddAuthorytoRole(new RoletoAuthoryModel { Id = 0, RoleID = RoleGuid, AuthoryID = unitOfWork.AuthoryRepository.FindAuthoryByName(UserAuthory.User_Screen).ConcurrencyStamp });
            unitOfWork.RoletoAuthoryRepository.AddAuthorytoRole(new RoletoAuthoryModel { Id = 0, RoleID = RoleGuid, AuthoryID = unitOfWork.AuthoryRepository.FindAuthoryByName(UserAuthory.User_Add).ConcurrencyStamp });
            unitOfWork.RoletoAuthoryRepository.AddAuthorytoRole(new RoletoAuthoryModel { Id = 0, RoleID = RoleGuid, AuthoryID = unitOfWork.AuthoryRepository.FindAuthoryByName(UserAuthory.User_Update).ConcurrencyStamp });
            unitOfWork.RoletoAuthoryRepository.AddAuthorytoRole(new RoletoAuthoryModel { Id = 0, RoleID = RoleGuid, AuthoryID = unitOfWork.AuthoryRepository.FindAuthoryByName(UserAuthory.User_Delete).ConcurrencyStamp });
            unitOfWork.RoletoAuthoryRepository.AddAuthorytoRole(new RoletoAuthoryModel { Id = 0, RoleID = RoleGuid, AuthoryID = unitOfWork.AuthoryRepository.FindAuthoryByName(UserAuthory.User_ManageAll).ConcurrencyStamp });
            unitOfWork.RoletoAuthoryRepository.AddAuthorytoRole(new RoletoAuthoryModel { Id = 0, RoleID = RoleGuid, AuthoryID = unitOfWork.AuthoryRepository.FindAuthoryByName(UserAuthory.Roles_Screen).ConcurrencyStamp });
            unitOfWork.RoletoAuthoryRepository.AddAuthorytoRole(new RoletoAuthoryModel { Id = 0, RoleID = RoleGuid, AuthoryID = unitOfWork.AuthoryRepository.FindAuthoryByName(UserAuthory.Roles_Add).ConcurrencyStamp });
            unitOfWork.RoletoAuthoryRepository.AddAuthorytoRole(new RoletoAuthoryModel { Id = 0, RoleID = RoleGuid, AuthoryID = unitOfWork.AuthoryRepository.FindAuthoryByName(UserAuthory.Roles_Update).ConcurrencyStamp });
            unitOfWork.RoletoAuthoryRepository.AddAuthorytoRole(new RoletoAuthoryModel { Id = 0, RoleID = RoleGuid, AuthoryID = unitOfWork.AuthoryRepository.FindAuthoryByName(UserAuthory.Roles_Delete).ConcurrencyStamp });
            unitOfWork.RoletoAuthoryRepository.AddAuthorytoRole(new RoletoAuthoryModel { Id = 0, RoleID = RoleGuid, AuthoryID = unitOfWork.AuthoryRepository.FindAuthoryByName(UserAuthory.Roles_ManageAll).ConcurrencyStamp });
            unitOfWork.RoletoAuthoryRepository.AddAuthorytoRole(new RoletoAuthoryModel { Id = 0, RoleID = RoleGuid, AuthoryID = unitOfWork.AuthoryRepository.FindAuthoryByName(UserAuthory.Admin).ConcurrencyStamp });
            unitOfWork.UsertoRoleRepository.AddRolestoUser(new UsertoRoleModel { Id = 0, RoleID = RoleGuid, UserID = userGuid });
            unitOfWork.Complate();
            return Ok(new ResponseModel { Status = "Success", Massage = "Kullanıcı Başarı ile Oluşturuldu" });
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = unitOfWork.UserRepository.FindUserByName(model.Username);
            if ((user == null))
            {
                return NotFound(new ResponseModel { Status = "Error", Massage = "Kullanıcı Bulunamadı" });
            }
            if (!CheckPassword(user, model.Password))
            {
                return Unauthorized(new ResponseModel { Status = "Error", Massage = "Kullanıcı Adı veya Şifre Hatalı" });
            }
            var authClaims = new List<Claim>
                {
                     new Claim(ClaimTypes.Name,user.Username),
                     new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
                };
            List<AuthoryModel> Yetkiler = unitOfWork.AuthoryRepository.GetAll();
            foreach (var userrole in unitOfWork.UsertoRoleRepository.GetRolesbyUser(user.ConcurrencyStamp))
            {
                List<string> yetkis = unitOfWork.RoletoAuthoryRepository.GetAuthoriesByRole(userrole);
                foreach (var yetki in yetkis)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, Yetkiler.FirstOrDefault(u => u.ConcurrencyStamp == yetki).Name));
                }
            }
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    expires: DateTime.Now.AddHours(3),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );
            Response.Cookies.Append("X-Access-Token", new JwtSecurityTokenHandler().WriteToken(token), new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict });
            Response.Cookies.Append("X-Username", user.Username, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict });
            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo,
                User = user.Username
            });

        }

        [Authorize]
        [HttpGet]
        [Route("GetActiveUser")]
        public async Task<IActionResult> GetActiveUser()
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;
            return Ok(userId);
        }

        [Authorize(Roles = UserAuthory.Admin)]
        [HttpGet]
        [Route("ConfigureRoles")]
        public async Task<IActionResult> ConfigureRolesAsAdmin()
        {
            ConfigureRoles();
            return Ok();
        }

        private bool CheckPassword(UserModel user, string password)
        {
            return CryptographyProcessor.AreEqual(password, user.PasswordHash, unitOfWork.UsertoSaltRepository.GetSaltByGuid(user.ConcurrencyStamp));
        }

        private async void ConfigureRoles()
        {
            List<AuthoryModel> Roles = new List<AuthoryModel>();
            List<AuthoryModel> newRoles = new List<AuthoryModel>();

            Roles.Add(new AuthoryModel { Group = UserAuthory.BaseGroup, Name = UserAuthory.Basic });
            Roles.Add(new AuthoryModel { Group = UserAuthory.BaseGroup, Name = UserAuthory.Admin });
            Roles.Add(new AuthoryModel { Group = UserAuthory.User, Name = UserAuthory.User_Screen });
            Roles.Add(new AuthoryModel { Group = UserAuthory.User, Name = UserAuthory.User_Add });
            Roles.Add(new AuthoryModel { Group = UserAuthory.User, Name = UserAuthory.User_Update });
            Roles.Add(new AuthoryModel { Group = UserAuthory.User, Name = UserAuthory.User_Delete });
            Roles.Add(new AuthoryModel { Group = UserAuthory.User, Name = UserAuthory.User_ManageAll });
            Roles.Add(new AuthoryModel { Group = UserAuthory.Department, Name = UserAuthory.Department_Screen });
            Roles.Add(new AuthoryModel { Group = UserAuthory.Department, Name = UserAuthory.Department_Add });
            Roles.Add(new AuthoryModel { Group = UserAuthory.Department, Name = UserAuthory.Department_Update });
            Roles.Add(new AuthoryModel { Group = UserAuthory.Department, Name = UserAuthory.Department_Delete });
            Roles.Add(new AuthoryModel { Group = UserAuthory.Department, Name = UserAuthory.Department_ManageAll });
            Roles.Add(new AuthoryModel { Group = UserAuthory.Stock, Name = UserAuthory.Stock_Screen });
            Roles.Add(new AuthoryModel { Group = UserAuthory.Stock, Name = UserAuthory.Stock_Add });
            Roles.Add(new AuthoryModel { Group = UserAuthory.Stock, Name = UserAuthory.Stock_Update });
            Roles.Add(new AuthoryModel { Group = UserAuthory.Stock, Name = UserAuthory.Stock_Delete });
            Roles.Add(new AuthoryModel { Group = UserAuthory.Stock, Name = UserAuthory.Stock_ManageAll });
            Roles.Add(new AuthoryModel { Group = UserAuthory.Process, Name = UserAuthory.Process_Screen });
            Roles.Add(new AuthoryModel { Group = UserAuthory.Process, Name = UserAuthory.Process_Add });
            Roles.Add(new AuthoryModel { Group = UserAuthory.Process, Name = UserAuthory.Process_Update });
            Roles.Add(new AuthoryModel { Group = UserAuthory.Process, Name = UserAuthory.Process_Delete });
            Roles.Add(new AuthoryModel { Group = UserAuthory.Process, Name = UserAuthory.Process_ManageAll });
            Roles.Add(new AuthoryModel { Group = UserAuthory.Patients, Name = UserAuthory.Patients_Screen });
            Roles.Add(new AuthoryModel { Group = UserAuthory.Patients, Name = UserAuthory.Patients_Add });
            Roles.Add(new AuthoryModel { Group = UserAuthory.Patients, Name = UserAuthory.Patients_Update });
            Roles.Add(new AuthoryModel { Group = UserAuthory.Patients, Name = UserAuthory.Patients_Delete });
            Roles.Add(new AuthoryModel { Group = UserAuthory.Patients, Name = UserAuthory.Patients_ManageAll });
            Roles.Add(new AuthoryModel { Group = UserAuthory.Patients, Name = UserAuthory.Patients_UploadFile });
            Roles.Add(new AuthoryModel { Group = UserAuthory.Patients, Name = UserAuthory.Patients_DownloadFile });
            Roles.Add(new AuthoryModel { Group = UserAuthory.Patients, Name = UserAuthory.Patients_ViewFile });
            Roles.Add(new AuthoryModel { Group = UserAuthory.Patienttype, Name = UserAuthory.Patienttype_Screen });
            Roles.Add(new AuthoryModel { Group = UserAuthory.Patienttype, Name = UserAuthory.Patienttype_Add });
            Roles.Add(new AuthoryModel { Group = UserAuthory.Patienttype, Name = UserAuthory.Patienttype_Update });
            Roles.Add(new AuthoryModel { Group = UserAuthory.Patienttype, Name = UserAuthory.Patienttype_Delete });
            Roles.Add(new AuthoryModel { Group = UserAuthory.Patienttype, Name = UserAuthory.Patienttype_ManageAll });
            Roles.Add(new AuthoryModel { Group = UserAuthory.Unit, Name = UserAuthory.Unit_Screen });
            Roles.Add(new AuthoryModel { Group = UserAuthory.Unit, Name = UserAuthory.Unit_Add });
            Roles.Add(new AuthoryModel { Group = UserAuthory.Unit, Name = UserAuthory.Unit_Update });
            Roles.Add(new AuthoryModel { Group = UserAuthory.Unit, Name = UserAuthory.Unit_Delete });
            Roles.Add(new AuthoryModel { Group = UserAuthory.Unit, Name = UserAuthory.Unit_ManageAll });
            Roles.Add(new AuthoryModel { Group = UserAuthory.Stations, Name = UserAuthory.Stations_Screen });
            Roles.Add(new AuthoryModel { Group = UserAuthory.Stations, Name = UserAuthory.Stations_Add });
            Roles.Add(new AuthoryModel { Group = UserAuthory.Stations, Name = UserAuthory.Stations_Update });
            Roles.Add(new AuthoryModel { Group = UserAuthory.Stations, Name = UserAuthory.Stations_Delete });
            Roles.Add(new AuthoryModel { Group = UserAuthory.Stations, Name = UserAuthory.Stations_ManageAll });
            Roles.Add(new AuthoryModel { Group = UserAuthory.Case, Name = UserAuthory.Case_Screen });
            Roles.Add(new AuthoryModel { Group = UserAuthory.Case, Name = UserAuthory.Case_Add });
            Roles.Add(new AuthoryModel { Group = UserAuthory.Case, Name = UserAuthory.Case_Update });
            Roles.Add(new AuthoryModel { Group = UserAuthory.Case, Name = UserAuthory.Case_Delete });
            Roles.Add(new AuthoryModel { Group = UserAuthory.Case, Name = UserAuthory.Case_ManageAll });
            Roles.Add(new AuthoryModel { Group = UserAuthory.Costumertype, Name = UserAuthory.Costumertype_Screen });
            Roles.Add(new AuthoryModel { Group = UserAuthory.Costumertype, Name = UserAuthory.Costumertype_Add });
            Roles.Add(new AuthoryModel { Group = UserAuthory.Costumertype, Name = UserAuthory.Costumertype_Update });
            Roles.Add(new AuthoryModel { Group = UserAuthory.Costumertype, Name = UserAuthory.Costumertype_Delete });
            Roles.Add(new AuthoryModel { Group = UserAuthory.Costumertype, Name = UserAuthory.Costumertype_ManageAll });
            Roles.Add(new AuthoryModel { Group = UserAuthory.Roles, Name = UserAuthory.Roles_Screen });
            Roles.Add(new AuthoryModel { Group = UserAuthory.Roles, Name = UserAuthory.Roles_Add });
            Roles.Add(new AuthoryModel { Group = UserAuthory.Roles, Name = UserAuthory.Roles_Update });
            Roles.Add(new AuthoryModel { Group = UserAuthory.Roles, Name = UserAuthory.Roles_Delete });
            Roles.Add(new AuthoryModel { Group = UserAuthory.Roles, Name = UserAuthory.Roles_ManageAll });
            Roles.Add(new AuthoryModel { Group = UserAuthory.File, Name = UserAuthory.File_Screen });
            Roles.Add(new AuthoryModel { Group = UserAuthory.File, Name = UserAuthory.File_Add });
            Roles.Add(new AuthoryModel { Group = UserAuthory.File, Name = UserAuthory.File_Update });
            Roles.Add(new AuthoryModel { Group = UserAuthory.File, Name = UserAuthory.File_Delete });
            Roles.Add(new AuthoryModel { Group = UserAuthory.File, Name = UserAuthory.File_ManageAll });
            Roles.Add(new AuthoryModel { Group = UserAuthory.Dashboard, Name = UserAuthory.Dashboard_AllScreen });
            Roles.Add(new AuthoryModel { Group = UserAuthory.Dashboard, Name = UserAuthory.Dashboard_DepartmentScreen });
            Roles.Add(new AuthoryModel { Group = UserAuthory.Reminding, Name = UserAuthory.Reminding_Screen });
            Roles.Add(new AuthoryModel { Group = UserAuthory.Reminding, Name = UserAuthory.Reminding_Add });
            Roles.Add(new AuthoryModel { Group = UserAuthory.Reminding, Name = UserAuthory.Reminding_Update });
            Roles.Add(new AuthoryModel { Group = UserAuthory.Reminding, Name = UserAuthory.Reminding_Delete });
            Roles.Add(new AuthoryModel { Group = UserAuthory.Reminding, Name = UserAuthory.Reminding_ManageAll });
            Roles.Add(new AuthoryModel { Group = UserAuthory.Reminding, Name = UserAuthory.Reminding_DefineforAll });
            Roles.Add(new AuthoryModel { Group = UserAuthory.Reminding, Name = UserAuthory.Reminding_Define });
            foreach (var role in Roles)
            {
                var dbRole = unitOfWork.AuthoryRepository.FindAuthoryByName(role.Name);
                if (dbRole == null)
                {
                    var model = new AuthoryModel { Name = role.Name, Group = role.Group, ConcurrencyStamp = Guid.NewGuid().ToString() };
                    unitOfWork.AuthoryRepository.Add(model);
                    newRoles.Add(model);
                }
            }
            if (newRoles.Count > 0)
            {
                unitOfWork.Complate();
            }
        }

        #endregion

        #region Roles 

        [Authorize(Roles = UserAuthory.Roles_Screen)]
        [Route("Roles/GetAll")]
        [HttpGet]
        public IActionResult GetAll()
        {
            var roles = unitOfWork.RolesRepository.GetAll().Where(u => u.IsActive).ToList();
            foreach (var role in roles)
            {
                List<string> authories = unitOfWork.RoletoAuthoryRepository.GetAll().Where(u => u.RoleID == role.ConcurrencyStamp).Select(u => u.AuthoryID).ToList();
                role.Authories.AddRange(unitOfWork.AuthoryRepository.GetAuthoriesbyGuids(authories));
            }
            return Ok(roles);
        }

        [Authorize(Roles = UserAuthory.Roles_Screen)]
        [Route("Roles/GetSelectedRole")]
        [HttpGet]
        public IActionResult GetSelectedRole(int ID)
        {
            var role = unitOfWork.RolesRepository.Getbyid(ID);
            List<string> authories = unitOfWork.RoletoAuthoryRepository.GetAll().Where(u => u.RoleID == role.ConcurrencyStamp).Select(u => u.AuthoryID).ToList();
            role.Authories.AddRange(unitOfWork.AuthoryRepository.GetAuthoriesbyGuids(authories));
            return Ok(role);
        }

        [Authorize(Roles = UserAuthory.Roles_Screen)]
        [Route("Roles/GetAllAuthories")]
        [HttpGet]
        public IActionResult GetAllroles()
        {
            return Ok(unitOfWork.AuthoryRepository.GetAll().Where(U => U.Name != UserAuthory.Admin).ToList().OrderBy(u => u.Group));
        }

        [Authorize(Roles = UserAuthory.Roles_Add)]
        [Route("Roles/Add")]
        [HttpPost]
        public IActionResult Add(RoleModel model)
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var username = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;
            model.CreatedUser = username;
            model.IsActive = true;
            model.CreateTime = DateTime.Now;
            model.ConcurrencyStamp = Guid.NewGuid().ToString();
            unitOfWork.RolesRepository.Add(model);
            foreach (var yetki in model.Authories)
            {
                unitOfWork.RoletoAuthoryRepository.AddAuthorytoRole(new RoletoAuthoryModel { RoleID = model.ConcurrencyStamp, AuthoryID = yetki.ConcurrencyStamp });
            }
            unitOfWork.Complate();
            return Ok();
        }

        [Authorize(Roles = (UserAuthory.Roles_Screen + "," + UserAuthory.Roles_Update))]
        [Route("Roles/Update")]
        [HttpPost]
        public IActionResult Update(RoleModel model)
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var username = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;
            model.UpdatedUser = username;
            model.UpdateTime = DateTime.Now;
            unitOfWork.RolesRepository.Update(unitOfWork.RolesRepository.Getbyid(model.Id), model);
            unitOfWork.RoletoAuthoryRepository.DeleteAuthoriesbyRole(model.ConcurrencyStamp);
            foreach (var yetki in model.Authories)
            {
                unitOfWork.RoletoAuthoryRepository.AddAuthorytoRole(new RoletoAuthoryModel { RoleID = model.ConcurrencyStamp, AuthoryID = yetki.ConcurrencyStamp });
            }
            unitOfWork.Complate();
            return Ok();
        }

        [Authorize(Roles = UserAuthory.Roles_Delete)]
        [Route("Roles/Delete")]
        [HttpDelete]
        public IActionResult Delete(RoleModel model)
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var username = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;
            model.DeleteUser = username;
            model.IsActive = false;
            model.DeleteTime = DateTime.Now;
            unitOfWork.RolesRepository.Update(unitOfWork.RolesRepository.Getbyid(model.Id), model);
            unitOfWork.Complate();
            return Ok();
        }

        [Authorize(Roles = UserAuthory.Admin)]
        [Route("Roles/DeleteFromDB")]
        [HttpDelete]
        public IActionResult DeleteFromDB(RoleModel model)
        {
            unitOfWork.RolesRepository.Remove(model.Id);
            unitOfWork.RoletoAuthoryRepository.DeleteAuthoriesbyRole(model.ConcurrencyStamp);
            unitOfWork.Complate();
            return Ok();
        }

        #endregion

    }
}
