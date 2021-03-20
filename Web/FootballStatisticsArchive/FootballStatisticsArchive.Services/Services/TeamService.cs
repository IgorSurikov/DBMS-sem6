using FootballStatisticsArchive.Database.Interfaces;
using FootballStatisticsArchive.Database.Models;
using FootballStatisticsArchive.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FootballStatisticsArchive.Services.Services
{
    public class TeamService : ITeamService
    {
        public TeamService(ITeamRepository teamRepository)
        {
            this.teamRepository = teamRepository;
        }

        private readonly ITeamRepository teamRepository;

        public ICollection<Team> GetTeams(int tournamentId)
        {
            var teamGetResult = this.teamRepository.GetTeams(tournamentId);
            if(teamGetResult.Result == DbResult.Faild)
            {
                return null;
            }

            List<Team> teams = new List<Team>();
            List<List<object>> stuffTeams = new List<List<object>>();

            for (int i = 0; i < teamGetResult.OutElements.Count; i += 8)
            {
                stuffTeams.Add(teamGetResult.OutElements.Skip(i).Take(8).ToList());
            }

            var teamIds = stuffTeams.Select(obj => obj.ElementAt(1)).GroupBy(s => s).Select(obj => Convert.ToInt32(obj.Key)).ToList();

            foreach(int teamId in teamIds)
            {
                var team = stuffTeams.First(obj => Convert.ToInt32(obj.ElementAt(1)) == teamId);
                teams.Add(new Team()
                {
                    Id = teamId,
                    Name = team.ElementAt(2).ToString(),
                    Initial = team.ElementAt(3).ToString(),
                    Players = stuffTeams.Where(obj => Convert.ToInt32(obj.ElementAt(1)) == teamId).Select(playerInfo => new Player()
                    {
                        id = Convert.ToInt32(playerInfo.ElementAt(5)),
                        Name = playerInfo.ElementAt(6).ToString(),
                        ShirtNumber = Convert.ToInt32(playerInfo.ElementAt(7))
                    }).ToList()
                }); ;
            }

            return teams;
        }
    }
}
