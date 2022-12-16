namespace Sportiada.Web.Areas.Admin.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class FootballStadiumFormModel
    {
        public int Id { get; set; }

        public int TeamId { get; set; }

        public string Name { get; set; }
    }
}
