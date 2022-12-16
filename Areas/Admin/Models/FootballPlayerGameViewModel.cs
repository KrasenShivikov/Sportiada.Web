using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sportiada.Web.Areas.Admin.Models
{
    public class FootballPlayerGameViewModel
    {
        public int PlayerId { get; set; }

        public string ProfileName { get; set; }

        public int Number { get; set; }

        public string Picture { get; set; }

        public string Country { get; set; }
    }
}
