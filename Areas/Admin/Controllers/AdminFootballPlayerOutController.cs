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
    public class AdminFootballPlayerOutController : Controller
    {
        private readonly IFootballPlayerOutAdminService playerOuts;
        private readonly IFootballSquadPlayerAdminService squadPlayers;

        public AdminFootballPlayerOutController(IFootballPlayerOutAdminService playerOuts, IFootballSquadPlayerAdminService squadPlayers)
        {
            this.playerOuts = playerOuts;
            this.squadPlayers = squadPlayers;
        }

        [Route("admin/createPlayerOut/{substituteId}/{squadId}/{gameId}")]
        public IActionResult CreatePlayerOut(int substituteId, int squadId, int gameId)
        {
            var model = new FootballPlayerOutFormModel
            {
                SubstituteId = substituteId,
                Players = squadPlayers.PlayersBySquadId(squadId)
                                .Where(p => p.LeftInSummer != true && p.LeftInwinter != true && p.OnLoan != true)
            };

            TempData["gameId"] = gameId;

            return View(model);
        }

        [HttpPost]
        [Route("admin/createPlayerOut/{substituteId}/{squadId}/{gameId}")]
        public IActionResult CreatePlayerOut(FootballPlayerOutFormModel model)
        {
            int gameId = (int)TempData["gameId"];

            playerOuts.Create(model.PlayerId, model.SubstituteId, model.OutIcon);

            return RedirectToAction("footballGame", "admin", new { id = gameId });
        }

        [Route("admin/updatePlayerOut/{id}/{squadId}/{gameId}")]
        public IActionResult UpdatePlayerOut(int id, int squadId, int gameId)
        {
            var playerOut = playerOuts.GetPlayerOut(id);

            var model = new FootballPlayerOutFormModel
            {
                Id = playerOut.Id,
                SubstituteId = playerOut.SubstituteId,
                PlayerId = playerOut.PlayerId,
                OutIcon = playerOut.OutIcon,
                Players = squadPlayers.PlayersBySquadId(squadId)
                                .Where(p => p.LeftInSummer != true && p.LeftInwinter != true && p.OnLoan != true),

            };

            TempData["gameId"] = gameId;

            return View(model);
        }

        [HttpPost]
        [Route("admin/updatePlayerOut/{id}/{squadId}/{gameId}")]
        public IActionResult UpdatePlayerOut(FootballPlayerOutFormModel model)
        {
            int gameId = (int)TempData["gameId"];

            playerOuts.Update(model.Id, model.PlayerId, model.SubstituteId, model.OutIcon);

            return RedirectToAction("footballGame", "admin", new { id = gameId });
        }

        [Route("admin/deletePlayerOut/{id}/{gameId}")]
        public IActionResult DeletePlayerOut(int id, int gameId)
        {
            var playerOut = playerOuts.ById(id);

            var model = new FootballPlayerOutDeleteModel
            {
                PlayerName = playerOut.Player.ProfileName,
                PlayerCountry = playerOut.Player.Picture,
                OutIcon = playerOut.OutIcon,
                GameId = gameId
            };

            TempData["id"] = id;
            TempData["gameId"] = gameId;

            return View(model);
        }

        [Route("admin/deletePlayerOut")]
        public IActionResult DeletePlayerOut()
        {
            int gameId = (int)TempData["gameId"];
            int id = (int)TempData["id"];

            playerOuts.Delete(id);
            return RedirectToAction("footballGame", "admin", new { id = gameId });
        }
    }
}
