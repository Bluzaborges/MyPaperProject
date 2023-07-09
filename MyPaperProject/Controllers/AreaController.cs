using Microsoft.AspNetCore.Mvc;
using MyPaperProject.Database;
using MyPaperProject.Models;

namespace MyPaperProject.Controllers
{
	public class AreaController : Controller
	{
        #region Public Methods

        [HttpPost]
		public JsonResult GetAllAreas()
		{
			DbAreaPostgre dbArea = new DbAreaPostgre();

			List<Area> areas = dbArea.GetAllAreas();

			return Json(areas);
		}

		[HttpPost]
		public JsonResult GetAllSubareas()
		{
			DbSubareaPostgre dbSubarea = new DbSubareaPostgre();

			List<Subarea> subareas = dbSubarea.GetAllSubareas();

			return Json(subareas);
		}

        #endregion
    }
}
