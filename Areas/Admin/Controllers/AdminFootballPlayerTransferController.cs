

namespace Sportiada.Web.Areas.Admin.Controllers
{
    using Models;
    using Microsoft.AspNetCore.Mvc;
    using Services.Admin.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [Area("Admin")]
    public class AdminFootballPlayerTransferController : Controller
    {
        private readonly IFootballPlayerTransferAdminService transfers;
        public AdminFootballPlayerTransferController(IFootballPlayerTransferAdminService transfers)
        {
            this.transfers = transfers;
        }


        [Route("admin/createPlayerTransfer/{playerId}")]
        public IActionResult CreateTransfer(int playerId)
        {
            FootballPlayerTransferViewModel model = new FootballPlayerTransferViewModel();
            model.PlayerId = playerId;
            return View(model);
        }

        [HttpPost]
        [Route("admin/createPlayerTransfer/{playerId}")]
        public IActionResult CreateTransfer(FootballPlayerTransferViewModel model)
        {
            transfers.Create(model.PlayerId, model.Season, model.Date, model.PreviousTeam, model.PreviousTeamLogo, model.PreviousTeamCountryFlag,
                model.CurrentTeam, model.CurrentTeamLogo, model.CurrentTeamCountryFlag, model.TransferPrice, model.OnLoan);

            return RedirectToAction("footballPlayer", "admin", new { id = model.PlayerId });
        }

        [Route("admin/updatePlayerTransfer/{id}")]
        public IActionResult UpdateTransfer(int id)
        {
            var transfer = transfers.GetTransfer(id);
            var model = new FootballPlayerTransferViewModel
            {
                Id = transfer.Id,
                Season = transfer.Season,
                CurrentTeam = transfer.CurrentTeam,
                CurrentTeamLogo = transfer.CurrentTeamLogo,
                CurrentTeamCountryFlag = transfer.CurrentTeamCountryFlag,
                PreviousTeam = transfer.PreviousTeam,
                PreviousTeamLogo = transfer.PreviousTeamLogo,
                PreviousTeamCountryFlag = transfer.PreviousTeamCountryFlag,
                Date = transfer.Date,
                PlayerId = transfer.PlayerId,
                OnLoan = transfer.OnLoan,
                TransferPrice = transfer.TransferPrice
            };

            return View(model);
        }

        [HttpPost]
        [Route("admin/updatePlayerTransfer/{id}")]
        public IActionResult UpdateTransfer(FootballPlayerTransferViewModel model)
        {
            transfers.Update(model.Id, model.PlayerId, model.Season, model.Date, model.PreviousTeam, model.PreviousTeamLogo, model.PreviousTeamCountryFlag,
                model.CurrentTeam, model.CurrentTeamLogo, model.CurrentTeamCountryFlag, model.TransferPrice, model.OnLoan);

            return RedirectToAction("footballPlayer", "admin", new { id = model.PlayerId });
        }

        [Route("admin/deletePlayerTransfer/{id}")]
        public IActionResult DeleteTransfer(int id)
        {
            var transfer = transfers.GetTransfer(id);
            var model = new FootballPlayerTransferViewModel
            {
                PlayerId = transfer.PlayerId,
                Season = transfer.Season,
                Date = transfer.Date,
                CurrentTeam = transfer.CurrentTeam,
                CurrentTeamLogo = transfer.CurrentTeamLogo,
                CurrentTeamCountryFlag = transfer.CurrentTeamCountryFlag,
                PreviousTeam = transfer.PreviousTeam,
                PreviousTeamLogo = transfer.PreviousTeamLogo,
                PreviousTeamCountryFlag = transfer.PreviousTeamCountryFlag,
                TransferPrice = transfer.TransferPrice,
                OnLoan = transfer.OnLoan
            };

            TempData["id"] = transfer.Id;
            TempData["playerId"] = transfer.PlayerId;

            return View(model);
        }


        [Route("admin/deletePlayerTransfer")]
        public IActionResult DeleteTransfer()
        {
            int id = (int)TempData["id"];
            int playerId = (int)TempData["playerId"];
            transfers.Delete(id);

            return RedirectToAction("footballPlayer", "admin", new { id = playerId });
        }
    }
}