namespace Sportiada.Web.Areas.Admin.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    public class FootballSquadFormModel
    {
        public int Id { get; set; }

        public int SeasonId { get; set; }

        public int TeamId { get; set; }

        public IEnumerable<SeasonViewModel> Seasons { get; set; }
    }
}
