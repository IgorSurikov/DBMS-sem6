using System;
using System.Collections.Generic;
using System.Text;

namespace FootballStatisticsArchive.Database.Models
{
    public class Match
    {
        public int MatchId { get; set; }
        public int TournamentId { get; set; }
        public DateTime Date { get; set; }
        public string StageName { get; set; }
        public int StageNumber { get; set; }
        public Stadium Stadium { get; set; }
        public Team HomeTeam { get; set; }
        public int HomeTeamGoals { get; set; }
        public Team AwayTeam { get; set; }
        public int AwayTeamGoals { get; set; }
        public string WinConditions { get; set; }
        public string RefereeName { get; set; }
        public string RefereeAssistant1Name { get; set; }
        public string RefereeAssistant2Name { get; set; }
    }
}
