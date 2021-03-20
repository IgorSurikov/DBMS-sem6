using System;
using System.Collections.Generic;
using System.Text;

namespace FootballStatisticsArchive.Database.Models
{
    public class Tournament
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public string Country { get; set; }
        public string Name { get; set; }
        public Team Winner { get; set; }
        public int WinnerId { get; set; }
        public Team RunnersUp { get; set; }
        public int RunnersUpId { get; set; }
        public Team Third { get; set; }
        public int ThirdId { get; set; }
        public Team Fourth { get; set; }
        public int FourthId{ get; set; }
        public int GoalsScored { get; set; }
        public int QualifiedTeams { get; set; }
        public int MatchesPlayed { get; set; }
    }
}
