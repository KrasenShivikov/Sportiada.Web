namespace Sportiada.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Models;
    using Services.Admin.Interfaces;
    using Sportiada.Services.Admin.Models;
    using System.Collections.Generic;
    using System.Linq;

    [Area("Admin")]
    public class AdminCityController : Controller
    {
        private readonly ICityAdminService cities;
        private readonly ICountryAdminService countries;

        
        public AdminCityController(ICityAdminService cities, ICountryAdminService countries)
        {
            this.cities = cities;
            this.countries = countries;
        }

        [Route("admin/createCity")]
        public IActionResult CreateCity()
        {     
            var countries = this.countries.All();

            return View(new CityViewModel
            {
                Countries = countries
                            .OrderBy(c => c.Name)
                            .Select(c => new SelectListItem 
                            { 
                                Text = c.Name,
                                Value = c.Id.ToString()
                            })
            });
        }

        [HttpPost]
        [Route("admin/createCity")]
        public IActionResult CreateCity(CityViewModel city)
        {
            this.cities.Create(city.Name, city.CountryId);
            return RedirectToAction(nameof(Cities));
        }

        [Route("admin/cities")]
        public IActionResult Cities()
            => View(this.cities.All());

        [Route("admin/updateCity/{id}")]
        public IActionResult UpdateCity(int id)
        {
            var city = cities.ById(id);
            var countries = this.countries.All().ToList();
            var country = countries.Where(c => c.Id == city.Country.Id).FirstOrDefault();
            countries.Remove(country);
            var finalList = countries
                    .OrderBy(c => c.Name)
                    .Prepend(country);



            return View(new CityViewModel()
            { 
                Id = city.Id,
                Name = city.Name,
                CountryId = city.Country.Id,
                Countries = finalList
                            .Select(c => new SelectListItem
                            {
                                Text = c.Name,
                                Value = c.Id.ToString()
                            })
            });
        }

        [HttpPost]
        [Route("admin/updateCity/{id}")]
        public IActionResult UpdateCity(CityViewModel city)
        {
            this.cities.Update(city.Id, city.Name, city.CountryId);
            return RedirectToAction(nameof(Cities));
        }

           
        [Route("admin/deleteCity/{id}")]
        public IActionResult DeleteCity(int id)
        {
            CityAdminModel city = this.cities.ById(id);
            TempData["Id"] = city.Id;
            return View(city);
        }

        [Route("admin/deleteCity")]
        public IActionResult FinalizeDeleteCity()
        {
            var id = (int)TempData["Id"];
            this.cities.Delete(id);
            return RedirectToAction(nameof(Cities));
        }
    }
}
