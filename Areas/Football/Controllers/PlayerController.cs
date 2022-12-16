namespace Sportiada.Web.Areas.Football.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Sportiada.Services.Football.Interfaces;

    [Area("Football")]
    public class PlayerController : Controller
    {
        private readonly IPlayerService players;

        public PlayerController(IPlayerService players)
        {
            this.players = players;
        }

        [Route("football/player/{id}")]
        public IActionResult ProfileById(int id)
            => View(this.players.ProfileById(id));


        //[Route("football/player/{id}/{seasonId}")]
        //public IActionResult PlayerSeasonStatistic(int id, int seasonId)
        //{
        //    return View(this.players.PlayerSeasonStatistic(id, seasonId));
        //    try
        //    {
        //        return View(this.players.PlayerSeasonStatistic(id, seasonId));
        //    }
        //    catch (System.Exception)
        //    {
        //        return RedirectToAction("ProfileById");
        //    }
        //}
    }
}
