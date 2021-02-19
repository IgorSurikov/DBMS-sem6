using FootballStatisticsArchive.Database.Interfaces;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;

namespace FootballStatisticsArchive.Database.Repositories
{
    public class BaseReposetory : IBaseReposetory
    {
        public BaseReposetory(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        private readonly IConfiguration configuration;
        public void SetUpConnectionOptions()
        {
            OracleConfiguration.TnsAdmin = this.configuration["Authentication:WalletPath"];
            OracleConfiguration.WalletLocation = OracleConfiguration.TnsAdmin;
        }

        public ICollection<object> RunDbRequest(string funckName, bool mustRespond=false, Tuple<string, OracleDbType, object>[] args=null, Tuple<string, OracleDbType, object> returnVal=null)
        {
            string connectionString = $"User Id={this.configuration["Authentication:Login"]};Password={this.configuration["Authentication:Password"]};Data Source={this.configuration["Authentication:Schema"]};Connection Timeout=100;";
            List<object> returnVals = null;
            using (var con = new OracleConnection(connectionString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        cmd.CommandText = funckName;
                        cmd.CommandType = CommandType.StoredProcedure;
                        foreach(Tuple<string, OracleDbType, object> arg in args)
                        {
                            cmd.Parameters.Add(arg.Item1, arg.Item2).Value = arg.Item3;
                        }
                        con.Open();
                        if (mustRespond)
                        {
                            returnVals = new List<object>();
                            cmd.Parameters.Add(returnVal.Item1, returnVal.Item2).Direction = ParameterDirection.Output;
                            OracleDataReader rdr = cmd.ExecuteReader();
                            while (rdr.Read())
                            {
                                for (int i = 0; i < rdr.FieldCount; i++)
                                {
                                    returnVals.Add(rdr[i]);
                                }
                            }
                        }
                        cmd.ExecuteNonQuery();
                    }
                    catch (OracleException ex)
                    {
                        switch (ex.Number)
                        {
                            case 1:
                                Console.WriteLine("Error attempting to insert duplicate data.");
                                break;
                            case 12545:
                                Console.WriteLine("The database is unavailable.");
                                break;
                            default:
                                Console.WriteLine("Database error: " + ex.Message.ToString());
                                break;
                        }
                        return null;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        return null;
                    }
                    finally
                    {
                        con.Close();
                    }
                }
            }
            return returnVals;
        }
    }
}
