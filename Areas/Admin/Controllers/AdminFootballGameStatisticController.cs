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
    public class AdminFootballGameStatisticController : Controller
    {
        private readonly IFootballGameStatisticAdminService gameStatistics;
        public readonly IFootballSquadAdminService squads;

        public AdminFootballGameStatisticController(IFootballGameStatisticAdminService gameStatistics, IFootballSquadAdminService squads)
        {
            this.gameStatistics = gameStatistics;
            this.squads = squads;
        }

        [Route("admin/createGameStatistic/{gameId}")]
        public IActionResult CreateGameStatistic(int gameId)
        {

            var model = new FootballGameStatisticFormModel
            {
                Squads = squads
                .All()
                .Select(s => new FootballSquadViewModel 
                {
                    Id = s.Id,
                    SeasonName = s.Season,
                    TeamName = s.TeamName
                }),
                GameId = gameId,
                GameStatisticTypes = gameStatistics
                .GameStatisticTypes()
                .Select(t => new SelectListItem 
                {
                    Text = t.Name,
                    Value = t.Id.ToString()
                })
            };

            return View(model);
        }

        [HttpPost]
        [Route("admin/createGameStatistic/{gameId}")]
        public IActionResult CreateGameStatistic(FootballGameStatisticFormModel model)
        {
            gameStatistics.Create(model.TypeId, model.SquadId, model.GameId, model.BallPossession, model.Corners, model.ShotsOnTarget,model.ShotsWide, model.Fouls, model.Offsides);
            return RedirectToAction("footballGame", "admin", new { id = model.GameId });
        }

        [Route("admin/updateGameStatistic/{id}")]
        public IActionResult UpdateGameStatistic(int id)
        {
            var gameStatistic = gameStatistics.GetGameStatistic(id);

            var model = new FootballGameStatisticFormModel
            {
                Id = gameStatistic.Id,
                GameId = gameStatistic.GameId,
                SquadId = gameStatistic.SquadId,
                TypeId = gameStatistic.TypeId,
                BallPossession = gameStatistic.BallPossession,
                Corners = gameStatistic.Corners,
                Fouls = gameStatistic.Fouls,
                Offsides = gameStatistic.Offsides,
                ShotsOnTarget = gameStatistic.ShotsOnTarget,
                ShotsWide = gameStatistic.ShotsWide,
                GameStatisticTypes = gameStatistics
                .GameStatisticTypes()
                .Select(t => new SelectListItem
                {
                    Text = t.Name,
                    Value = t.Id.ToString()
                }),
                Squads = squads
                .All()
                .Select(s => new FootballSquadViewModel
                {
                    Id = s.Id,
                    SeasonName = s.Season,
                    TeamName = s.TeamName
                })
            };

            return View(model);
        }

        [HttpPost]
        [Route("admin/updateGameStatistic/{id}")]
        public IActionResult UpdateGameStatistic(FootballGameStatisticFormModel model)
        {
            gameStatistics.Update(model.Id, model.TypeId, model.SquadId, model.GameId, model.BallPossession, model.Corners,
                model.ShotsOnTarget, model.ShotsWide, model.Fouls, model.Offsides);

            return RedirectToAction("footballGame", "admin", new { id = model.GameId });
        }

        [Route("admin/deleteGameStatistic/{id}")]
        public IActionResult DeleteGameStatitistic(int id)
        {
            var gameStatistic = gameStatistics.GetGameStatistic(id);
            var model = new FootballGameStatisticDeleteModel()
            {
                GameId = gameStatistic.GameId,
                BallPossession = gameStatistic.BallPossession,
                ShotsOnTarget = gameStatistic.ShotsOnTarget,
                ShotsWide = gameStatistic.ShotsWide,
                Corners = gameStatistic.Corners,
                Fouls = gameStatistic.Fouls,
                Offsides = gameStatistic.Offsides
            };

            TempData["id"] = id;
            TempData["gameId"] = gameStatistic.GameId;

            return View(model);
        }


        [Route("admin/deleteGameStatistic")]
        public IActionResult DeleteGameStatistic()
        {
            int id = (int)TempData["id"];
            int gameId = (int)TempData["gameId"];

            gameStatistics.Delete(id);

            return RedirectToAction("footballGame", "admin", new { id = gameId });
        }
    }
}
