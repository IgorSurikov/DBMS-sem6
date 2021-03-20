using FootballStatisticsArchive.Database.Interfaces;
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
    }
}
