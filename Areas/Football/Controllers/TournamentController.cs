namespace Sportiada.Web.Areas.Football.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Services.Football.Interfaces;
    using Services.Football.Models.Tournament;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [Area("Football")]
    public class TournamentController : Controller
    {
        private readonly IGameService games;
        public TournamentController(IGameService games)
        {
            this.games = games;
        }

        [Route("football/tournament/{tournamentId}/{seasonId}")]
        public IActionResult OverallStatisticByIdBySeasonId(int tournamentId, int seasonId)
        {
            var model = new TournamentSeasonStatisticModel
            {
                Games = games.GamesByTournamentBySeason(tournamentId, seasonId)
            };

            return View(model);
        }
    }
}
