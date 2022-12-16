namespace Sportiada.Web.Areas.Admin.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    public class FootballGoalAssistanceDeleteModel
    {
        public int Id { get; set; }
        public int GameId { get; set; }

        public string PlayerName { get; set; }

        public string Country { get; set; }

        public int Minute { get; set; }

        public bool FirstHalf { get; set; }
    }
}
