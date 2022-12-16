using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sportiada.Web.Areas.Admin.Models
{
    public class FootballGameFormModel
    {
        public int Id { get; set; }

        public int Attendance { get; set; }

        public DateTime Date { get; set; }

        public int RoundId { get; set; }
    }
}
