namespace Sportiada.Web.Areas.Admin.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    public class FootballSubstituteFormModel
    {
        public int Id { get; set; }

        public int GameStatisticId { get; set; }

        public int PlayerInId { get; set; }

        public int PlayerOutId { get; set; }

        public int Minute { get; set; }

        public bool FirstHalf { get; set; }
    }
}
