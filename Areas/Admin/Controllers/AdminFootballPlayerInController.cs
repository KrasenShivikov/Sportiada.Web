namespace Sportiada.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Services.Admin.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [Area("Admin")]
    public class AdminFootballPlayerInController : Controller
    {
        private readonly IFootballPlayerInAdminService playerIns;
        private readonly IFootballSquadPlayerAdminService squadPlayers;

        public AdminFootballPlayerInController(IFootballPlayerInAdminService playerIns, IFootballSquadPlayerAdminService squadPlayers)
        {
            this.playerIns = playerIns;
            this.squadPlayers = squadPlayers;
        }

        [Route("admin/createPlayerIn/{substituteId}/{squadId}/{gameId}")]
        public IActionResult CreatePlayerIn(int substituteId, int squadId, int gameId)
        {
            var model = new FootballPlayerInFormModel
            {
                SubstituteId = substituteId,
                Players = squadPlayers.PlayersBySquadId(squadId)
                                .Where(p => p.LeftInSummer != true && p.LeftInwinter != true && p.OnLoan != true)
            };

            TempData["gameId"] = gameId;

            return View(model);
        }

        [HttpPost]
        [Route("admin/createPlayerIn/{substituteId}/{squadId}/{gameId}")]
        public IActionResult CreatePlayerIn(FootballPlayerInFormModel model)
        {
            int gameId = (int)TempData["gameId"];

            playerIns.Create(model.PlayerId, model.SubstituteId, model.inIcon);

            return RedirectToAction("footballGame", "admin", new { id = gameId });
        }

        [Route("admin/updatePlayerIn/{id}/{squadId}/{gameId}")]
        public IActionResult UpdatePlayerIn(int id, int squadId, int gameId)
        {
            var playerIn = playerIns.GetPlayerIn(id);

            var model = new FootballPlayerInFormModel
            {
                Id = playerIn.Id,
                SubstituteId = playerIn.SubstituteId,
                PlayerId = playerIn.PlayerId,
                inIcon = playerIn.InIcon,
                Players = squadPlayers.PlayersBySquadId(squadId)
                                .Where(p => p.LeftInSummer != true && p.LeftInwinter != true && p.OnLoan != true),

            };

            TempData["gameId"] = gameId;

            return View(model);
        }

        [HttpPost]
        [Route("admin/updatePlayerIn/{id}/{squadId}/{gameId}")]
        public IActionResult UpdatePlayerIn(FootballPlayerInFormModel model)
        {
            int gameId = (int)TempData["gameId"];

            playerIns.Update(model.Id, model.PlayerId, model.SubstituteId, model.inIcon);

            return RedirectToAction("footballGame", "admin", new { id = gameId });
        }

        [Route("admin/deletePlayerIn/{id}/{gameId}")]
        public IActionResult DeletePlayerIn(int id, int gameId)
        {
            var playerIn = playerIns.ById(id);

            var model = new FootballPlayerInDeleteModel
            {
                PlayerName = playerIn.Player.ProfileName,
                PlayerCountry = playerIn.Player.Picture,
                InIcon = playerIn.InIcon,
                GameId = gameId
            };

            TempData["id"] = id;
            TempData["gameId"] = gameId;

            return View(model);
        }

        [Route("admin/deletePlayerIn")]
        public IActionResult DeletePlayerIn()
        {
            int gameId = (int)TempData["gameId"];
            int id = (int)TempData["id"];

            playerIns.Delete(id);
            return RedirectToAction("footballGame", "admin", new { id = gameId });
        }
    }
}

