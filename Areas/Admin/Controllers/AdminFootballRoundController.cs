namespace Sportiada.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Services.Admin.Interfaces;
    using Sportiada.Services.Admin.Models;
    using Sportiada.Web.Areas.Admin.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [Area("Admin")]
    public class AdminFootballRoundController : Controller
    {
        private readonly IFootballRoundAdminService rounds;
        public AdminFootballRoundController(IFootballRoundAdminService rounds)
        {
            this.rounds = rounds;
        }

        [Route("admin/roundsByCompetition/{competitionId}")]
        public IActionResult RoundsByCompetition(int competitionId)
        {
            TempData["competitionId"] = competitionId;
            return View(rounds.RoundsByCompetition(competitionId));
        }

        [Route("admin/createFootballRound")]
        public IActionResult CreateFootballRound()
        {
            int competitionId = (int)TempData["competitionId"];
            var model = new FootballRoundViewModel
            {
                CompetitionId = competitionId
            };

            return View(model);
        }

        [HttpPost]
        [Route("admin/createFootballRound")]
        public IActionResult CreateFootballRound(FootballRoundViewModel model)
        {
            rounds.Create(model.Name, model.CompetitionId);
            return RedirectToAction("roundsByCompetition", "admin", new { id = model.CompetitionId });
        }


        [Route("admin/updateFootballRound/{id}")]
        public IActionResult UpdateFootballRound(int id)
        {
            var round = rounds.GetRound(id);
            var model = new FootballRoundViewModel
            {
                Id = round.Id,
                Name = round.Name,
                CompetitionId = round.CompetitionId
            };
            return View(model);
        }

        [HttpPost]
        [Route("admin/updateFootballRound/{id}")]
        public IActionResult UpdateFootballRound(FootballRoundViewModel model)
        {
            rounds.Update(model.Id, model.Name, model.CompetitionId);
            return RedirectToAction("roundsByCompetition", "admin", new { id = model.CompetitionId });
        }


        [Route("admin/deleteFootballRound/{id}")]
        public IActionResult DeleteFootballRound(int id)
        {
            var round = rounds.GetRound(id);
            var model = new FootballRoundAdminModel
            {
                Name = round.Name,
                CompetitionId = round.CompetitionId,
            };
            TempData["id"] = round.Id;
            TempData["competitionId"] = round.CompetitionId;
            return View(model);
        }

        [Route("admin/deleteFootballRound")]
        public IActionResult DeleteFootballRound()
        {
            int id = (int)TempData["id"];
            int competitionId = (int)TempData["competitionId"];

            rounds.Delete(id);

            return RedirectToAction("roundsByCompetition", "admin", new { competitionId = competitionId});
        }


    }
}
