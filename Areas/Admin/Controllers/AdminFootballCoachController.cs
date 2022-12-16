namespace Sportiada.Web.Areas.Admin.Controllers
{
    using Services.Admin.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;


    [Area("Admin")]
    public class AdminFootballCoachController : Controller
    {
        private readonly IFootballCoachAdminService coaches;
        private readonly ICountryAdminService countries;
        private readonly IFootballCoachTeamAdminService coachTeams;
        public AdminFootballCoachController(IFootballCoachAdminService coaches, ICountryAdminService countries,
            IFootballCoachTeamAdminService coachTeams)
        {
            this.coaches = coaches;
            this.countries = countries;
            this.coachTeams = coachTeams;
        }

        [Route("admin/footballCoaches")]
        public IActionResult FootballCoaches()
        {
            var model = coaches.All()
                .Select(c => new FootballCoachListModel
                {
                    Id = c.Id,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    ShortName = c.ShortName,
                    CountryName = c.CountryName,
                    CountryPicture = c.CountryPicture,
                    Picture = c.Picture
                });

            return View(model);
        }

        [Route("admin/footballCoach/{id}")]
        public IActionResult FootballCoach(int id)
        {
            var coach = coaches.ById(id);

            var model = new FootballCoachViewModel
            {
                Id = coach.Id,
                FirstName = coach.FirstName,
                LastName = coach.LastName,
                ShortName = coach.ShortName,
                BirthDate = coach.BirthDate,
                CountryId = coach.CountryId,
                Picture = coach.Picture,
                Description = coach.Description,
                Country = new CountryViewModel
                {
                    Name = coach.Country.Name,
                    ShortName = coach.Country.ShortName,
                    PicturePath = coach.Country.PicturePath
                },
                Teams = coach.Teams.Select(t => new FootballCoachTeamViewModel 
                {
                    Id = t.Id,
                    CoachId = t.CoachId,
                    FromDate = t.FromDate,
                    UntilDate = t.UntilDate,
                    Team = t.Team,
                    TeamLogo = t.TeamLogo,
                    TeamCountryFlag = t.TeamCountryFlag,
                    Matches = t.Matches,
                    Position = t.Position
                })
            };

            return View(model);

        }

        [Route("admin/createFootballCoach")]
        public IActionResult CreateFootballCoach()
        {
            FootballCoachFormModel model = new FootballCoachFormModel
            {
                Countries = countries.All()
            };
            return View(model);
        }

        [HttpPost]
        [Route("admin/createFootballCoach")]
        public IActionResult CreateFootballCoach(FootballCoachFormModel model)
        {
            coaches.Create(model.FirstName, model.LastName, model.ShortName, model.CountryId, model.BirthDate,
                model.Picture, model.Description);

            return RedirectToAction(nameof(FootballCoaches));
        }


        [Route("admin/updateFootballCoach/{id}")]
        public IActionResult UpdateFootballCoach(int id)
        {
            var coach = coaches.GetCoach(id);

            var model = new FootballCoachFormModel
            {
                Id = coach.Id,
                FirstName = coach.FirstName,
                LastName = coach.LastName,
                ShortName = coach.ShortName,
                CountryId = coach.CountryId,
                Description = coach.Description,
                BirthDate = coach.BirthDate.Value,
                Picture = coach.Picture,
                Countries = countries.All()
            };

            return View(model);
        }

        [HttpPost]
        [Route("admin/updateFootballCoach/{id}")]
        public IActionResult UpdateFootballCoach(FootballCoachFormModel model)
        {
            coaches.Update(model.Id, model.FirstName, model.LastName, model.ShortName, model.CountryId,
                model.BirthDate, model.Picture, model.Description);

            return RedirectToAction("footballCoach", "admin", new { id = model.Id});
        }

        [Route("admin/deleteFootballcoach/{id}")]
        public IActionResult DeleteFootballCoach(int id)
        {
            var coach = coaches.ById(id);

            var model = new FootballCoachDeleteModel
            {
                Id = coach.Id,
                Name = $"{coach.FirstName} {coach.LastName}",
                CountryPicture = coach.Country.PicturePath
            };

            TempData["id"] = coach.Id;

            return View(model);
        }


        [Route("admin/deleteFootballCoach")]
        public IActionResult DeleteFootballCoach()
        {
            int id = (int)TempData["id"];

            coaches.Delete(id);

            return RedirectToAction(nameof(FootballCoaches));
        }
    }
}
