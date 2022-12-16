namespace Sportiada.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Services.Admin.Interfaces;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Sportiada.Services.Admin.Models;

    [Area("Admin")]
    public class AdminFootballCardTypeController : Controller
    {
        private readonly IFootballCardTypeAdminService types;
        public AdminFootballCardTypeController(IFootballCardTypeAdminService types)
        {
            this.types = types;
        }

        
        [Route("admin/cardTypes")]
        public IActionResult CardTypes()
         => View(types.CardTypes());

        [Route("admin/createCardType")]
        public IActionResult CreateCardType()
          => View();

        [HttpPost]
        [Route("admin/createCardType")]
        public IActionResult CreateCardType(FootballCardTypeViewModel model)
        {
            types.Create(model.Name, model.Picture);
            return RedirectToAction(nameof(CardTypes));
        }

        [Route("admin/updateCardType/{id}")]
        public IActionResult UpdateCardType(int id)
        {
            var type = types.GetCardType(id);
            var model = new FootballCardTypeViewModel
            {
                Id = type.Id,
                Name = type.Name,
                Picture = type.Picture
            };

            return View(model);
        }

        [HttpPost]
        [Route("admin/updateCardType/{id}")]
        public IActionResult UpdateCardType(FootballCardTypeViewModel model)
        {
            types.Update(model.Id, model.Name, model.Picture);
            return RedirectToAction(nameof(CardTypes));
        }

        [Route("admin/deleteCardType/{id}")]
        public IActionResult DeleteCardType(int id)
        {
            var type = types.GetCardType(id);
            var model = new FootballCardTypeAdminModel
            {
                Name = type.Name,
                Picture = type.Picture
            };

            TempData["id"] = type.Id;

            return View(model);
        }

        [Route("admin/deleteCardType")]
        public IActionResult DeleteCardType()
        {
            int id = (int)TempData["id"];
            types.Delete(id);

            return RedirectToAction(nameof(CardTypes));
        }
    }
}
