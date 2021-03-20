using System;
using System.Collections.Generic;
using System.Text;

namespace FootballStatisticsArchive.Database.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Initial { get; set; }
        public ICollection<Player> Players { get; set; }
    }
}
