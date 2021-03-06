namespace FootballStatisticsArchive.Database.Models
{
    public class ApplicationUser
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Nickname { get; set; }
        public Role UserRole { get; set; }
    }
}
