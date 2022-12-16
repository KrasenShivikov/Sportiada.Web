using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sportiada.Web.Areas.Admin.Models
{
    public class FootballPlayerTransferViewModel
    {
        public int Id { get; set; }

        public int PlayerId { get; set; }

        public string Season { get; set; }

        public string Date { get; set; }

        public string PreviousTeam { get; set; }

        public string PreviousTeamLogo { get; set; }

        public string PreviousTeamCountryFlag { get; set; }

        public string CurrentTeam { get; set; }

        public string CurrentTeamLogo { get; set; }

        public string CurrentTeamCountryFlag { get; set; }

        public string TransferPrice { get; set; }

        public string OnLoan { get; set; }
    }
}
