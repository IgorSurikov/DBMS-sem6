using FootballStatisticsArchive.Database.Models;
using FootballStatisticsArchive.Services.Interfaces;
using FootballStatisticsArchive.Views;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FootballStatisticsArchive.Web.Controllers
{
    [Route("[controller]")]
    public class AccountController : Controller
    {
        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }
        private readonly IAccountService accountService;

        [HttpPost]
        [Route("register")]
        public IActionResult Registration([FromForm]RegistrationViewModel vm)
        {
            string registationResult = this.accountService.Registration(vm.Nickname, vm.Email, vm.Password);
            if (registationResult != null)
            {
                return BadRequest(registationResult);
            }
            return Ok();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromForm] LoginViewModel vm)
        {
            string resultLogin = this.accountService.Login(vm.Email, vm.Password);
            try
            {
                int userId = Convert.ToInt32(resultLogin);
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, userId.ToString())
                };
                var id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
                ApplicationUser user = this.accountService.GetUser(userId);
                return Ok(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(resultLogin);
            }
        }

        [HttpGet]
        [Authorize]
        [Route("logout")]
        public virtual async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            HttpContext.Response.Cookies.Delete(".AspNetCore.Cookies");
            return Ok();
        }

        [Authorize]
        [HttpGet]
        [Route("users/all")]
        public IActionResult GetAllUsers()
        {
            Claim userIdClaim = HttpContext.User.Identities.First().Claims.First();
            if (userIdClaim.Value == null)
            {
                return Unauthorized("You are not logged in!");
            }
            var isAdmin = this.IsAdmin(userIdClaim.Value);

            if (isAdmin != null)
            {
                return BadRequest(isAdmin);
            }
            var users = this.accountService.GetAllUsers();
            if(users == null)
            {
                return BadRequest("There is no users!");
            }
            return Ok(users);
        }


        [Authorize]
        [HttpGet]
        [Route("users/admin")]
        public IActionResult IsAdmin()
        {
            Claim userIdClaim = HttpContext.User.Identities.First().Claims.First();
            if (userIdClaim.Value == null)
            {
                return Unauthorized("You are not logged in!");
            }

            var isAdmin = this.IsAdmin(userIdClaim.Value);

            if(isAdmin != null)
            {
                return BadRequest(isAdmin);
            }
            return Ok();
        }

        [Authorize]
        [HttpPost]
        [Route("users/update")]
        public IActionResult ChangeRole([FromForm] ChangeRoleViewModel model)
        {
            if(model.RoleId == -1)
            {
                return BadRequest("roleId required!");
            }
            Claim userIdClaim = HttpContext.User.Identities.First().Claims.First();
            if (userIdClaim.Value == null)
            {
                return Unauthorized("You mast be logged in!");
            }
            var isAdmin = this.IsAdmin(userIdClaim.Value);
            if (isAdmin != null)
            {
                return BadRequest(isAdmin);

            }
            var result = this.accountService.ChangeRole(model.UserId, model.RoleId);
            if (result == null)
            {
                return BadRequest("Input data is incorrect!");
            }
            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        [Route("users/delete")]
        public IActionResult DeleteUser([FromForm] int userId = -1)
        {
            if (userId == -1)
            {
                return BadRequest("Bad params!");
            }
            Claim userIdClaim = HttpContext.User.Identities.First().Claims.First();
            if(userIdClaim.Value == null)
            {
                return Unauthorized("You are not logged in!");
            }
            var isAdmin = this.IsAdmin(userIdClaim.Value);
            if(isAdmin != null)
            {
                return BadRequest(isAdmin);

            }
            if(!this.accountService.DeleteUser(userId))
            {
                return BadRequest("Not expected error!");
            }
            return Ok();
        }

        private string IsAdmin(string userId)
        {
            var user = this.accountService.GetUser(Convert.ToInt32(userId));
            if (user == null)
            {
                return "Input data is incorrect!";
            }
            if (user.UserRole.Name != "ADMIN")
            {
                return "You are not admin!";
            }
            return null;
        }
    }
}
