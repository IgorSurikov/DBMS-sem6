﻿using FootballStatisticsArchive.Database.Interfaces;
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

        public DbOutput GetTournaments(int year)
        {
            var returnValArg = new Tuple<string, OracleDbType>("out_tournaments", OracleDbType.RefCursor);
            var arg1 = new Tuple<string, OracleDbType, object>("p_year", OracleDbType.Decimal, null);

            if (year != 0)
            {
                arg1 = new Tuple<string, OracleDbType, object>("p_year", OracleDbType.Decimal, year);
            }


            return this.baseReposetory.RunDbRequest("get_tournaments", true, new Tuple<string, OracleDbType, object>[] { arg1 }, returnValArg);
        }

        public DbOutput GetMatches(int tournamentId)
        {
            var arg1 = new Tuple<string, OracleDbType, object>("p_tournament_id", OracleDbType.Decimal, tournamentId);
            var returnValArg = new Tuple<string, OracleDbType>("out_matches", OracleDbType.RefCursor);

            return this.baseReposetory.RunDbRequest("get_matches", true, new Tuple<string, OracleDbType, object>[] { arg1 }, returnValArg);
        }   
    }
}
