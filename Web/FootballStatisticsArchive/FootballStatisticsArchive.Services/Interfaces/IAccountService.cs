using FootballStatisticsArchive.Database.Models;
using System.Collections.Generic;

namespace FootballStatisticsArchive.Services.Interfaces
{
    public interface IAccountService
    {
        public string Registration(string nickname, string email, string password);
        public string Login(string email, string password);
        public ApplicationUser GetUser(int userId);
        public ApplicationUser ChangeRole(int userId, int roleId);
        public ICollection<ApplicationUser> GetAllUsers();
        public bool DeleteUser(int userId);
    }
}
