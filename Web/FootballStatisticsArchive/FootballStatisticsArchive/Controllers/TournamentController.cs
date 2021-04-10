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
        public TournamentController(ITeamService teamService, ITournamentService tournamentService)
        {
            this.teamService = teamService;
            this.tournamentService = tournamentService;
        }

        private readonly ITeamService teamService;
        ITournamentService tournamentService;

        [HttpGet]
        [Route("")]
        public IActionResult GetTournaments()
        {
            var tournaments = this.tournamentService.GetTournaments();
            if (tournaments == null)
            {
                return BadRequest("Error!");
            }
            return Ok(tournaments);
        }

        [HttpGet]
        [Route("{tournamentId}/team")]
        public IActionResult GetTeams([FromRoute] int tournamentId)
        {
            var teams = this.teamService.GetTeams(tournamentId);
            if (teams == null)
            {
                return BadRequest("Error!");
            }
            return Ok(teams);
        }

        [HttpGet]
        [Route("{tournamentId}/match")]
        public IActionResult GetMathces([FromRoute] int tournamentId)
        {
            var match = this.tournamentService.GetMatches(tournamentId);
            if (match == null)
            {
                return BadRequest("Error!");
            }
            return Ok(match);
        }
    }
}
