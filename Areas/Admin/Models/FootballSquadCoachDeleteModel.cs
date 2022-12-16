namespace Sportiada.Web.Areas.Admin.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class FootballSquadCoachDeleteModel
    {
        public int SquadId { get; set; }

        public int CoachId { get; set; }

        public FootballCoachListModel Coach { get; set; }

        public string Position { get; set; }
    }
}
