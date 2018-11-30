namespace Sportiada.Web.Areas.Football.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Sportiada.Services.Football.Interfaces;

    [Area("Football")]
    public class TeamController : Controller
    {
        private readonly ITeamService teams;

        
        public TeamController(ITeamService teams)
        {
            this.teams = teams;
        }

        [Route("football/team/{seasonId}/{teamId}")]
        public IActionResult TeamSeasonstatistic(int seasonId, int teamId)
            => View(this.teams.TeamSeasonStatistic(seasonId, teamId));
    }
}
