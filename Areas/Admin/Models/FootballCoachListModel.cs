namespace Sportiada.Web.Areas.Admin.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    public class FootballCoachListModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ShortName { get; set; }

        public string Picture { get; set; }

        public string CountryName { get; set; }

        public string CountryPicture { get; set; }
    }
}
