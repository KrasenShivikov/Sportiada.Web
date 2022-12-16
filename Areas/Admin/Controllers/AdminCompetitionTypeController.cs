namespace Sportiada.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Sportiada.Data;
    using Sportiada.Data.Models;
    using Sportiada.Services.Admin.Interfaces;
    using Sportiada.Services.Admin.Models;
    using Sportiada.Web.Areas.Admin.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [Area("Admin")]
    public class AdminCompetitionTypeController : Controller
    {
        private readonly ICompetitionTypeAdminService types;
        private readonly SportiadaDbContext db;
        public AdminCompetitionTypeController(ICompetitionTypeAdminService types, SportiadaDbContext db)
        {
            this.types = types;
            this.db = db;
        }

        [Route("admin/competitionTypes")]
        public IActionResult CompetitionTypes()
        {
            return View(types.All());
        }

        [Route("admin/createCompetitionType")]
        public IActionResult CreateCompetitionType()
        {
            return View();
        }


        [HttpPost]
        [Route("admin/createCompetitionType")]
        public IActionResult CreateCompetitionType(CompetitionTypeViewModel model)
        {
            types.Create(model.Name);
            return RedirectToAction(nameof(CompetitionTypes));
        }

        [Route("admin/updateCompetitionType/{id}")]
        public IActionResult UpdateCompetitionType(int id)
        {
            var type = GetCompetitionType(id);
            return View(new CompetitionTypeViewModel
            {
                Id = type.Id,
                Name = type.Name
            });
        }

        [HttpPost]
        [Route("admin/updateCompetitionType/{id}")]
        public IActionResult UpdateCompetitionType(CompetitionTypeViewModel model)
        {
            types.Update(model.Id, model.Name);
            return RedirectToAction(nameof(CompetitionTypes));
        }

        [Route("admin/deleteCompetitionType/{id}")]
        public IActionResult DeleteCompetitionType(int id)
        {
            var type = GetCompetitionType(id);
            TempData["id"] = type.Id;
            return View(new CompetitionTypeAdminModel
            {
                Name = type.Name
            });
        }

        [Route("admin/deleteCompetitionType")]
        public IActionResult FinalizeDelete()
        {
            var id = (int)TempData["id"];
            types.Delete(id);
            return RedirectToAction(nameof(CompetitionTypes));
        }

        private CompetitionType GetCompetitionType(int id)
        {
            var type = db.CompetitionTypes
                        .Where(t => t.Id == id)
                        .FirstOrDefault();

            return type;
        }
    }
}
