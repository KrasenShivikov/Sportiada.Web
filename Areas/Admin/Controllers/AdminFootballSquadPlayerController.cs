namespace Sportiada.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Services.Admin.Interfaces;
    using Services.Admin.Models;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [Area("Admin")]
    public class AdminFootballSquadPlayerController : Controller
    {
        private readonly IFootballSquadPlayerAdminService squadPlayers;
        private readonly IFootballPlayerAdminService players;
        private readonly ISeasonAdminService seasons;
        private readonly IFootballSquadAdminService squads;
        public AdminFootballSquadPlayerController(IFootballSquadPlayerAdminService squadPlayers, IFootballPlayerAdminService players, 
            ISeasonAdminService seasons, IFootballSquadAdminService squads)
        {
            this.squadPlayers = squadPlayers;
            this.players = players;
            this.seasons = seasons;
            this.squads = squads;
        }


        [Route("/admin/createBulkSquadPlayers/{seasonId}/{teamId}/{squadId}")]
        public IActionResult CreateBulkSquadPlayers(int seasonId, int teamId, int squadId)
        {
            int previousSeasonId = seasons.GetPreviousSeasonId(seasonId);
            int previousSeasonSquadId = squads.GetSquadIdByTeamIdBySeasonsId(previousSeasonId, teamId);

            var players = squadPlayers.PlayersBySquadId(previousSeasonSquadId).ToList();
            var currentSeasonPlayers = squadPlayers.PlayersBySquadId(squadId).ToList();

            for (int i = 0; i < players.Count(); i++)
            {
                var arr = currentSeasonPlayers.Where(p => p.PlayerId == players[i].PlayerId && p.SquadId == squadId);

                if (arr.Count() == 0 && players[i].LeftInSummer == false && players[i].LeftInwinter == false)
                {
                    squadPlayers.Create(players[i].PlayerId, squadId, players[i].Number, players[i].Picture, players[i].Position, players[i].SquadType, players[i].ContractStartDate, players[i].ContractEndDate,
                                        players[i].OnLoan, players[i].JoinedInSummer, players[i].JoinedInWinter, players[i].LeftInSummer, players[i].LeftInwinter);
                }   
            }

            return RedirectToAction("FootballSquad", "admin", new { id = squadId });
        }

        [Route("admin/createSquadPlayer/{squadId}")]
        public IActionResult CreateSquadPlayer(int squadId)
        {
            var model = new FootballSquadPlayerFormModel
            {
                Players = players.All(),
                SquadId = squadId,
            };

            return View(model);
        }

        [HttpPost]
        [Route("admin/createSquadPlayer/{squadId}")]
        public IActionResult CreateSquadPlayer(FootballSquadPlayerFormModel model)
        {
            squadPlayers.Create(model.PlayerId, model.SquadId, model.Number, model.Picture, model.Position, model.SquadType,
                model.ContractStartDate, model.ContractEndDate, model.OnLoan, model.JoinedInSummer, model.JoinedInWinter,
                model.LeftInSummer, model.LeftInWinter);

            return RedirectToAction("FootballSquad", "admin", new { id = model.SquadId});
        }

        [Route("admin/updateSquadPlayer/{squadId}/{playerId}")]
        public IActionResult UpdateSquadPlayer(int squadId, int playerId)
        {
            var squadPlayer = squadPlayers.GetSquadPlayer(playerId, squadId);
            var footballPlayers = players.All();

            var model = new FootballSquadPlayerFormModel
            {
                PlayerId = squadPlayer.PlayerId,
                SquadId = squadPlayer.SquadId,
                Number = squadPlayer.Number,
                Picture = squadPlayer.Picture,
                Position = squadPlayer.Position,
                SquadType = squadPlayer.SquadType,
                ContractStartDate = squadPlayer.ContractStartDate,
                ContractEndDate = squadPlayer.ContractEndDate,
                Players = footballPlayers,
                OnLoan = squadPlayer.OnLoan,
                JoinedInSummer = squadPlayer.JoinedInSummer,
                JoinedInWinter = squadPlayer.JoinedInWinter,
                LeftInSummer = squadPlayer.LeftInSummer,
                LeftInWinter = squadPlayer.LeftInWinter
            };

            TempData["squadId"] = model.SquadId;
            TempData["playerId"] = model.PlayerId;

            return View(model);
        }

        [HttpPost]
        [Route("admin/updateSquadPlayer/{squadId}/{playerId}")]
        public IActionResult UpdateSquadPlayer(FootballSquadPlayerFormModel model)
        {
            int squadId = (int)TempData["squadId"];
            int playerId = (int)TempData["playerId"];

            squadPlayers.Delete(playerId, squadId);
            squadPlayers.Create(model.PlayerId, model.SquadId, model.Number, model.Picture, model.Position, model.SquadType,
                        model.ContractStartDate, model.ContractEndDate, model.OnLoan, model.JoinedInSummer, model.JoinedInWinter,
                        model.LeftInSummer, model.LeftInWinter);

            return RedirectToAction("FootballSquad", "admin", new { id = model.SquadId });
        }

        [Route("admin/deleteSquadPlayer/{playerId}/{squadId}")]
        public IActionResult DeleteSquadPlayer(int playerId, int squadId)
        {
            var model = squadPlayers.SquadPlayer(playerId, squadId);

            TempData["playerId"] = playerId;
            TempData["squadId"] = squadId;

            return View(model);
        }

        [Route("admin/deleteSquadPlayer")]
        public IActionResult DeleteSquadPlayer()
        {
            int playerId = (int)TempData["playerId"];
            int squadId = (int)TempData["squadId"];

            squadPlayers.Delete(playerId, squadId);
            return RedirectToAction("FootballSquad", "admin", new { id = squadId });
        }
    }
}
