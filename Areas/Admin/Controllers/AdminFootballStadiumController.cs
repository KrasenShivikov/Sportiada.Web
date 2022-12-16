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
    public class AdminFootballStadiumController : Controller
    {
        private readonly IFootballStadiumAdminService stadiums;
        public AdminFootballStadiumController(IFootballStadiumAdminService stadiums)
        {
            this.stadiums = stadiums;
        }

        [Route("admin/createStadium/{teamID}")]
        public IActionResult CreateFootballStadium(int teamId)
        {
            var model = new FootballStadiumFormModel
            {
                TeamId = teamId
            };

            return View(model);
        }

        [HttpPost]
        [Route("admin/createStadium/{teamID}")]
        public IActionResult CreateFootbalStadium(FootballStadiumFormModel stadium)
        {
            stadiums.Create(stadium.TeamId, stadium.Name);
            return RedirectToAction("footballTeam", "admin", new { id = stadium.TeamId });
        }

        [Route("admin/updateStadium/{id}")]
        public IActionResult UpdateFootballStadium(int id)
        {
            var stadium = stadiums.GetFootballStadium(id);

            var model = new FootballStadiumFormModel
            {
                Id = stadium.Id,
                TeamId = stadium.TeamId,
                Name = stadium.Name
            };

            return View(model);
        }


        [HttpPost]
        [Route("admin/updateStadium/{id}")]
        public IActionResult UpdateFootballStadium(FootballStadiumFormModel model)
        {
            stadiums.Update(model.Id, model.TeamId, model.Name);
            return RedirectToAction("footballTeam", "admin", new { id = model.TeamId });
        }

        [Route("admin/deleteStadium/{id}")]
        public IActionResult DeleteFootballStadium(int id)
        {
            var model = stadiums.ById(id);

            TempData["id"] = model.Id;
            TempData["teamId"] = model.TeamId;

            return View(model);
        }

        [Route("admin/deleteStadium")]
        public IActionResult DeleteFootballStadium()
        {
            int id = (int)TempData["id"];
            int teamId = (int)TempData["teamId"];

            stadiums.Delete(id);
            return RedirectToAction("footballTeam", "admin", new { id = teamId });
        }
    }
}
