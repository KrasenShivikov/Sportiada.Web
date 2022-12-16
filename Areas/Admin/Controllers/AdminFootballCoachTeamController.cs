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
    public class AdminFootballCoachTeamController : Controller
    {
        private readonly IFootballCoachTeamAdminService coachTeams;

        public AdminFootballCoachTeamController(IFootballCoachTeamAdminService coachTeams)
        {
            this.coachTeams = coachTeams;
        }

        [Route("admin/createCoachTeam/{coachId}")]
        public IActionResult CreateCoachTeam(int coachId)
        {
            var model = new FootballCoachTeamFormModel
            {
                CoachId = coachId
            };
            return View(model);
        }

        [HttpPost]
        [Route("admin/createCoachTeam/{coachId}")]
        public IActionResult CreateCoachTeam(FootballCoachTeamFormModel model)
        {
            coachTeams.Create(model.CoachId, model.Team, model.TeamLogo, model.TeamCountryFlag, model.FromDate,
                model.UntilDate, model.Position, model.Matches);

            return RedirectToAction("footballCoach", "admin", new { id = model.CoachId });
        }

        [Route("admin/updateCoachTeam/{id}")]
        public IActionResult UpdateCoachTeam(int id)
        {
            var coachTeam = coachTeams.GetCoachTeam(id);

            var model = new FootballCoachTeamFormModel
            {
                Id = coachTeam.Id,
                CoachId = coachTeam.CoachId,
                FromDate = coachTeam.FromDate,
                UntilDate = coachTeam.UntilDate,
                Team = coachTeam.Team,
                TeamCountryFlag = coachTeam.TeamCountryFlag,
                TeamLogo = coachTeam.TeamLogo,
                Position = coachTeam.Position,
                Matches = coachTeam.Matches
            };

            return View(model);
        }

        [HttpPost]
        [Route("admin/updateCoachTeam/{id}")]
        public IActionResult UpdateCoachTeam(FootballCoachTeamFormModel model)
        {
            coachTeams.Update(model.Id, model.CoachId, model.Team, model.TeamLogo, model.TeamCountryFlag,
                model.FromDate, model.UntilDate, model.Position, model.Matches);

            return RedirectToAction("footballCoach", "admin", new { id = model.CoachId });
        }

        [Route("admin/deleteCoachTeam/{id}")]
        public IActionResult DeleteCoachTeam(int id)
        {
            var coachTeam = coachTeams.GetCoachTeam(id);

            var model = new FootballCoachTeamFormModel
            {
                Id = coachTeam.Id,
                CoachId = coachTeam.CoachId,
                Team = coachTeam.Team,
                TeamCountryFlag = coachTeam.TeamCountryFlag,
                TeamLogo = coachTeam.TeamLogo,
                FromDate = coachTeam.FromDate,
                UntilDate = coachTeam.UntilDate,
                Position = coachTeam.Position,
                Matches = coachTeam.Matches
            };

            TempData["id"] = coachTeam.Id;
            TempData["coachId"] = coachTeam.CoachId;

            return View(model);
        }

        [Route("admin/deleteCoachTeam")]
        public IActionResult DeleteCoachTeam()
        {
            int id = (int)TempData["id"];
            int coachId = (int)TempData["coachId"];

            coachTeams.Delete(id);
            return RedirectToAction("footballCoach", "admin", new { id = coachId });
        }

    }
}
