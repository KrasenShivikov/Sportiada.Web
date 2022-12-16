namespace Sportiada.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Services.Admin.Interfaces;
    using Models;
    using Services.Admin.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [Area("Admin")]
    public class AdminFootballGoalTypeController : Controller
    {
        private readonly IFootballGoalTypeAdminService types;
        public AdminFootballGoalTypeController(IFootballGoalTypeAdminService types)
        {
            this.types = types;
        }

        [Route("admin/goalTypes")]
        public IActionResult GoalTypes()
          => View(types.All());

        [Route("admin/createGoalType")]
        public IActionResult CreateGoalType()
        {
            var model = new FootballGoalTypeViewModel();
            return View(model);
        }

        [HttpPost]
        [Route("admin/createGoalType")]
        public IActionResult CreateGoalType(FootballGoalTypeViewModel model)
        {
            types.Create(model.Name, model.Picture);
            return RedirectToAction(nameof(GoalTypes));
        }

        [Route("admin/updateGoalType/{id}")]
        public IActionResult UpdateGoalType(int id)
        {
            var type = types.GetGoalType(id);
            var model = new FootballGoalTypeViewModel
            {
                Id = type.Id,
                Name = type.Name,
                Picture = type.picture
            };

            return View(model);
        }

        [HttpPost]
        [Route("admin/updateGoalType/{id}")]
        public IActionResult UpdateGoalType(FootballGoalTypeViewModel model)
        {
            types.Update(model.Id, model.Name, model.Picture);
            return RedirectToAction(nameof(GoalTypes));
        }

        [Route("admin/deleteGoalType/{id}")]
        public IActionResult DeleteGoalType(int id)
        {
            var type = types.GetGoalType(id);
            var model = new FootballGoalTypeAdminModel
            {
                Name = type.Name,
                Picture = type.picture
            };

            TempData["id"] = type.Id;
            return View(model);
        }

        [Route("admin/deleteGoalType")]
        public IActionResult DeleteGoalType()
        {
            int id = (int)TempData["id"];
            types.Delete(id);
            return RedirectToAction(nameof(GoalTypes));
        }


    }
}
