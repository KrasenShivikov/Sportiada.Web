namespace Sportiada.Web.Areas.Admin.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    public class FootballGoalDeleteModel
    {
        public int Id { get; set; }
        public int GameId { get; set; }              
        public string PlayerName { get; set; }
        public string Country { get; set; }

        public string TypeName { get; set; }

        public string TypePicture { get; set; }

        public int Minute { get; set; }

        public bool FirstHalf { get; set; }
    }
}
