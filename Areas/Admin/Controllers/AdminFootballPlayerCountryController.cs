

namespace Sportiada.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Services.Admin.Interfaces;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc.Rendering;

    [Area("Admin")]
    public class AdminFootballPlayerCountryController : Controller
    {
        private readonly IFootballPlayerCountryAdminService playerCountries;
        private readonly ICountryAdminService countries;

        public AdminFootballPlayerCountryController(IFootballPlayerCountryAdminService playerCountries, ICountryAdminService countries)
        {
            this.playerCountries = playerCountries;
            this.countries = countries;
        }

        [Route("admin/createPlayerCountry/{playerId}")]
        public IActionResult CreatePlayerCountry(int playerId)
        { 
            PlayerCountryFormModel model = new PlayerCountryFormModel
            {
                PlayerId = playerId,
                Countries = countries.All().OrderBy(c => c.Name).Select(c => new CountryViewModel 
                {
                    Id = c.Id,
                    Name = c.Name,
                    ShortName = c.ShortName,
                    PicturePath = c.PicturePath
                })
            };

            return View(model);
        }

        [HttpPost]
        [Route("admin/createPlayerCountry/{playerId}")]
        public IActionResult CreatePlayerCountry(PlayerCountryViewModel model)
        {
            playerCountries.Create(model.PlayerId, model.CountryId, model.MainCountry);
            return RedirectToAction("footballPlayer", "admin", new { id = model.PlayerId });
        }

        [Route("admin/updatePlayerCountry/{playerId}/{countryId}")]
        public IActionResult UpdatePlayerCountry(int playerId, int countryId)
        {
            var country = playerCountries.GetPlayerCountry(playerId, countryId);
            var model = new PlayerCountryFormModel
            {
                PlayerId = country.PlayerId,
                CountryId = country.CountryId,
                MainCountry = country.MainCountry,
                Countries = countries.All().OrderBy(c => c.Name).Select(c => new CountryViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    ShortName = c.ShortName,
                    PicturePath = c.PicturePath
                })

            };

            TempData["playerId"] = country.PlayerId;
            TempData["countryId"] = country.CountryId;

            return View(model);
        }

        [HttpPost]
        [Route("admin/updatePlayerCountry/{playerId}/{countryId}")]
        public IActionResult UpdatePlayerCountry(PlayerCountryFormModel model)
        {
            int playerId = (int)TempData["playerId"];
            int countryId = (int)TempData["countryId"];

            playerCountries.Delete(playerId, countryId);
            playerCountries.Create(model.PlayerId, model.CountryId, model.MainCountry);
            return RedirectToAction("footballPlayer", "admin", new { id = model.PlayerId });
        }

        [Route("admin/deletePlayerCountry/{playerId}/{countryId}")]
        public IActionResult DeletePlayerCountry(int playerId, int countryId)
        {
            var playerCountry = playerCountries.GetPlayerCountry(playerId, countryId);
            var country = countries.ById(countryId);

            var model = new PlayerCountryViewModel
            {
                PlayerId = playerCountry.PlayerId,
                Country = new CountryViewModel
                {
                    Name = country.Name,
                    PicturePath = country.PicturePath
                }
            };

            TempData["countryId"] = countryId;
            TempData["playerId"] = playerId;

            return View(model);
        }

        [Route("admin/deletePlayerCountry")]
        public IActionResult DeletePlayerCountry()
        {
            int playerId = (int)TempData["playerId"];
            int countryId = (int)TempData["countryId"];

            playerCountries.Delete(playerId, countryId);
            return RedirectToAction("footballPlayer", "admin", new { id = playerId });
        }
        
    }
}
