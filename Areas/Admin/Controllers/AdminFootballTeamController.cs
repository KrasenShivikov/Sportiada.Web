namespace Sportiada.Web.Areas.Admin.Controllers
{
    using Areas.Admin.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Services.Admin.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [Area("Admin")]
    public class AdminFootballTeamController : Controller
    {
        private readonly IFootballTeamAdminService teams;
        private readonly ICityAdminService cities;
        public AdminFootballTeamController(IFootballTeamAdminService teams, ICityAdminService cities)
        {
            this.teams = teams;
            this.cities = cities;
        }

        [Route("admin/footballTeams")]
        public IActionResult FootballTeams()
          => View(this.teams.All());


        [Route("admin/footballTeam/{id}")]
        public IActionResult FootballTeamById(int id)
        {
            var model = teams.ById(id);
            return View(model);
        }

        [Route("admin/createFootballTeam")]
        public IActionResult CreateFootballTeam()
        {
            return View(new FootballTeamViewModel
            {
                Cities = cities.All()
                             .Select(c => new SelectListItem
                             {
                                 Text = c.Name,
                                 Value = c.Id.ToString()
                             })
            });
        }


        [HttpPost]
        [Route("admin/createFootballTeam")]
        public IActionResult CreateFootballTeam(FootballTeamViewModel team)
        {
            this.teams.CreateFootballTeam(team.Name, team.CityId, team.Logo);
            return RedirectToAction(nameof(FootballTeams));
        }


        [Route("admin/updateFootballTeam/{id}")]
        public IActionResult UpdateFootballTeam(int id)
        {
            var team = this.teams.ById(id);

            var model = new FootballTeamViewModel
            {
                Id = team.Id,
                Name = team.Name,
                CityId = team.City.Id,
                Logo = team.Logo,
                Cities = cities.All()
                             .Select(c => new SelectListItem
                             {
                                 Text = c.Name,
                                 Value = c.Id.ToString()
                             })
            };

            return View(model);
        }

        [HttpPost]
        [Route("admin/updateFootballTeam/{id}")]
        public IActionResult UpdateFootballTeam(FootballTeamViewModel model)
        {
            this.teams.UpdateFootballTeam(model.Id, model.CityId, model.Name, model.Logo);
            return RedirectToAction(nameof(FootballTeams));
        }

        [Route("admin/deleteFootballTeam/{id}")]
        public IActionResult DeleteFootballTeam(int id)
        {
            var team = this.teams.ById(id);
            TempData["id"] = team.Id;
            return View(team);
        }

        [Route("admin/deleteFootballTeam")]
        public IActionResult FinalizrDelete()
        {
            int id = (int)TempData["id"];
            this.teams.DeleteFootballTeam(id);
            return RedirectToAction(nameof(FootballTeams));
        }
    }
}
