namespace Sportiada.Web.Areas.Admin.Controllers
{
    using Services.Admin.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Sportiada.Services.Admin.Models;

    [Area("admin")]
    public class AdminFootballRefereeController : Controller
    {

        private readonly IFootballRefereeAdminService referees;
        private readonly ICountryAdminService countries;
        public AdminFootballRefereeController(IFootballRefereeAdminService referees, ICountryAdminService countries)
        {
            this.referees = referees;
            this.countries = countries;
        }


        [Route("admin/footballReferees")]
        public IActionResult FootballReferees()
           => View(referees.All());

        [Route("admin/createFootballReferee")]
        public IActionResult CreateFootballReferee()
        {
            var countryModel = countries.All();

            var model = new FootballRefereeViewModel
            {
                Countries = countryModel
                            .Select(c => new SelectListItem
                            {
                                Text = c.Name,
                                Value = c.Id.ToString()
                            })
            };

            return View(model);
        }

        [HttpPost]
        [Route("admin/createFootballReferee")]
        public IActionResult CreateFootballReferee(FootballRefereeViewModel model)
        {
            referees.Create(model.Name, model.BirthDate, model.CountryId, model.Picture);
            return RedirectToAction(nameof(FootballReferees));
        }

        [Route("admin/updateFootballReferee/{id}")]
        public IActionResult UpdateFootballReferee(int id)
        {
            var referee = referees.GetReferee(id);
            var countryModel = countries.All();

            var model = new FootballRefereeViewModel
            {
                Countries = countryModel
                           .Select(c => new SelectListItem
                           {
                               Text = c.Name,
                               Value = c.Id.ToString()
                           })
            };

            model.Id = referee.Id;
            model.Name = referee.Name;
            model.BirthDate = referee.BirthDate;
            model.CountryId = referee.CountryId;
            model.Picture = referee.Picture;

            return View(model);
        }

        [HttpPost]
        [Route("admin/updateFootballReferee/{id}")]
        public IActionResult UpdateFootballReferee(FootballRefereeViewModel model)
        {
            referees.Update(model.Id, model.Name, model.BirthDate, model.CountryId, model.Picture);
            return RedirectToAction(nameof(FootballReferees));
        }

        [Route("admin/deletefootballReferee/{id}")]
        public IActionResult DeleteFootballReferee(int id)
        {
            var referee = referees.GetReferee(id);

            var model = new FootballRefereeAdminModel
            {
                Name = referee.Name,
                Picture = referee.Picture
            };

            TempData["Id"] = referee.Id;

            return View(model);
        }

        [Route("admin/deletefootballReferee")]
        public IActionResult DeleteFootballReferee()
        {
            int id = (int)TempData["Id"];
            referees.Delete(id);
            return RedirectToAction(nameof(FootballReferees));
        }

    }
}
