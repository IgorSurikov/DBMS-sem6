using FootballStatisticsArchive.Database.Interfaces;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;

namespace FootballStatisticsArchive.Database.Repositories
{
    public class AccountReposetory : IAccountReposetory
    {
        public AccountReposetory(IBaseReposetory baseReposetory)
        {
            this.baseReposetory = baseReposetory;
        }

        private readonly IBaseReposetory baseReposetory;

        public ICollection<object> Registration(string nickname, string email, string password)
        {
            var arg1 = new Tuple<string, OracleDbType, object>("p_nickname", OracleDbType.Varchar2, nickname);
            var arg2 = new Tuple<string, OracleDbType, object>("p_email", OracleDbType.Varchar2, email);
            var arg3 = new Tuple<string, OracleDbType, object>("p_password", OracleDbType.Varchar2, password);

            return this.baseReposetory.RunDbRequest("post_register_user", args: new Tuple<string, OracleDbType, object>[] { arg1, arg2, arg3 });
        }

    }
}
