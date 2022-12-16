using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sportiada.Web.Areas.Admin.Models
{
    public class FootballPlayerPictureViewModel
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }

        public string PlayerName { get; set; }

        public string PicturePath { get; set; }

        public DateTime Date { get; set; }
    }
}
