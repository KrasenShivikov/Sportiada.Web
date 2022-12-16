using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sportiada.Web.Areas.Admin.Models
{
    public class FootballSquadCoachFormModel
    {
        public int SquadId { get; set; }

        public int CoachId { get; set; }

        public string Position { get; set; }

        public string SquadType { get; set; }

        public string FromDate { get; set; }

        public string ToDate { get; set; }

        public bool LeftInSeason { get; set; }

        public IEnumerable<FootballCoachListModel> Coaches { get; set; }
    }
}
