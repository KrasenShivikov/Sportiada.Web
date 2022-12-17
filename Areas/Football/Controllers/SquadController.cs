namespace Sportiada.Web.Areas.Football.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Sportiada.Services.Football.Interfaces;
    using Services.Football.Models.Squad;

    [Area("Football")]
    public class SquadController : Controller
    {


        private readonly ISquadService squads;
        private readonly IPlayerService players;
        private readonly IGameService games;
        private readonly ICoachService coaches;

        public SquadController(ISquadService squads, IPlayerService players, IGameService games, ICoachService coaches)
        {
            this.squads = squads;
            this.players = players;
            this.games = games;
            this.coaches = coaches;
        }

        //[Route("football/squad/{id}")]
        //public IActionResult ById(int id)
        //    =>View(this.squads.ById(id));

        [Route("football/squad/{teamId}/{squadId}/{seasonId}")]
        public IActionResult SquadWithPlayersStatistic(int teamId, int squadId, int seasonId)
        {
            var model = new SquadPlayersStatisticModel
            {
                TeamId = teamId,
                Games = games.GamesBySquadBySeason(squadId, seasonId),
                Players = players.PlayersBySquad(squadId),
                Coaches = coaches.CoachesBySquad(squadId)
            };

            return View(model);
        }

        [Route("football/squadSquadstatistic/{teamId}/{squadid}/{seasonId}")]
        public IActionResult SquadOverallSeasonStatistic(int teamId, int squadId, int seasonId)
        {
            var model = new SquadSeasonStatisticModel
            {
                Games = games.GamesBySquadBySeason(squadId, seasonId),
                TeamId = teamId
            };

            return View(model);
        }

    }
}