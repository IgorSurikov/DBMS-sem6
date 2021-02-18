using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;

namespace FootballStatisticsArchive.Database.Interfaces
{
    public interface IBaseReposetory
    {
        public void SetUpConnectionOptions();
        public ICollection<object> RunDbRequest(string funckName, bool mustRespond = false, Tuple<string, OracleDbType, object>[] args = null, Tuple<string, OracleDbType, object> returnVal = null);
    }
}
