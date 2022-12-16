namespace Sportiada.Web.Areas.Admin.Models
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Services.Admin.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    public class FootballSidelineFormModel
    {
        public int Id { get; set; }

        public int ReasonId { get; set; }

        public int PlayerId { get; set; }

        public int GameStatisticId { get; set; }

        public IEnumerable<FootballSquadPlayerAdminModel> Players { get; set; }

        public IEnumerable<SelectListItem> Reasons { get; set; }
    }
}
