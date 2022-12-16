namespace Sportiada.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Services.Admin.Interfaces;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [Area("Admin")]
    public class AdminFootballSquadCoachController : Controller
    {
        private readonly IFootballSquadCoachAdminService squadCoaches;
        private readonly ISeasonAdminService seasons;
        private readonly IFootballSquadAdminService squads;
        private readonly IFootballCoachAdminService coaches;
        public AdminFootballSquadCoachController(IFootballSquadCoachAdminService squadCoaches, IFootballCoachAdminService coaches,
            ISeasonAdminService seasons, IFootballSquadAdminService squads)
        {
            this.squadCoaches = squadCoaches;
            this.coaches = coaches;
            this.seasons = seasons;
            this.squads = squads;
        }

        [Route("admin/createSquadCoach/{squadId}")]
        public IActionResult CreateSquadCoach(int squadId)
        {
            var model = new FootballSquadCoachFormModel
            {
                SquadId = squadId,
                Coaches = coaches.All()
                       .Select(c => new FootballCoachListModel 
                       {
                           Id = c.Id,
                           FirstName = c.FirstName,
                           LastName = c.LastName,
                           ShortName = c.ShortName,
                           CountryName = c.CountryName,
                           CountryPicture = c.CountryPicture,
                           Picture = c.Picture
                       })
            };

            return View(model);
        }

        [HttpPost]
        [Route("admin/createSquadCoach/{squadId}")]
        public IActionResult CreateSquadCoach(FootballSquadCoachFormModel model)
        {
            squadCoaches.Create(model.SquadId, model.CoachId, model.Position, model.SquadType, model.FromDate, model.ToDate, model.LeftInSeason);
            return RedirectToAction("footballSquad", "admin", new { id = model.SquadId });
        }

        [Route("/admin/createBulkSquadCoaches/{seasonId}/{teamId}/{squadId}")]
        public IActionResult CreateBulkSquadCoaches(int seasonId, int teamId, int squadId)
        {
            int previousSeasonId = seasons.GetPreviousSeasonId(seasonId);
            int previousSeasonSquadId = squads.GetSquadIdByTeamIdBySeasonsId(previousSeasonId, teamId);

            var coaches = squadCoaches.CoachesBySquadId(previousSeasonSquadId).ToList();
            var currentSeasonCoaches = squadCoaches.CoachesBySquadId(squadId);

            for (int i = 0; i < coaches.Count(); i++)
            {
                var arr = currentSeasonCoaches.Where(c => c.CoachId == coaches[i].CoachId && c.SquadId == squadId);

                if (arr.Count() == 0 && coaches[i].LeftInSeason == false)
                {
                    squadCoaches.Create(squadId, coaches[i].CoachId, coaches[i].Position, coaches[i].SquadType, coaches[i].FromDate, coaches[i].ToDate, coaches[i].LeftInSeason);
                }             
            }

            return RedirectToAction("FootballSquad", "admin", new { id = squadId });
        }

        [Route("admin/updateSquadCoach/{squadId}/{coachId}")]
        public IActionResult UpdateSquadCoach(int squadId, int coachId)
        {
            var squadCoach = squadCoaches.GetSquadCoach(squadId, coachId);

            var model = new FootballSquadCoachFormModel
            {
                CoachId = squadCoach.CoachId,
                SquadId = squadCoach.SquadId,
                Position = squadCoach.Position,
                SquadType = squadCoach.SquadType,
                FromDate = squadCoach.FromDate,
                ToDate = squadCoach.ToDate,
                LeftInSeason = squadCoach.LeftInSeason,
                Coaches = coaches.All()
                       .Select(c => new FootballCoachListModel
                       {
                           Id = c.Id,
                           FirstName = c.FirstName,
                           LastName = c.LastName,
                           ShortName = c.ShortName,
                           CountryName = c.CountryName,
                           CountryPicture = c.CountryPicture,
                           Picture = c.Picture
                       })
            };

            TempData["coachId"] = squadCoach.CoachId;
            TempData["squadId"] = squadCoach.SquadId;

            return View(model);
        }

        [HttpPost]
        [Route("admin/updateSquadCoach/{squadId}/{coachId}")]
        public IActionResult UpdateSquadCoach(FootballSquadCoachFormModel model)
        {
            int squadId = (int)TempData["squadId"];
            int coachId = (int)TempData["coachId"];

            squadCoaches.Delete(squadId, coachId);
            squadCoaches.Create(model.SquadId, model.CoachId, model.Position, model.SquadType, model.FromDate, model.ToDate, model.LeftInSeason);

            return RedirectToAction("footballSquad", "admin", new { id = model.SquadId });

        }


        [Route("admin/deleteSquadCoach/{squadId}/{coachId}")]
        public IActionResult DeleteSquadCoach(int squadId, int coachId)
        {
            var squadCoach = squadCoaches.BySquadIdByCoachId(squadId, coachId);

            var model = new FootballSquadCoachDeleteModel
            {
                SquadId = squadCoach.
                CoachId = squadCoach.CoachId,
                Coach = new FootballCoachListModel
                {
                    FirstName = squadCoach.Coach.FirstName,
                    LastName = squadCoach.Coach.LastName,
                    Picture = squadCoach.Coach.Picture,
                    CountryName = squadCoach.Coach.Country.Name,
                    CountryPicture = squadCoach.Coach.Country.PicturePath,
                    ShortName = squadCoach.Coach.ShortName,     
                }
            };

            TempData["squadId"] = squadId;
            TempData["coachId"] = coachId;

            return View(model);
        }

        [Route("admin/deleteSquadCoach")]
        public IActionResult DeleteSquadCoach()
        {
            int squadId = (int)TempData["squadId"];
            int coachId = (int)TempData["coachId"];

            squadCoaches.Delete(squadId, coachId);

            return RedirectToAction("footballSquad", "admin", new { id = squadId});
        }
    }
}
