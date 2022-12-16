using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sportiada.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class Admin : Controller
    {
        [Route("admin")]
        public IActionResult Index()
           => View();
    }
}
