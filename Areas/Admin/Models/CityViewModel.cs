namespace Sportiada.Web.Areas.Admin.Models
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Services.Admin.Models;

    public class CityViewModel
    { 
        public int Id { get; set; }
        public string Name { get; set; }

        public int CountryId { get; set; }
        public IEnumerable<SelectListItem> Countries { get; set; }
    }
}
