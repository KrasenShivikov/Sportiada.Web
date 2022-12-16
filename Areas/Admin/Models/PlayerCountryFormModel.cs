

namespace Sportiada.Web.Areas.Admin.Models
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    public class PlayerCountryFormModel
    {
        public int PlayerId { get; set; }

        public int CountryId { get; set; }

        public bool MainCountry { get; set; }
        public IEnumerable<CountryViewModel> Countries { get; set; }


    }
}
