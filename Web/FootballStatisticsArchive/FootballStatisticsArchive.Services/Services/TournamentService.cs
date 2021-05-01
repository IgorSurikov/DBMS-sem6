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

        public ICollection<Tournament> GetTournaments(int year = 0)
        {
            List<Tournament> tournaments = new List<Tournament>();
            var tournamentResult = this.tournamentRepository.GetTournaments(year);
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
        public ICollection<Match> GetMatches(int tournamentId)
        {
            var matchesResult = this.tournamentRepository.GetMatches(tournamentId);
            if(matchesResult.Result == DbResult.Faild)
            {
                return null;
            }

            List<Match> matches = new List<Match>();

            for (int i = 0; i < matchesResult.OutElements.Count; i += 16)
            {
                matches.Add(new Match()
                {
                    MatchId = Convert.ToInt32(matchesResult.OutElements.ElementAt(i)),
                    TournamentId = Convert.ToInt32(matchesResult.OutElements.ElementAt(i + 1)),
                    Date = DateTime.Parse(matchesResult.OutElements.ElementAt(i + 2).ToString()),
                    Stadium = new Stadium()
                    {
                        Name = matchesResult.OutElements.ElementAt(i + 3).ToString(),
                        City = matchesResult.OutElements.ElementAt(i + 4).ToString()
                    },
                    StageName = matchesResult.OutElements.ElementAt(i + 5).ToString(),
                    HomeTeam = new Team()
                    {
                        TeamId = Convert.ToInt32(matchesResult.OutElements.ElementAt(i + 6)),
                        Name = matchesResult.OutElements.ElementAt(i + 7).ToString(),
                        Initial = matchesResult.OutElements.ElementAt(i + 8).ToString()
                    },
                    HomeTeamGoals = Convert.ToInt32(matchesResult.OutElements.ElementAt(i + 9)),
                    AwayTeam = new Team()
                    {
                        TeamId = Convert.ToInt32(matchesResult.OutElements.ElementAt(i + 10)),
                        Name = matchesResult.OutElements.ElementAt(i + 11).ToString(),
                        Initial = matchesResult.OutElements.ElementAt(i + 12).ToString()
                    },
                    AwayTeamGoals = Convert.ToInt32(matchesResult.OutElements.ElementAt(i + 13)),
                    WinConditions = matchesResult.OutElements.ElementAt(i + 14).ToString(),
                    Referee = matchesResult.OutElements.ElementAt(i + 15).ToString()
                });
            }
            return matches;
        }
    }
}
