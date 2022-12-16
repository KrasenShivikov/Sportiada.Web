namespace Sportiada.Web.Areas.Admin.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    public class FootballSquadDeleteModel
    {
        public int TeamId { get; set; }

        public string TeamName { get; set; }

        public string SeasonName { get; set; }
    }
}
