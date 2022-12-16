namespace Sportiada.Web.Areas.Admin.Models
{
    using Services.Admin.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    public class FootballGameStatisticCoachFormModel
    {
        public int Id { get; set; }

        public int CoachId { get; set; }

        public int GameStattisticId { get; set; }

        public IEnumerable<FootballCoachListModel> Coaches { get; set; }
    }
}
