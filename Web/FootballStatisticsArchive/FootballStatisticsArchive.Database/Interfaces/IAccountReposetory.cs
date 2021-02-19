using System.Collections.Generic;

namespace FootballStatisticsArchive.Database.Interfaces
{
    public interface IAccountReposetory
    {
        public ICollection<object> Registration(string nickname, string email, string password);
    }
}
