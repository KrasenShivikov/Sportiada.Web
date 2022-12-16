using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sportiada.Web.Areas.Admin.Models
{
    public class FootballCoachTeamViewModel
    {
        public int Id { get; set; }

        public int CoachId { get; set; }

        public string Team { get; set; }

        public string TeamLogo { get; set; }

        public string TeamCountryFlag { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime UntilDate { get; set; }

        public string Position { get; set; }

        public int Matches { get; set; }
    }
}
