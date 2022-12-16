namespace Sportiada.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Models;
    using Services.Admin.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [Area("Admin")]
    public class AdminFootballSidelineController : Controller
    {
        private readonly IFootballSidelineAdminService sidelines;
        private readonly IFootballSquadPlayerAdminService squadPlayers;
        private readonly IFootballSidelineReasonAdminService reasons;

        public AdminFootballSidelineController(IFootballSidelineAdminService sidelines, IFootballSquadPlayerAdminService squadPlayers
            ,IFootballSidelineReasonAdminService reasons)
        {
            this.sidelines = sidelines;
            this.squadPlayers = squadPlayers;
            this.reasons = reasons;
        }

        [Route("admin/createSideline/{squadId}/{gameStatisticId}/{gameId}")]
        public IActionResult CreateSideline(int squadId, int gameStatisticId, int gameId)
        {
            var model = new FootballSidelineFormModel
            {
                GameStatisticId = gameStatisticId,
                Players = squadPlayers.PlayersBySquadId(squadId)
                                .Where(p => p.LeftInSummer != true && p.LeftInwinter != true && p.OnLoan != true),
                Reasons = reasons
                  .All()
                  .Select(t => new SelectListItem
                  {
                      Text = t.Name,
                      Value = t.Id.ToString()
                  })
            };

            TempData["gameId"] = gameId;

            return View(model);
        }

        [HttpPost]
        [Route("admin/createSideline/{squadId}/{gameStatisticId}/{gameId}")]
        public IActionResult CreateSideline(FootballSidelineFormModel model)
        {
            int gameId = (int)TempData["gameId"];

            sidelines.Create(model.PlayerId, model.GameStatisticId, model.ReasonId);

            return RedirectToAction("footballGame", "admin", new { id = gameId });
        }

        [Route("admin/updateSideline/{id}/{squadId}/{gameId}")]
        public IActionResult UpdateSideline(int id, int squadId, int gameId)
        {
            var sideline = sidelines.GetSideline(id);

            var model = new FootballSidelineFormModel
            {
                Id = sideline.Id,
                GameStatisticId = sideline.GameStatisticId,
                PlayerId = sideline.PlayerId,
                Players = squadPlayers.PlayersBySquadId(squadId)
                                .Where(p => p.LeftInSummer != true && p.LeftInwinter != true && p.OnLoan != true),

                Reasons = reasons
                  .All()
                  .Select(t => new SelectListItem
                  {
                      Text = t.Name,
                      Value = t.Id.ToString()
                  })
            };

            TempData["gameId"] = gameId;

            return View(model);
        }

        [HttpPost]
        [Route("admin/updateSideline/{id}/{squadId}/{gameId}")]
        public IActionResult UpdateSideline(FootballSidelineFormModel model)
        {
            int gameId = (int)TempData["gameId"];

            sidelines.Update(model.Id, model.PlayerId, model.GameStatisticId, model.ReasonId);

            return RedirectToAction("footballGame", "admin", new { id = gameId });
        }

        [Route("admin/deleteSideline/{id}/{gameId}")]
        public IActionResult DeleteSideline(int id, int gameId)
        {
            var sideline = sidelines.ById(id);

            var model = new FootballSidelineDeleteModel
            {
                Id = sideline.Id,
                GameId = gameId,
                PlayerName = sideline.Player.ProfileName,
                Country = sideline.Player.Country,
                ReasonName = sideline.Reason.Name,
                ReasonPicture = sideline.Reason.Picture
            };

            TempData["id"] = id;
            TempData["gameId"] = gameId;

            return View(model);
        }

        [Route("admin/deleteSideline")]
        public IActionResult DeleteSideline()
        {
            int gameId = (int)TempData["gameId"];
            int id = (int)TempData["id"];

            sidelines.Delete(id);
            return RedirectToAction("footballGame", "admin", new { id = gameId });
        }
    }
}
