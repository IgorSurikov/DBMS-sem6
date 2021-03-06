using FootballStatisticsArchive.Database.Models;

namespace FootballStatisticsArchive.Services.Interfaces
{
    public interface IAccountService
    {
        public string Registration(string nickname, string email, string password);
        public string Login(string email, string password);
        public ApplicationUser GetUser(int userId);
    }
}
