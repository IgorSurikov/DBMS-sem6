using FootballStatisticsArchive.Database.Models;

namespace FootballStatisticsArchive.Database.Interfaces
{
    public interface IAccountReposetory
    {
        public DbOutput Registration(string nickname, string email, string password);
        public DbOutput Login(string email, string password);
        public DbOutput GetUser(int userId);
    }
}
