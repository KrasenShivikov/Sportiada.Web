﻿namespace Sportiada.Web.Areas.Admin.Models
{
    using System;
    public class SeasonViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }
    }
}
