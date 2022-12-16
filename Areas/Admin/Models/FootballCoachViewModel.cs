namespace Sportiada.Web.Areas.Admin.Models
{
    using Services.Admin.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    public class FootballCoachViewModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ShortName { get; set; }

        public int CountryId { get; set; }

        public CountryViewModel Country { get; set; }

        public DateTime BirthDate { get; set; }

        public string Description { get; set; }

        public string Picture { get; set; }

        public IEnumerable<FootballCoachTeamViewModel> Teams { get; set; }
    }
}
