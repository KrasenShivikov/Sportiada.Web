namespace Sportiada.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Services.Admin.Interfaces;
    using System;
    using System.Collections.Generic;


    [Area("Admin")]
    public class AdminSeasonController : Controller
    {
        private readonly ISeasonAdminService seasons;      
        public AdminSeasonController(ISeasonAdminService seasons)
        {
            this.seasons = seasons;
        }

        [Route("admin/seasons")]
        public IActionResult Seasons()
          => View(seasons.All());

        [Route("admin/createSeason")]
        public IActionResult CreateSeason()
          => View();

        [HttpPost]
        [Route("admin/createSeason")]
        public IActionResult CreateSeason(SeasonViewModel model)
        {
            seasons.Create(model.Name, model.Start, model.End);
            return RedirectToAction(nameof(Seasons));
        }

        [Route("admin/updateSeason/{id}")]
        public IActionResult UpdateSeason(int id)
        {
            var season = this.seasons.ById(id);

            return View(new SeasonViewModel
            {
                Id = season.Id,
                Name = season.Name,
                Start = season.Start,
                End = season.End
            });
        }

        [HttpPost]
        [Route("admin/updateSeason/{id}")]
        public IActionResult UpdateSeason(SeasonViewModel model)
        {
            this.seasons.Update(model.Id, model.Name, model.Start, model.End);
            return RedirectToAction(nameof(Seasons));
        }


        [Route("admin/deleteSeason/{id}")]
        public IActionResult DeleteSeason(int id)
        {
            var season = this.seasons.ById(id);
            TempData["id"] = season.Id;
            return View(season);
        }

        [Route("admin/deleteSeason")]
        public IActionResult FinalizeDelete()
        {
            int id = (int)TempData["id"];
            this.seasons.Delete(id);
            return RedirectToAction(nameof(Seasons));
        }

    }
}
