using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sportiada.Web.Areas.Admin.Models
{
    public class FootballCompetitionAdminViewModel
    {
        public int Id { get; set; }
        public int SeasonId { get; set; }
        public IEnumerable<SelectListItem> Seasons { get; set; }

        public int TournamentId { get; set; }

        public int TypeId { get; set; }
        public IEnumerable<SelectListItem> Types { get; set; }
    }
}
