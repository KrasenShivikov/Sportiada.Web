namespace Sportiada.Web.Areas.Football.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Sportiada.Services.Football.Interfaces;

    [Area("Football")]
    public class SideController : Controller
    {
        private readonly ISideService sides;

        public SideController(ISideService sides)
        {
            this.sides = sides;
        }

        [Route("football/side/{id}")]
        public IActionResult ById(int id)
            =>View(this.sides.ById(id));

    }
}