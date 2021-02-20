using FootballStatisticsArchive.Services.Interfaces;
using FootballStatisticsArchive.Views;
using Microsoft.AspNetCore.Mvc;

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
    }
}
