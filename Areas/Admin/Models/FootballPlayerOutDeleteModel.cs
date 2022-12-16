namespace Sportiada.Web.Areas.Admin.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    public class FootballPlayerOutDeleteModel
    {
        public string PlayerName { get; set; }

        public string PlayerCountry { get; set; }

        public string OutIcon { get; set; }

        public int GameId { get; set; }
    }
}
