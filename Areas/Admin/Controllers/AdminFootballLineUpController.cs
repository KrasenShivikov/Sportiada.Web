namespace Sportiada.Web.Areas.Admin.Controllers
{
    using Services.Admin.Interfaces;
    using Models;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [Area("Admin")]
    public class AdminFootballLineUpController : Controller
    {
        private readonly IFootballLineUpAdminService lineUps;
        private readonly IFootballSquadPlayerAdminService squadPlayers;
        public AdminFootballLineUpController(IFootballLineUpAdminService lineUps, IFootballSquadPlayerAdminService squadPlayers)
        {
            this.lineUps = lineUps;
            this.squadPlayers = squadPlayers;
        }

        [Route("admin/createLineUp/{squadId}/{gameStatisticId}/{gameId}")]
        public IActionResult CreateLineUp(int squadId, int gameStatisticId, int gameId)
        {
            var model = new FootballLineUpFormModel
            {
                GameStattisticId = gameStatisticId,
                Players = squadPlayers.PlayersBySquadId(squadId)
                                .Where(p => p.LeftInSummer != true && p.LeftInwinter != true && p.OnLoan != true)
            };

            TempData["gameId"] = gameId;

            return View(model);
        }

        [HttpPost]
        [Route("admin/createLineUp/{squadId}/{gameStatisticId}/{gameId}")]
        public IActionResult CreateLineUp(FootballLineUpFormModel model)
        {
            int gameId = (int)TempData["gameId"];

            lineUps.Create(model.PlayerId, model.GameStattisticId);

            return RedirectToAction("footballGame", "admin", new { id = gameId });
        }

        [Route("admin/updateLineUp/{id}/{squadId}/{gameId}")]
        public IActionResult UpdatelineUp(int id, int squadId, int gameId)
        {
            var lineUp = lineUps.GetLineUp(id);

            var model = new FootballLineUpFormModel
            {
                Id = lineUp.Id,
                GameStattisticId = lineUp.GameStattisticId,
                PlayerId = lineUp.PlayerId,
                Players = squadPlayers.PlayersBySquadId(squadId)
                                .Where(p => p.LeftInSummer != true && p.LeftInwinter != true && p.OnLoan != true),

            };

            TempData["gameId"] = gameId;

            return View(model);
        }

        [HttpPost]
        [Route("admin/updateLineUp/{id}/{squadId}/{gameId}")]
        public IActionResult UpdateLineUp(FootballLineUpFormModel model)
        {
            int gameId = (int)TempData["gameId"];

            lineUps.Update(model.Id, model.PlayerId, model.GameStattisticId);

            return RedirectToAction("footballGame", "admin", new { id = gameId });
        }


        [Route("admin/deleteLineUp/{id}/{gameId}")]
        public IActionResult DeleteLineUp(int id, int gameId)
        {
            var lineUp = lineUps.LineUpById(id);

            var model = new FootballLineUpDeleteModel
            {
                Id = lineUp.Id,
                GameId = gameId,
                PlayerName = lineUp.Player.ProfileName,
                Country = lineUp.Player.Country
            };

            TempData["id"] = id;
            TempData["gameId"] = gameId;

            return View(model);
        }

        [Route("admin/deleteLineUp")]
        public IActionResult DeleteLineUp()
        {
            int gameId = (int)TempData["gameId"];
            int id = (int)TempData["id"];

            lineUps.Delete(id);
            return RedirectToAction("footballGame", "admin", new { id = gameId });
        }
    }
}
