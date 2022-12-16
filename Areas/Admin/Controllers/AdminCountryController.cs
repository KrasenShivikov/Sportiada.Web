namespace Sportiada.Web.Areas.Admin.Controllers
{
    using Data;
    using Microsoft.AspNetCore.Mvc;
    using Services.Admin.Interfaces;
    using Services.Admin.Models;
    using Models;

    [Area("Admin")]
    public class AdminCountryController : Controller
    {
        private readonly ICountryAdminService countries;
        public AdminCountryController(ICountryAdminService countries)
        {
            this.countries = countries;
        }

        [Route("admin/createCountry")]
        public IActionResult CreateCountry()
            => View();

        [HttpPost]
        [Route("admin/createCountry")]
        public IActionResult CreateCountry(CountryViewModel country)
        {
            this.countries.Create(country.Name, country.ShortName, country.PicturePath);
            return RedirectToAction(nameof(Countries));
        }

        [Route("admin/countries")]
        public IActionResult Countries()
            => View(this.countries.All());

        [Route("admin/updateCountry/{id}")]
        public IActionResult UpdateCountry(int id)
        {
            var country = countries.ById(id);
            var model = (new CountryViewModel
            {
                Id = country.Id,
                ShortName = country.ShortName,
                Name = country.Name,
                PicturePath = country.PicturePath
            });

            return View(model);
        }

        [HttpPost]
        [Route("admin/updateCountry/{id}")]
        public IActionResult UpdateCountry(CountryViewModel country)
        {
            this.countries.Change(country.Id, country.Name, country.ShortName, country.PicturePath);
            return RedirectToAction(nameof(Countries));
        }

        [Route("admin/deleteCountry/{id}")]
        public IActionResult DeleteCountry(int id)
        {
            CountryAdminModel country = this.countries.ById(id);
            TempData["countryId"] = country.Id;
            return View(country);
        }
        
        [Route("admin/deleteCountry")]
        public IActionResult FinalizeDeleteCountry()
        {
            var id = (int)TempData["countryId"];
            this.countries.Delete(id);
            return RedirectToAction(nameof(Countries));
        }

        
    }
}
