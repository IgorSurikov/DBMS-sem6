using FootballStatisticsArchive.Database.Models;

namespace FootballStatisticsArchive.Database.Interfaces
{
    public interface IAccountReposetory
    {
        public DbOutput Registration(string nickname, string email, string password);
        public DbOutput Login(string email, string password);
        public DbOutput GetUser(int userId);
        public DbOutput ChangeRole(int userId, int roleId);
        public DbOutput GetAllUsers();
        public DbOutput DeleteUser(int userId);
    }
}
