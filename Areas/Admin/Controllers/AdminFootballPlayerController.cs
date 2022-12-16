namespace Sportiada.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Data;
    using Models;
    using Services.Admin.Interfaces;
    using Services.Admin.Models;

    [Area("Admin")]
    public class AdminFootballPlayerController : Controller
    {
        private readonly IFootballPlayerAdminService players;

        public AdminFootballPlayerController(IFootballPlayerAdminService players)
        {
            this.players = players;
        }

        [Route("admin/footballPlayer/{id}")]
        public IActionResult FootballPlayerById(int id)
        {
            var model = players.ById(id);
            return View(model);
        }

        [Route("admin/footballPlayers")]
        public IActionResult FootballPlayers()
            => View(this.players.All());

        [Route("admin/createFootballPlayer")]
        public IActionResult CreateFootballPlayer()
            => View();

        [HttpPost]
        [Route("admin/createFootballPlayer")]
        public IActionResult CreateFootballPlayer(FootballPlayerViewModel player)
        {
            this.players.Create(player.FirstName, player.LastName, player.BirthDate, player.ProfileName, player.BirthPlace, player.Foot, player.Height);
            return RedirectToAction(nameof(FootballPlayers));
        }

        [Route("admin/updateFootballPlayer/{id}")]
        public IActionResult UpdateFootballPlayer(int id)
        {
            var player = this.players.ById(id);
       
            return View(new FootballPlayerViewModel
            {
                Id = player.Id,
                FirstName = player.FirstName,
                LastName = player.LastName,
                ProfileName = player.ProfileName,
                BirthDate = player.BirthDate,
                BirthPlace = player.BirthPlace,
                Foot = player.Foot,
                Height = player.Height
            });
        }

        [HttpPost]
        [Route("admin/updateFootballPlayer/{id}")]
        public IActionResult UpdateFootballPlayer(FootballPlayerViewModel player)
        {
            this.players.Update(player.Id, player.FirstName, player.LastName, player.BirthDate, player.ProfileName, player.BirthPlace, player.Foot, player.Height);
            return RedirectToAction("footballPlayer", "admin", new { id = player.Id});
        }

        [Route("admin/deleteFootballPlayer/{id}")]
        public IActionResult DeleteFootballPlayer(int id)
        {
            FootballPlayerProfileAdminModel player = players.ById(id);
            TempData["playerId"] = player.Id;
            return View(player);
        }

        [Route("admin/deleteFootballPlayer")]
        public IActionResult FinalizeDelete()
        {
            int id = (int)TempData["playerId"];
            this.players.Delete(id);
            return RedirectToAction(nameof(FootballPlayers));
        }



        
    }
}
