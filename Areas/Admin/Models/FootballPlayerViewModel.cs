namespace Sportiada.Web.Areas.Admin.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    public class FootballPlayerViewModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ProfileName { get; set; }
        
        public DateTime BirthDate { get; set; }

        public string BirthPlace { get; set; }

        public string Foot { get; set; }

        public int Height { get; set; }

    }
}
