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
    public class AdminFootballSubstituteController : Controller
    {
        private readonly IFootballSubstituteAdminService substitutes;

        public AdminFootballSubstituteController(IFootballSubstituteAdminService substitutes)
        {
            this.substitutes = substitutes;
        }

        [Route("admin/createSubstitute/{gameStatisticId}/{gameId}")]
        public IActionResult CreateSubstitute(int gameStatisticId, int gameId)
        {
            var model = new FootballSubstituteFormModel
            {
                GameStatisticId = gameStatisticId,
            };

            TempData["gameId"] = gameId;

            return View(model);
        }

        [HttpPost]
        [Route("admin/createSubstitute/{gameStatisticId}/{gameId}")]
        public IActionResult CreateSubstitute(FootballSubstituteFormModel model)
        {
            int gameId = (int)TempData["gameId"];

            substitutes.Create(model.GameStatisticId, model.PlayerInId, model.PlayerOutId, model.Minute, model.FirstHalf);

            return RedirectToAction("footballGame", "admin", new { id = gameId });
        }

        [Route("admin/updateSubstitute/{id}/{gameStatisticId}/{gameId}")]
        public IActionResult UpdateSubstitute(int id, int gameStatisticId, int gameId)
        {
            var substitute = substitutes.GetSubstitute(id);

            var model = new FootballSubstituteFormModel
            {
                Id = substitute.Id,
                GameStatisticId = substitute.Id,
                FirstHalf = substitute.firstHalf,
                Minute = substitute.Minute,
                PlayerInId = substitute.PlayerInId,
                PlayerOutId = substitute.PlayerOutId
            };

            TempData["gameId"] = gameId;

            return View(model);
        }

        [HttpPost]
        [Route("admin/updateSubstitute/{id}/{gameStatisticId}/{gameId}")]
        public IActionResult UpdateSubstitute(FootballSubstituteFormModel model)
        {
            int gameId = (int)TempData["gameId"];

            substitutes.Update(model.Id, model.GameStatisticId, model.PlayerInId, model.PlayerOutId, model.Minute, model.FirstHalf);

            return RedirectToAction("footballGame", "admin", new { id = gameId });
        }

        [Route("admin/deleteSubstitute/{id}/{gameId}")]
        public IActionResult DeleteSubstitute(int id, int gameId)
        {
            var substitute = substitutes.ById(id);

            var model = new FootballSubstituteDeleteModel
            {
                GameId = gameId,
                Minute = substitute.Minute,
                FirstHalf = substitute.FirstHalf            
            };

            TempData["id"] = id;
            TempData["gameId"] = gameId;

            return View(model);
        }

        [Route("admin/deleteSubstitute")]
        public IActionResult DeleteSubstitute()
        {
            int gameId = (int)TempData["gameId"];
            int id = (int)TempData["id"];

            substitutes.Delete(id);
            return RedirectToAction("footballGame", "admin", new { id = gameId });
        }
    }
}
