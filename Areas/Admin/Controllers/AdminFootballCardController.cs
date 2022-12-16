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
    public class AdminFootballCardController : Controller
    {
        private readonly IFootballCardAdminService cards;
        private readonly IFootballSquadPlayerAdminService squadPlayers;
        private readonly IFootballCardTypeAdminService cardTypes;
        public AdminFootballCardController(IFootballCardAdminService cards, IFootballSquadPlayerAdminService squadPlayers, IFootballCardTypeAdminService cardTypes)
        {
            this.cards = cards;
            this.squadPlayers = squadPlayers;
            this.cardTypes = cardTypes;
        }

        [Route("admin/createCard/{squadId}/{gameStatisticId}/{gameId}")]
        public IActionResult CreateCard(int squadId, int gameStatisticId, int gameId)
        {
            var model = new FootballCardFormModel
            {
                GameStatisticId = gameStatisticId,
                Players = squadPlayers.PlayersBySquadId(squadId)
                                .Where(p => p.LeftInSummer != true && p.LeftInwinter != true && p.OnLoan != true),
                CardTypes = cardTypes
                  .CardTypes()
                  .Select(t => new SelectListItem
                  {
                      Text = t.Name,
                      Value = t.Id.ToString()
                  }),
            };

            TempData["gameId"] = gameId;

            return View(model);
        }

        [HttpPost]
        [Route("admin/createCard/{squadId}/{gameStatisticId}/{gameId}")]
        public IActionResult CreateCard(FootballCardFormModel model)
        {
            int gameId = (int)TempData["gameId"];

            cards.Create(model.TypeId, model.PlayerId, model.GameStatisticId, model.Minute, model.FirstHalf);

            return RedirectToAction("footballGame", "admin", new { id = gameId });
        }

        [Route("admin/updateCard/{id}/{squadId}/{gameId}")]
        public IActionResult UpdateCard(int id, int squadId, int gameId)
        {
            var card = cards.GetCard(id);

            var model = new FootballCardFormModel
            {
                Id = card.Id,
                GameStatisticId = card.GameStatisticId,
                PlayerId = card.PlayerId,
                Players = squadPlayers.PlayersBySquadId(squadId)
                                .Where(p => p.LeftInSummer != true && p.LeftInwinter != true && p.OnLoan != true),

                CardTypes = cardTypes
                  .CardTypes()
                  .Select(t => new SelectListItem
                  {
                      Text = t.Name,
                      Value = t.Id.ToString()
                  }),
                TypeId = card.TypeId,
                Minute = card.Minute,
                FirstHalf = card.FirstHalf
            };

            TempData["gameId"] = gameId;

            return View(model);
        }

        [HttpPost]
        [Route("admin/updateCard/{id}/{squadId}/{gameId}")]
        public IActionResult UpdateCard(FootballCardFormModel model)
        {
            int gameId = (int)TempData["gameId"];

            cards.Update(model.Id, model.TypeId, model.PlayerId, model.GameStatisticId, model.Minute, model.FirstHalf);

            return RedirectToAction("footballGame", "admin", new { id = gameId });
        }

        [Route("admin/deleteCard/{id}/{gameId}")]
        public IActionResult DeleteCard(int id, int gameId)
        {
            var card = cards.ById(id);

            var model = new FootballCardDeleteModel
            {
                Id = card.Id,
                GameId = gameId,
                PlayerName = card.Player.ProfileName,
                Country = card.Player.Country,
                TypeName = card.Type.Name,
                TypePicture = card.Type.Picture,
                Minute = card.Minute,
                FirstHalf = card.FirstHalf
            };

            TempData["id"] = id;
            TempData["gameId"] = gameId;

            return View(model);
        }

        [Route("admin/deleteCard")]
        public IActionResult DeleteCard()
        {
            int gameId = (int)TempData["gameId"];
            int id = (int)TempData["id"];

            cards.Delete(id);
            return RedirectToAction("footballGame", "admin", new { id = gameId });
        }
    }
}
