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
    public class AdminFootballReserveController : Controller
    {
        private readonly IFootballReserveAdminService reserves;
        private readonly IFootballSquadPlayerAdminService squadPlayers;
        public AdminFootballReserveController(IFootballReserveAdminService reserves, IFootballSquadPlayerAdminService squadPlayers)
        {
            this.reserves = reserves;
            this.squadPlayers = squadPlayers;
        }

        [Route("admin/createReserve/{squadId}/{gameStatisticId}/{gameId}")]
        public IActionResult CreateReserve(int squadId, int gameStatisticId, int gameId)
        {
            var model = new FootballReserveFormModel
            {
                GameStattisticId = gameStatisticId,
                Players = squadPlayers.PlayersBySquadId(squadId)
                                .Where(p => p.LeftInSummer != true && p.LeftInwinter != true && p.OnLoan != true)
            };

            TempData["gameId"] = gameId;

            return View(model);
        }

        [HttpPost]
        [Route("admin/createReserve/{squadId}/{gameStatisticId}/{gameId}")]
        public IActionResult CreateReserve(FootballReserveFormModel model)
        {
            int gameId = (int)TempData["gameId"];

            reserves.Create(model.PlayerId, model.GameStattisticId);

            return RedirectToAction("footballGame", "admin", new { id = gameId });
        }

        [Route("admin/updateReserve/{id}/{squadId}/{gameId}")]
        public IActionResult UpdateReserve(int id, int squadId, int gameId)
        {
            var lineUp = reserves.GetReserve(id);

            var model = new FootballReserveFormModel
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
        [Route("admin/updateReserve/{id}/{squadId}/{gameId}")]
        public IActionResult UpdateReserve(FootballLineUpFormModel model)
        {
            int gameId = (int)TempData["gameId"];

            reserves.Update(model.Id, model.PlayerId, model.GameStattisticId);

            return RedirectToAction("footballGame", "admin", new { id = gameId });
        }


        [Route("admin/deleteReserve/{id}/{gameId}")]
        public IActionResult DeleteReserve(int id, int gameId)
        {
            var reserve = reserves.ById(id);

            var model = new FootballReserveDeleteModel
            {
                Id = reserve.Id,
                GameId = gameId,
                PlayerName = reserve.Player.ProfileName,
                Country = reserve.Player.Country
            };

            TempData["id"] = id;
            TempData["gameId"] = gameId;

            return View(model);
        }

        [Route("admin/deleteReserve")]
        public IActionResult DeleteReserve()
        {
            int gameId = (int)TempData["gameId"];
            int id = (int)TempData["id"];

            reserves.Delete(id);
            return RedirectToAction("footballGame", "admin", new { id = gameId });
        }
    }
}
