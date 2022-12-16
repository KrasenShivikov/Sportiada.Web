using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sportiada.Web.Areas.Admin.Models
{
    public class FootballLineUpDeleteModel
    {
        public int Id { get; set; }
        public int GameId { get; set; }

        public string PlayerName { get; set; }

        public string Country { get; set; }
    }
}
