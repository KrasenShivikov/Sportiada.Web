namespace Sportiada.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Services.Admin.Interfaces;
    using Services.Admin.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [Area("Admin")]
    public class AdminFootballSquadController : Controller
    {
        private readonly IFootballSquadAdminService squads;
        private readonly ISeasonAdminService seasons;
        public AdminFootballSquadController(IFootballSquadAdminService squads, ISeasonAdminService seasons)
        {
            this.squads = squads;
            this.seasons = seasons;
        }


        [Route("admin/footballSquad/{id}")]
        public IActionResult FootballSquad(int id)
        {
            var model = squads.FootballSquadById(id);
            return View(model);
        }

        [Route("admin/createFootballSquad/{teamId}")]
        public IActionResult CreateFootballSquad(int teamId)
        {
            var seasonsArray = seasons
                             .All()
                             .Select(s => new SeasonViewModel
                             {
                                 Id = s.Id,
                                 Name = s.Name
                             });

            var model = new FootballSquadFormModel
            {
                TeamId = teamId,
                Seasons = seasonsArray

            };

            return View(model);
        }

        [HttpPost]
        [Route("admin/createFootballSquad/{teamId}")]
        public IActionResult CreateFootballSquad(FootballSquadFormModel model)
        {
            squads.Create(model.SeasonId, model.TeamId);
            return RedirectToAction("footballTeam", "admin", new { id = model.TeamId });
        }

        [Route("admin/updateFootballSquad/{id}")]
        public IActionResult UpdateFootballSquad(int id)
        {
            var squad = squads.GetSquad(id);
            var model = new FootballSquadFormModel
            {
                Id = squad.Id,
                SeasonId = squad.SeasonId,
                TeamId = squad.TeamId,
                Seasons = seasons
                    .All()
                    .Select(s => new SeasonViewModel
                    {
                        Id = s.Id,
                        Name = s.Name
                    })
            };

            return View(model);
        }

        [HttpPost]
        [Route("admin/updateFootballSquad/{id}")]
        public IActionResult UpdateFootballSquad(FootballSquadFormModel model)
        {
            squads.Update(model.Id, model.SeasonId, model.TeamId);
            return RedirectToAction("footballTeam", "admin", new { id = model.TeamId });
        }

        [Route("admin/deleteFootballSquad/{id}")]
        public IActionResult DeleteFootballSquad(int id)
        {
            var squad = squads.FootballSquadById(id);

            var model = new FootballSquadDeleteModel
            {
                TeamId = squad.TeamId,
                TeamName = squad.Team.Name,
                SeasonName = squad.Season.Name
            };         

            TempData["id"] = squad.Id;
            TempData["teamId"] = squad.TeamId;

            return View(model);
        }


        [Route("admin/deleteFootballSquad")]
        public IActionResult DeleteFootballSquad()
        {
            int id = (int)TempData["id"];
            int teamId = (int)TempData["teamId"];

            squads.Delete(id);
            return RedirectToAction("footballTeam", "admin", new { id = teamId });
        }
    }
}
