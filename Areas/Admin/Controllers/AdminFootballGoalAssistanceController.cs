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
    public class AdminFootballGoalAssistanceController : Controller
    {
        private readonly IFootballGoalAssistanceAdminService assistances;
        private readonly IFootballSquadPlayerAdminService squadPlayers;

        public AdminFootballGoalAssistanceController(IFootballGoalAssistanceAdminService assistances, IFootballSquadPlayerAdminService squadPlayers)
        {
            this.assistances = assistances;
            this.squadPlayers = squadPlayers;
        }

        [Route("admin/createGoalAssistance/{goalId}/{squadId}/{gameId}")]
        public IActionResult CreateGoalAssistance(int goalId, int squadId, int gameId)
        {
            var model = new FootballGoalAssistanceFormModel
            {
                GoalId = goalId,
                Players = squadPlayers.PlayersBySquadId(squadId)
                                .Where(p => p.LeftInSummer != true && p.LeftInwinter != true && p.OnLoan != true)
            };

            TempData["gameId"] = gameId;

            return View(model);
        }

        [HttpPost]
        [Route("admin/createGoalAssistance/{goalId}/{squadId}/{gameId}")]
        public IActionResult CreateGoalAssitance(FootballGoalAssistanceFormModel model)
        {
            int gameId = (int)TempData["gameId"];

            assistances.Create(model.PlayerId, model.GoalId);

            return RedirectToAction("footballGame", "admin", new { id = gameId });
        }

        [Route("admin/updateGoalAssistance/{id}/{squadId}/{gameId}")]
        public IActionResult UpdateGoalAssistance(int id, int squadId, int gameId)
        {
            var assistance = assistances.GetAssistance(id);

            var model = new FootballGoalAssistanceFormModel
            {
                Id = assistance.Id,
                GoalId = assistance.GoalId,
                PlayerId = assistance.PlayerId,
                Players = squadPlayers.PlayersBySquadId(squadId)
                                .Where(p => p.LeftInSummer != true && p.LeftInwinter != true && p.OnLoan != true),

            };

            TempData["gameId"] = gameId;

            return View(model);
        }

        [HttpPost]
        [Route("admin/updateGoalAssistance/{id}/{squadId}/{gameId}")]
        public IActionResult UpdateGoalaAsistance(FootballGoalAssistanceFormModel model)
        {
            int gameId = (int)TempData["gameId"];

            assistances.Update(model.Id, model.PlayerId, model.GoalId) ;

            return RedirectToAction("footballGame", "admin", new { id = gameId });
        }

        [Route("admin/deleteGoalAssistance/{id}/{gameId}")]
        public IActionResult DeleteGoalAssistance(int id, int gameId)
        {
            var assistance = assistances.ById(id);

            var model = new FootballGoalAssistanceDeleteModel
            {
                Id = assistance.Id,
                GameId = gameId,
                PlayerName = assistance.Player.ProfileName,
                Country = assistance.Player.Country,
                Minute = assistance.Goal.Minute,
                FirstHalf = assistance.Goal.FirstHalf
            };

            TempData["id"] = id;
            TempData["gameId"] = gameId;

            return View(model);
        }

        [Route("admin/deleteGoalAssistance")]
        public IActionResult DeleteGoalAssistance()
        {
            int gameId = (int)TempData["gameId"];
            int id = (int)TempData["id"];

            assistances.Delete(id);
            return RedirectToAction("footballGame", "admin", new { id = gameId });
        }
    }
}
