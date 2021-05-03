using System;
using System.Collections.Generic;
using System.Text;

namespace FootballStatisticsArchive.Database.Models
{
    public class Team
    {
        public int TeamId { get; set; }
        public string Name { get; set; }
        public string Initial { get; set; }
        public string CoachName { get; set; }
        public ICollection<Player> Players { get; set; }
    }
}
