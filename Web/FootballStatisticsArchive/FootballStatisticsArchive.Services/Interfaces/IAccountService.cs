namespace FootballStatisticsArchive.Services.Interfaces
{
    public interface IAccountService
    {
        public bool Registration(string nickname, string email, string password);
    }
}
