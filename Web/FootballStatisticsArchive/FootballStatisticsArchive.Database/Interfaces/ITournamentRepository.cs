using FootballStatisticsArchive.Database.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FootballStatisticsArchive.Database.Interfaces
{
    public interface ITournamentRepository
    {
        public DbOutput GetTournaments();
    }
}
