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
    public class AdminFootballSidelineReasonController : Controller
    {

        private readonly IFootballSidelineReasonAdminService reasons;
        public AdminFootballSidelineReasonController(IFootballSidelineReasonAdminService reasons)
        {
            this.reasons = reasons;
        }

        [Route("admin/sidelineReasons")]
        public IActionResult SidelineReasons()
          => View(reasons.All());

        [Route("admin/createSidelineReason")]
        public IActionResult CreateSidelineReason()
          => View();

        [HttpPost]
        [Route("admin/createSidelineReason")]
        public IActionResult CreateSideLineReason(FootballSidelineReasonViewModel model)
        {
            reasons.Create(model.Name, model.Picture);
            return RedirectToAction(nameof(SidelineReasons));
        }


        [Route("admin/updateSidelineReason/{id}")]
        public IActionResult UpdateSidelineReason(int id)
        {
            var reason = reasons.GetReason(id);
            var model = new FootballSidelineReasonViewModel
            {
                Id = reason.Id,
                Name = reason.Name,
                Picture = reason.Picture
            };

            return View(model);
        }

        [HttpPost]
        [Route("admin/updateSidelineReason/{id}")]
        public IActionResult UpdateSidelineReason(FootballSidelineReasonViewModel model)
        {
            reasons.Update(model.Id, model.Name, model.Picture);
            return RedirectToAction(nameof(SidelineReasons));
        }

        [Route("admin/deleteSidelineReason/{id}")]
        public IActionResult DeleteSideLineReason(int id)
        {
            var reason = reasons.GetReason(id);
            var model = new FootballSidelineReasonAdminModel
            {
                Name = reason.Name,
                Picture = reason.Picture
            };

            TempData["id"] = reason.Id;

            return View(model);
        }

        [Route("admin/deleteSidelineReason")]
        public IActionResult DeleteSidelineReason()
        {
            int id = (int)TempData["id"];
            reasons.Delete(id);
            return RedirectToAction(nameof(SidelineReasons));
        }
    }
}
