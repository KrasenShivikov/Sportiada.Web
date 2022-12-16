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
    public class AdminFootballGameController : Controller
    {
        private readonly IFootballGameAdminService games;
        public AdminFootballGameController(IFootballGameAdminService games)
        {
            this.games = games;
        }

        [Route("admin/footballGamesByRound/{roundId}")]
        public IActionResult GamesByRound(int roundId)
        {
            TempData["roundId"] = roundId;
            return View(games.GamesByRoundId(roundId));
        }

        [Route("admin/footballGame/{id}")]
        public IActionResult FootballGame(int id)
           => View(games.ById(id));

        [Route("admin/createFootballGame")]
        public IActionResult CreateFootballGame()
        {
            int roundId = (int)TempData["roundId"];
            var model = new FootballGameFormModel
            {
                RoundId = roundId
            };

            return View(model);
        }

        [HttpPost]
        [Route("admin/createFootballGame")]
        public IActionResult CreateFootballGame(FootballGameFormModel model)
        {
            games.Create(model.Attendance, model.Date, model.RoundId);
            return RedirectToAction("footballGamesByRound", "admin", new { id = model.RoundId});
        }

        [Route("admin/updateFootballGame/{id}")]
        public IActionResult UpdateFootballGame(int id)
        {
            var game = games.GetGame(id);

            var model = new FootballGameFormModel
            {
                Id = game.Id,
                Date = game.Date,
                Attendance = game.Attendance,
                RoundId = game.RoundId
            };

            return View(model);
        }

        [HttpPost]
        [Route("admin/updateFootballGame/{id}")]
        public IActionResult UpdateFootballGame(FootballGameFormModel model)
        {
            games.Update(model.Id, model.Attendance, model.Date, model.RoundId);

            return RedirectToAction("footballGamesByRound", "admin", new { id = model.RoundId });
        }


        [Route("admin/deleteFootballGame/{id}")]
        public IActionResult DeleteFootballGame(int id)
        {
            var game = games.GetGame(id);

            var model = new FootballGameFormModel
            {
                Id = game.Id,
                Date = game.Date,
                Attendance = game.Attendance,
                RoundId = game.RoundId
            };

            TempData["id"] = game.Id;
            TempData["roundId"] = game.RoundId;

            return View(model);
        }

        [Route("admin/deleteFootballGame")]
        public IActionResult DeleteFootballGame()
        {
            int id = (int)TempData["id"];
            int roundId = (int)TempData["roundId"];

            games.Delete(id);

            return RedirectToAction("footballGamesByRound", "admin", new { id = roundId });
        }
    }
}
