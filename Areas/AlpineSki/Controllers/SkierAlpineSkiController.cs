namespace Sportiada.Web.Areas.AlpineSki.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Sportiada.Services.AlpineSki.Interfaces;

    [Area("AlpineSki")]
    public class SkierAlpineSkiController : Controller
    {
        private readonly ISkierAlpineSkiService skiers;

        public SkierAlpineSkiController(ISkierAlpineSkiService skiers)
        {
            this.skiers = skiers;
        }

        [Route("alpineski/skier/{id}")]
        public IActionResult SkierById(int id)
             => View(this.skiers.ById(id));

        [Route("alpineski/skier/{skierId}/{tournamentId}")]
        public IActionResult SkierByIdByTournament(int skierId, int tournamentId)
             => View(this.skiers.ByIdByTournament(skierId, tournamentId));

    }
}
