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
    public class AdminFootballRefereeTypeController : Controller
    {
        private readonly IFootballRefereeTypeAdminService types;
        public AdminFootballRefereeTypeController(IFootballRefereeTypeAdminService types)
        {
            this.types = types;
        }

        [Route("admin/refereeTypes")]
        public IActionResult RefereeTypes()
          => View(types.All());

        [Route("admin/createRefereeType")]
        public IActionResult CreateRefereeType()
          => View();

        [HttpPost]
        [Route("admin/createRefereeType")]
        public IActionResult CreateRefereeType(FootballRefereeTypeViewModel model)
        {
            types.Create(model.Name);
            return RedirectToAction(nameof(RefereeTypes));
        }

        [Route("admin/updateRefereeType/{id}")]
        public IActionResult UpdateRefereeType(int id)
        {
            var type = types.GetRefereeType(id);
            var model = new FootballRefereeTypeViewModel
            {
                Id = type.Id,
                Name = type.Name
            };

            return View(model);
        }

        [HttpPost]
        [Route("admin/updateRefereeType/{id}")]
        public IActionResult UpdateRefereeType(FootballRefereeTypeViewModel model)
        {
            types.Update(model.Id, model.Name);
            return RedirectToAction(nameof(RefereeTypes));
        }

        [Route("admin/deleteRefereeType/{id}")]
        public IActionResult DeleteRefereeType(int id)
        {
            var type = types.GetRefereeType(id);
            var model = new FootballRefereeTypeAdminModel
            {
                Name = type.Name
            };

            TempData["id"] = type.Id;
            return View(model);
        }

        [Route("admin/deleteRefereeType")]
        public IActionResult DeleteRefereeType()
        {
            int id = (int)TempData["id"];
            types.Delete(id);
            return RedirectToAction(nameof(RefereeTypes));
        }
    }
}
