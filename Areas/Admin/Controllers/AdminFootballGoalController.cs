namespace Sportiada.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Models;
    using Services.Admin.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;


    [Area("Admin")]
    public class AdminFootballGoalController : Controller
    {
        private readonly IFootballGoalAdminService goals;
        private readonly IFootballSquadPlayerAdminService squadPlayers;
        private readonly IFootballGoalTypeAdminService goalTypes;
        public AdminFootballGoalController(IFootballGoalAdminService goals, IFootballSquadPlayerAdminService squadPlayers, IFootballGoalTypeAdminService goalTypes)
        {
            this.goals = goals;
            this.squadPlayers = squadPlayers;
            this.goalTypes = goalTypes;
        }

        [Route("admin/createGoal/{squadId}/{gameStatisticId}/{gameId}")]
        public IActionResult CreateGoal(int squadId, int gameStatisticId, int gameId)
        {
            var model = new FootballGoalFormModel
            {
                GameStatisticId = gameStatisticId,
                Players = squadPlayers.PlayersBySquadId(squadId)
                                .Where(p => p.LeftInSummer != true && p.LeftInwinter != true && p.OnLoan != true),
                GoalTypes = goalTypes
                  .All()
                  .Select(t => new SelectListItem
                  {
                      Text = t.Name,
                      Value = t.Id.ToString()
                  })
            };

            TempData["gameId"] = gameId;

            return View(model);
        }

        [HttpPost]
        [Route("admin/createGoal/{squadId}/{gameStatisticId}/{gameId}")]
        public IActionResult CreateGoal(FootballGoalFormModel model)
        {
            int gameId = (int)TempData["gameId"];

            goals.Create(model.PlayerId, model.GameStatisticId, model.AssistanceId, model.TypeId, model.Minute, model.FirstHalf);

            return RedirectToAction("footballGame", "admin", new { id = gameId });
        }

        [Route("admin/updateGoal/{id}/{squadId}/{gameId}")]
        public IActionResult UpdateGoal(int id, int squadId, int gameId)
        {
            var goal = goals.GetGoal(id);

            var model = new FootballGoalFormModel
            {
                Id = goal.Id,
                GameStatisticId = goal.GameStatisticId,
                PlayerId = goal.PlayerId,
                Players = squadPlayers.PlayersBySquadId(squadId)
                                .Where(p => p.LeftInSummer != true && p.LeftInwinter != true && p.OnLoan != true),

                GoalTypes = goalTypes
                  .All()
                  .Select(t => new SelectListItem
                  {
                      Text = t.Name,
                      Value = t.Id.ToString()
                  }),
                AssistanceId = goal.AssistanceId,
                TypeId = goal.TypeId,
                Minute = goal.Minute,
                FirstHalf = goal.FirstHalf
            };

            TempData["gameId"] = gameId;

            return View(model);
        }

        [HttpPost]
        [Route("admin/updateGoal/{id}/{squadId}/{gameId}")]
        public IActionResult UpdateGoal(FootballGoalFormModel model)
        {
            int gameId = (int)TempData["gameId"];

            goals.Update(model.Id, model.PlayerId, model.GameStatisticId, model.AssistanceId, model.TypeId, model.Minute, model.FirstHalf);

            return RedirectToAction("footballGame", "admin", new { id = gameId });
        }

        [Route("admin/deleteGoal/{id}/{gameId}")]
        public IActionResult DeleteGoal(int id, int gameId)
        {
            var goal = goals.ById(id);

            var model = new FootballGoalDeleteModel
            {
                Id = goal.Id,
                GameId = gameId,
                PlayerName = goal.Player.ProfileName,
                Country = goal.Player.Country,
                TypeName = goal.Type.Name,
                TypePicture = goal.Type.Picture,
                Minute = goal.Minute,
                FirstHalf = goal.FirstHalf
            };

            TempData["id"] = id;
            TempData["gameId"] = gameId;

            return View(model);
        }

        [Route("admin/deleteGoal")]
        public IActionResult DeleteGoal()
        {
            int gameId = (int)TempData["gameId"];
            int id = (int)TempData["id"];

            goals.Delete(id);
            return RedirectToAction("footballGame", "admin", new { id = gameId });
        }
    }
}
