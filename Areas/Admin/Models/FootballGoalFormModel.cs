namespace Sportiada.Web.Areas.Admin.Models
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Services.Admin.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    public class FootballGoalFormModel
    {
        public int Id { get; set; }

        public int TypeId { get; set; }

        public int PlayerId { get; set; }

        public int Minute { get; set; }

        public bool FirstHalf { get; set; }

        public int GameStatisticId { get; set; }

        public int AssistanceId { get; set; }

        public IEnumerable<FootballSquadPlayerAdminModel> Players { get; set; }

        public IEnumerable<SelectListItem> GoalTypes { get; set; }
    }
}
