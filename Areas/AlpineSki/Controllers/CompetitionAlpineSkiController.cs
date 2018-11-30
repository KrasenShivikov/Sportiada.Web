namespace Sportiada.Web.Areas.AlpineSki.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Sportiada.Services.AlpineSki.Interfaces;

    [Area("AlpineSki")]
    public class CompetitionAlpineSkiController : Controller
    {
        private readonly ICompetitionAlpineSkiService competitions;

        public CompetitionAlpineSkiController(ICompetitionAlpineSkiService competitions)
        {
            this.competitions = competitions;
        }


        [Route("alpineski/competition/all")]
        public IActionResult All()
         => View(this.competitions.All());

        [Route("alpineski/competition/{id}/{stage}")]
        public IActionResult ById(int id, string stage)
         => View(this.competitions.ById(id, stage));
    }
}
