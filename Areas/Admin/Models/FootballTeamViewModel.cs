﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sportiada.Web.Areas.Admin.Models
{
    public class FootballTeamViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int CityId { get; set; }

        public string Logo { get; set; }
        public IEnumerable<SelectListItem> Cities { get; set; }
    }
}
