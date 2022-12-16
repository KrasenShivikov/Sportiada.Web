namespace Sportiada.Web.Areas.Admin.Models
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    public class FootballGameStatisticFormModel
    {
        public int Id { get; set; }

        public int GameId { get; set; }

        public int TypeId { get; set; }

        public int SquadId { get; set; }

        public int BallPossession { get; set; }

        public int Corners { get; set; }

        public int ShotsOnTarget { get; set; }

        public int ShotsWide { get; set; }

        public int Fouls { get; set; }

        public int Offsides { get; set; }

        public IEnumerable<FootballSquadViewModel> Squads { get; set; }

        public IEnumerable<SelectListItem> GameStatisticTypes { get; set; }
    }
}
