using FootballStatisticsArchive.Database.Interfaces;
using FootballStatisticsArchive.Database.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace FootballStatisticsArchive.Database.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        public TeamRepository(IBaseReposetory baseReposetory)
        {
            this.baseReposetory = baseReposetory;
        }

        private readonly IBaseReposetory baseReposetory;

        public DbOutput GetTeams(int tournamentId)
        {
            var arg1 = new Tuple<string, OracleDbType, object>("p_tournament_id", OracleDbType.Decimal, tournamentId);
            var returnValArg = new Tuple<string, OracleDbType>("out_teams", OracleDbType.RefCursor);

            return this.baseReposetory.RunDbRequest("get_teams", true, new Tuple<string, OracleDbType, object>[] { arg1 }, returnValArg);
        }
    }
}
