namespace Sportiada.Web.Areas.AlpineSki.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Sportiada.Services.AlpineSki.Interfaces;

    [Area("AlpineSki")]
    public class ResultAlpineskiController : Controller
    {
        private readonly IResultAlpineSkiService results;

        public ResultAlpineskiController(IResultAlpineSkiService results)
        {
            this.results = results;
        }

        [Route("alpineski/result/all/{skierid}/{place}/{tournamentid}/{disciplineName}")]
        public IActionResult AllBySkierByPlaceByTournamentByDiscipline(int skierId, int place, int tournamentId, string disciplineName)
            => View(this.results.AllBySkierByPlaceByTournamentByDiscipline(skierId, place, tournamentId, disciplineName));

        [Route("alpineski/result/all/{skierid}/{place}/{disciplineName}")]
        public IActionResult AllBySkierByPlaceByDiscipline(int skierId, int place, string disciplineName)
            => View(this.results.AllBySkierByPlaceByDiscipline(skierId, place, disciplineName));
    }
}
