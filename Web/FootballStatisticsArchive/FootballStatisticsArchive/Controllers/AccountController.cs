using FootballStatisticsArchive.Database.Models;
using FootballStatisticsArchive.Services.Interfaces;
using FootballStatisticsArchive.Views;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
    }
}
