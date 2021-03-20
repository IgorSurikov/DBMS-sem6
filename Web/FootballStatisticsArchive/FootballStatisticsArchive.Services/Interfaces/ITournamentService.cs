using FootballStatisticsArchive.Database.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FootballStatisticsArchive.Services.Interfaces
{
    public interface ITournamentService
    {
        public ICollection<Tournament> GetTournaments();
    }
}
