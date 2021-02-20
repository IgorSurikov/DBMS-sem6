using FootballStatisticsArchive.Database.Interfaces;
using FootballStatisticsArchive.Database.Models;
using FootballStatisticsArchive.Services.Interfaces;

namespace FootballStatisticsArchive.Services.Services
{
    public class AccountService : IAccountService
    {
        public AccountService(IAccountReposetory accountRepository)
        {
            this.accountRepository = accountRepository;
        }

        private readonly IAccountReposetory accountRepository;
        public string Registration(string nickname, string email, string password)
        {
            if(string.IsNullOrEmpty(password))
            {
                return "Password cannot be empty";
            }
            if (password.Length < 8)
            {
                return "Password length cannot be less then 8";
            }
            DbOutput registerResult = this.accountRepository.Registration(nickname, email, password);
            if(registerResult.Result == DbResult.Faild)
            {
                return registerResult.ErrorMessage = registerResult.ErrorMessage.Substring(0, 1).ToUpper() + registerResult.ErrorMessage.Substring(1, registerResult.ErrorMessage.Length - 1).ToLower();
            }
            return null;
        }
    }
}
