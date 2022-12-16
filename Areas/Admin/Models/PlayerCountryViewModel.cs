using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sportiada.Web.Areas.Admin.Models
{
    public class PlayerCountryViewModel
    {
        public int PlayerId { get; set; }

        public int CountryId { get; set; }

        public CountryViewModel Country { get; set; }

        public bool MainCountry { get; set; }

    }
}
