using FootballStatisticsArchive.Database.Models;
using Oracle.ManagedDataAccess.Client;
using System;

namespace FootballStatisticsArchive.Database.Interfaces
{
    public interface IBaseReposetory
    {
        public void SetUpConnectionOptions();
        public DbOutput RunDbRequest(string funckName, bool mustRespond = false, Tuple<string, OracleDbType, object>[] args = null, Tuple<string, OracleDbType> returnVal = null);
    }
}
