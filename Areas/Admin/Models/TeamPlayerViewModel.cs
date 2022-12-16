namespace Sportiada.Web.Areas.Admin.Models
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System;
    using System.Collections.Generic;

    public class TeamPlayerViewModel
    {
        public int TeamId { get; set; }

        public int PlayerId { get; set; }

        public string PlayerName { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime UntilDate { get; set; }

        public bool OnLoan { get; set; }

        public string TeamSelections { get; set; }

        public IEnumerable<SelectListItem> Teams { get; set; }
    }
}
