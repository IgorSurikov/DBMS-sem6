using FootballStatisticsArchive.Database.Interfaces;
using FootballStatisticsArchive.Database.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace FootballStatisticsArchive.Database.Repositories
{
    public class TournamentRepository : ITournamentRepository
    {
        public TournamentRepository(IBaseReposetory baseReposetory)
        {
            this.baseReposetory = baseReposetory;
        }

        private readonly IBaseReposetory baseReposetory;

        public DbOutput GetTournaments()
        {
            var returnValArg = new Tuple<string, OracleDbType>("out_tournaments", OracleDbType.RefCursor);

            return this.baseReposetory.RunDbRequest("get_tournaments", true, new Tuple<string, OracleDbType, object>[] { }, returnValArg);
        }
    }
}
