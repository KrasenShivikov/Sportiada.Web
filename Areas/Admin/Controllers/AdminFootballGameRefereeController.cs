namespace Sportiada.Web.Areas.Admin.Controllers
{
    using Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Services.Admin.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [Area("Admin")]
    public class AdminFootballGameRefereeController : Controller
    {
        private readonly IFootballGameRefereeAdminService gameReferees;
        private readonly IFootballRefereeAdminService referees;
        private readonly IFootballRefereeTypeAdminService refereeTypes;

        public AdminFootballGameRefereeController(IFootballGameRefereeAdminService gameReferees, IFootballRefereeAdminService referees,
             IFootballRefereeTypeAdminService refereeTypes)
        {
            this.gameReferees = gameReferees;
            this.referees = referees;
            this.refereeTypes = refereeTypes;
        }

        [Route("admin/createGameReferee/{gameid}")]
        public IActionResult CreateGameReferee(int gameId)
        {
            var model = new FootballGameRefereeFormModel
            {
                GameId = gameId,
                Referees = referees.All(),
                RefereeTypes = refereeTypes.All()
                   .Select(t => new SelectListItem
                   {
                       Text = t.Name,
                       Value = t.Id.ToString()
                   })
            };

            return View(model);
        }

        [HttpPost]
        [Route("admin/createGameReferee/{gameId}")]
        public IActionResult CreateGameReferee(FootballGameRefereeFormModel model)
        {
            gameReferees.Create(model.GameId, model.RefereeId, model.TypeId);

            return RedirectToAction("footballGame", "admin", new { id = model.GameId });
        }

        [Route("admin/updateGameReferee/{id}")]
        public IActionResult UpdateGameReferee(int id)
        {
            var gameReferee = gameReferees.GetGameReferee(id);

            var model = new FootballGameRefereeFormModel
            {
                Id = gameReferee.Id,
                GameId = gameReferee.GameId,
                RefereeId = gameReferee.RefereeId,
                TypeId = gameReferee.TypeId,
                RefereeTypes = refereeTypes.All()
                   .Select(t => new SelectListItem 
                   {
                       Text = t.Name,
                       Value = t.Id.ToString()
                   }),
                Referees = referees.All()
            };

            return View(model);
        }

        [HttpPost]
        [Route("admin/updateGameReferee/{id}")]
        public IActionResult UpdateGameReferee(FootballGameRefereeFormModel model)
        {
            gameReferees.Update(model.Id, model.GameId, model.RefereeId, model.TypeId);

            return RedirectToAction("footballGame", "admin", new { id = model.GameId });
        }

        [Route("admin/deleteGameReferee/{id}")]
        public IActionResult DeleteGameReferee(int id)
        {
            var model = gameReferees.ById(id);
            TempData["id"] = id;
            TempData["gameId"] = model.GameId;

            return View(model);
        }

        [Route("admin/deleteGameReferee")]
        public IActionResult DeleteGameReferee()
        {
            int id = (int)TempData["id"];
            int gameId = (int)TempData["gameId"];
            gameReferees.Delete(id);

            return RedirectToAction("footballGame", "admin", new { id = gameId });
        }

    }
}
