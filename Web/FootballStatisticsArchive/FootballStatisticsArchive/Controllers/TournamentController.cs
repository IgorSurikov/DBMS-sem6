using FootballStatisticsArchive.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballStatisticsArchive.Web.Controllers
{
    [Route("[controller]")]
    public class TournamentController : Controller
    {
        public TournamentController(ITeamService teamService)
        {
            this.teamService = teamService;
        }

        private readonly ITeamService teamService;

        [HttpGet]
        [Route("{tournamentId}/teams")]
        public IActionResult GetTeams([FromRoute] int tournamentId)
        {
            var teams = this.teamService.GetTeams(tournamentId);
            return Ok(teams);
        }
    }
}
