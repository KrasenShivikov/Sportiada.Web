namespace Sportiada.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Sportiada.Data;
    using Sportiada.Data.Models.Football;
    using Sportiada.Services.Admin.Interfaces;
    using Sportiada.Services.Admin.Models;
    using Sportiada.Web.Areas.Admin.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [Area("Admin")]
    public class AdminFootballCompetitionController : Controller
    {
        private readonly SportiadaDbContext db;
        private readonly IFootballCompetitionAdminService competitions;
        private readonly ISeasonAdminService seasons;
        private readonly IFootballTournamentService tournaments;
        private readonly ICompetitionTypeAdminService types;

        public AdminFootballCompetitionController(IFootballCompetitionAdminService competitions, ISeasonAdminService seasons,
            IFootballTournamentService tournaments, ICompetitionTypeAdminService types, SportiadaDbContext db)
        {
            this.competitions = competitions;
            this.seasons = seasons;
            this.tournaments = tournaments;
            this.types = types;
            this.db = db;
        }

        [Route("admin/footballCompetitions")]
        public IActionResult FootballCompetitionsByTournamentId(int tournamentId)
          => View(competitions.CompetitionsByTournamentId(tournamentId));

        [Route("admin/createFootballCompetition/{tournamentId}")]
        public IActionResult CreateFootballCompetition(int tournamentId)
        {
            int id = db.FootballTournamnets
                    .Where(c => c.Id == tournamentId)
                    .FirstOrDefault()
                    .Id;

            var model = GetModel(id);
            return View(model);
        }

        [HttpPost]
        [Route("admin/createFootballCompetition/{tournamentId}")]
        public IActionResult CreateFootballCompetition(FootballCompetitionAdminViewModel model)
        {
            competitions.Create(model.SeasonId, model.TournamentId, model.TypeId);
            return RedirectToAction("footballTournament", "admin", new { id = model.TournamentId });
        }


        [Route("admin/updateFootballCompetition/{id}")]
        public IActionResult UpdateFootballCompetition(int id)
        {
            var competition = db.FootballCompetitions
                               .Where(c => c.Id == id)
                               .FirstOrDefault();

            var model = GetModel(competition.TournamentId);
            model.SeasonId = competition.SeasonId;
            model.TypeId = competition.TypeId;
            return View(model);
        }

        [HttpPost]
        [Route("admin/updateFootballCompetition/{id}")]
        public IActionResult UpdateFootballCompetition(FootballCompetitionAdminViewModel model)
        {
            competitions.Update(model.Id, model.SeasonId, model.TournamentId, model.TypeId);
            return RedirectToAction("footballTournament", "admin", new { id = model.TournamentId });
        }



        [Route("admin/deleteFootballCompetition/{id}")]
        public IActionResult DeleteFootballCompetition(int id)
        {
            var competition = db.FootballCompetitions
                               .Where(c => c.Id == id)
                               .Select(c => new FootballCompetitionAdminModel
                               {
                                   TournamentId = c.TournamentId,
                                   SeasonName = c.Season.Name,
                                   TypeName = c.Type.Name
                               }).FirstOrDefault();

            TempData["id"] = id;
            TempData["tournamentId"] = competition.TournamentId;

            return View(competition);
        }

        [Route("admin/deleteFootballCompetition")]
        public IActionResult FinalizeDelete()
        {
            int id = (int)TempData["id"];
            int tournamentId = (int)TempData["TournamentId"];

            competitions.Delete(id);
            return RedirectToAction("footballTournament", "admin", new { id = tournamentId });
        }

        private FootballCompetitionAdminViewModel GetModel(int tournamentId)
        {
            var model = new FootballCompetitionAdminViewModel
            {
                TournamentId = tournamentId,
                Seasons = seasons.All().Select(s => new SelectListItem
                {
                    Text = s.Name,
                    Value = s.Id.ToString()
                }),
                Types = types.All().Select(t => new SelectListItem
                {
                    Text = t.Name,
                    Value = t.Id.ToString()
                })
            };

            return model;
        }
    }
}
