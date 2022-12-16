namespace Sportiada.Web.Areas.Admin.Controllers
{
    using Models;
    using Microsoft.AspNetCore.Mvc;
    using Services.Admin.Interfaces;
    using Services.Admin.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [Area("Admin")]
    public class AdminFootballGameStatisticCoachController : Controller
    {
        private readonly IFootballGameStatisticCoachAdminService gameStatisticCoaches;
        private readonly IFootballSquadCoachAdminService squadCoaches;

        public AdminFootballGameStatisticCoachController(IFootballGameStatisticCoachAdminService gameStatisticCoaches, IFootballSquadCoachAdminService squadCoaches)
        {
            this.gameStatisticCoaches = gameStatisticCoaches;
            this.squadCoaches = squadCoaches;
        }

        [Route("admin/createGameStatisticCoach/{squadId}/{gameStatisticId}/{gameId}")]
        public IActionResult CreateGameStatisticCoach(int squadId, int gameStatisticId, int gameId)
        {
            var model = new FootballGameStatisticCoachFormModel
            {
                GameStattisticId = gameStatisticId,
                Coaches = GetCoachesBySquad(squadId)
            };

            TempData["gameId"] = gameId;

            return View(model);
        }

        [HttpPost]
        [Route("admin/createGameStatisticCoach/{squadId}/{gameStatisticId}/{gameId}")]
        public IActionResult CreateGameStatisticCoach(FootballGameStatisticCoachFormModel model)
        {
            int gameId = (int)TempData["gameId"];

            gameStatisticCoaches.Create(model.CoachId, model.GameStattisticId);

            return RedirectToAction("footballGame", "admin", new { id = gameId });
        }

        [Route("admin/updateGameStatisticCoach/{id}/{squadId}/{gameId}")]
        public IActionResult UpdateGameStatisticCoach(int id, int squadId, int gameId)
        {
            var coach = gameStatisticCoaches.GetGameStatisticCoach(id);

            var model = new FootballGameStatisticCoachFormModel
            {
                Id = coach.Id,
                GameStattisticId = coach.GameStatisticId,
                CoachId = coach.CoachId,
                Coaches = GetCoachesBySquad(squadId),
            };

            TempData["gameId"] = gameId;

            return View(model);
        }

        [HttpPost]
        [Route("admin/updateGameStatisticCoach/{id}/{squadId}/{gameId}")]
        public IActionResult UpdateGameStatisticCoach(FootballGameStatisticCoachFormModel model)
        {
            int gameId = (int)TempData["gameId"];

            gameStatisticCoaches.Update(model.Id, model.CoachId, model.GameStattisticId);

            return RedirectToAction("footballGame", "admin", new { id = gameId });
        }



        private IEnumerable<FootballCoachListModel> GetCoachesBySquad(int squadId)
        {
            var coaches = squadCoaches.CoachesBySquadId(squadId)
                                .Where(c => c.LeftInSeason != true)
                                .Select(c => new FootballCoachListModel
                                {
                                    Id = c.Coach.Id,
                                    FirstName = c.Coach.FirstName,
                                    LastName = c.Coach.LastName,
                                    ShortName = c.Coach.ShortName,
                                    CountryName = c.Coach.Country.Name,
                                    CountryPicture = c.Coach.Country.PicturePath,
                                    Picture = c.Coach.Picture
                                });

            return coaches;
        }


        [Route("admin/deleteGameStatisticCoach/{id}/{gameId}")]
        public IActionResult DeleteGameStatisticCoach(int id, int gameId)
        {
            var coach = gameStatisticCoaches.ById(id);

            var model = new FootballGameStatisticCoachDeleteModel
            {
                Id = coach.Id,
                CoachName = $"{coach.Coach.FirstName} {coach.Coach.LastName}",
                Country = coach.Coach.CountryPicture,
                GameId = gameId
            };

            TempData["id"] = id;
            TempData["gameId"] = gameId;

            return View(model);
        }

        [Route("admin/deleteGameStatisticCoach")]
        public IActionResult DeleteGameStatisticCoach()
        {
            int gameId = (int)TempData["gameId"];
            int id = (int)TempData["id"];

            gameStatisticCoaches.Delete(id);
            return RedirectToAction("footballGame", "admin", new { id = gameId });
        }
    }
}
