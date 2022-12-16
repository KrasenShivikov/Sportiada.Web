using Sportiada.Services.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sportiada.Web.Areas.Admin.Models
{
    public class FootballSquadPlayerFormModel
    {
        public int PlayerId { get; set; }

        public FootballPlayerViewModel Player { get; set; }

        public int SquadId { get; set; }

        public int Number { get; set; }

        public string Picture { get; set; }

        public string Position { get; set; }

        public string SquadType { get; set; }

        public string ContractStartDate { get; set; }

        public string ContractEndDate { get; set; }

        public bool OnLoan { get; set; }

        public bool JoinedInSummer { get; set; }

        public bool JoinedInWinter { get; set; }

        public bool LeftInSummer { get; set; }

        public bool LeftInWinter { get; set; }

        public IEnumerable<FootballPlayerAdminModel> Players { get; set; }

    }
}
