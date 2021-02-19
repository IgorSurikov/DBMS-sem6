using FootballStatisticsArchive.Database.Interfaces;
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
        public bool Registration(string nickname, string email, string password)
        {
            return this.accountRepository.Registration(nickname, email, password) != null;
        }
    }
}
