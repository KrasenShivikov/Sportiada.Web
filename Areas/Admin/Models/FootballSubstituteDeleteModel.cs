namespace Sportiada.Web.Areas.Admin.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    public class FootballSubstituteDeleteModel
    {
        public int GameId { get; set; }

        public int Minute { get; set; }

        public bool FirstHalf { get; set; }
    }
}
