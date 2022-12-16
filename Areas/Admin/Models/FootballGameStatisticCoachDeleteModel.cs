namespace Sportiada.Web.Areas.Admin.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    public class FootballGameStatisticCoachDeleteModel
    {
        public int Id { get; set; }
        public int GameId { get; set; }

        public string CoachName { get; set; }

        public string Country { get; set; }
    }
}
