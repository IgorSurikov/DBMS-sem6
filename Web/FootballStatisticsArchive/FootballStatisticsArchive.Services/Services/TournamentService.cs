using FootballStatisticsArchive.Database.Interfaces;
using FootballStatisticsArchive.Database.Models;
using FootballStatisticsArchive.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FootballStatisticsArchive.Services.Services
{
    public class TournamentService : ITournamentService
    {
        public TournamentService(ITournamentRepository tournamentRepository)
        {
            this.tournamentRepository = tournamentRepository;
        }

        private readonly ITournamentRepository tournamentRepository;

        public ICollection<Tournament> GetTournaments()
        {
            List<Tournament> tournaments = new List<Tournament>();
            var tournamentResult = this.tournamentRepository.GetTournaments();
            if (tournamentResult.Result == DbResult.Faild)
            {
                return null;
            }
            for (int i = 0; i < tournamentResult.OutElements.Count; i += 19)
            {
                tournaments.Add(new Tournament()
                {
                    TournamentId = Convert.ToInt32(tournamentResult.OutElements.ElementAt(i)),
                    Year = Convert.ToInt32(tournamentResult.OutElements.ElementAt(i + 1)),
                    Country = tournamentResult.OutElements.ElementAt(i + 2).ToString(),
                    Name = tournamentResult.OutElements.ElementAt(i + 3).ToString(),
                    Winner = new Team()
                    {
                        TeamId = Convert.ToInt32(tournamentResult.OutElements.ElementAt(i + 4)),
                        Name = tournamentResult.OutElements.ElementAt(i + 5).ToString(),
                        Initial = tournamentResult.OutElements.ElementAt(i + 6).ToString()
                    },
                    RunnersUp = new Team()
                    {
                        TeamId = Convert.ToInt32(tournamentResult.OutElements.ElementAt(i + 7)),
                        Name = tournamentResult.OutElements.ElementAt(i + 8).ToString(),
                        Initial = tournamentResult.OutElements.ElementAt(i + 9).ToString()
                    },
                    Third = new Team()
                    {
                        TeamId = Convert.ToInt32(tournamentResult.OutElements.ElementAt(i + 10)),
                        Name = tournamentResult.OutElements.ElementAt(i + 11).ToString(),
                        Initial = tournamentResult.OutElements.ElementAt(i + 12).ToString()
                    },
                    Fourth = new Team()
                    {
                        TeamId = Convert.ToInt32(tournamentResult.OutElements.ElementAt(i + 13)),
                        Name = tournamentResult.OutElements.ElementAt(i + 14).ToString(),
                        Initial = tournamentResult.OutElements.ElementAt(i + 15).ToString()
                    },
                    GoalsScored = Convert.ToInt32(tournamentResult.OutElements.ElementAt(i + 16)),
                    QualifiedTeams = Convert.ToInt32(tournamentResult.OutElements.ElementAt(i + 17)),
                    MatchesPlayed = Convert.ToInt32(tournamentResult.OutElements.ElementAt(i + 18))
                });
            }
            return tournaments;
        }
    }
}
