using System.Collections.Generic;

namespace FootballStatisticsArchive.Database.Models
{
    public class DbOutput
    {
        public ICollection<object> OutElements { get; set; }
        public string ErrorMessage { get; set; }
        public DbResult Result { get; set; }
    }
}
