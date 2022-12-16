namespace Sportiada.Web.Areas.Admin.Models
{
    using Services.Admin.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    public class FootballPlayerInFormModel
    {
        public int Id { get; set; }

        public int PlayerId { get; set; }

        public int SubstituteId { get; set; }

        public string inIcon { get; set; }

        public IEnumerable<FootballSquadPlayerAdminModel> Players { get; set; }
    }
}
