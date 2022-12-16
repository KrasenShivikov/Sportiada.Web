namespace Sportiada.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Services.Admin.Interfaces;
    using Sportiada.Web.Areas.Admin.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [Area("Admin")]
    public class AdminFootballTournamentController : Controller
    {
        private readonly ICountryAdminService countries;
        private readonly IFootballTournamentService tournaments;
        public AdminFootballTournamentController(IFootballTournamentService tournaments, ICountryAdminService countries)
        {
            this.tournaments = tournaments;
            this.countries = countries;
        }

        [Route("admin/footballTournaments")]
        public IActionResult Tournaments()
            => View(tournaments.All());

        [Route("admin/footballTournament/{id}")]
        public IActionResult TournamentById(int id)
        {
            var tournament = this.tournaments.ById(id);
            return View(tournament);
        }


        [Route("admin/createTournament")]
        public IActionResult CreateTournament()
        {
            var model = new FootballTournamentViewModel
            {
                Countries = countries
                       .All()
                       .Select(c => new SelectListItem
                       {
                           Text = c.Name,
                           Value = c.Id.ToString()
                       })
            };

            return View(model);
        }

        [HttpPost]
        [Route("admin/createTournament")]
        public IActionResult CreateTournament(FootballTournamentViewModel model)
        {
            tournaments.CreateTournament(model.Name, model.CountryId);
            return RedirectToAction(nameof(Tournaments));
        }

        [Route("admin/updateTournament/{id}")]
        public IActionResult UpdateTournament(int id)
        {
            var tournament = tournaments.ById(id);
            var countries = this.countries.All().ToList();
            var country = countries.Where(c => c.Id == tournament.Country.Id).FirstOrDefault();
            countries.Remove(country);
            var orderedCountries = countries
                    .OrderBy(c => c.Name)
                    .Prepend(country);


            var model = new FootballTournamentViewModel
            {
                Id = tournament.Id,
                Name = tournament.Name,
                Countries = orderedCountries
                        .Select(c => new SelectListItem
                        {
                            Text = c.Name,
                            Value = c.Id.ToString()
                        })
            };

            return View(model);
        }

        [HttpPost]
        [Route("admin/updateTournament/{id}")]
        public IActionResult UpdateTournament(FootballTournamentViewModel model)
        {
            tournaments.UpdateTournament(model.Id, model.Name, model.CountryId);
            return RedirectToAction(nameof(Tournaments));
        }

        [Route("admin/deleteTorurnament/{id}")]
        public IActionResult DeleteTournament(int id)
        {
            var tournament = tournaments.ForDelete(id);
            TempData["id"] = tournament.Id;
            return View(tournament);
        }

        [Route("admin/deleteTournament")]
        public IActionResult FinalizeDelete()
        {
            int id = (int)TempData["id"];
            tournaments.DeleteTournament(id);
            return RedirectToAction(nameof(Tournaments));
        }
    }
}
