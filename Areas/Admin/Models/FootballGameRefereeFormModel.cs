namespace Sportiada.Web.Areas.Admin.Models
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Services.Admin.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    public class FootballGameRefereeFormModel
    {
        public int Id { get; set; }

        public int GameId { get; set; }

        public int RefereeId { get; set; }

        public int TypeId { get; set; }

        public IEnumerable<SelectListItem> RefereeTypes { get; set; }
        
        public IEnumerable<FootballRefereeAdminModel> Referees { get; set; }
    }
}
