namespace Sportiada.Web.Areas.Football.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Sportiada.Services.Football.Interfaces;

    [Area("Football")]
    public class GameController : Controller
    {
        private readonly IGameService games;

        public GameController(IGameService games)
        {
            this.games = games;
        }

        [Route("football/game/last20")]
        public IActionResult LastTwentyGames()
            => View(this.games.LastTwentyGames());

        [Route("football/game/{id}")]
        public IActionResult ById(int id)
         => View(this.games.ById(id));

    }
}
