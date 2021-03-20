using System;
using System.Collections.Generic;
using System.Text;

namespace FootballStatisticsArchive.Database.Models
{
    public class Tournament
    {
        public int TournamentId { get; set; }
        public int Year { get; set; }
        public string Country { get; set; }
        public string Name { get; set; }
        public Team Winner { get; set; }
        public Team RunnersUp { get; set; }
        public Team Third { get; set; }
        public Team Fourth { get; set; }
        public int GoalsScored { get; set; }
        public int QualifiedTeams { get; set; }
        public int MatchesPlayed { get; set; }
    }
}
