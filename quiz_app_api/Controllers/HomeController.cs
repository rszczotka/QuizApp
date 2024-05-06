using Microsoft.AspNetCore.Mvc;

namespace quiz_app_api.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return Json(new { ApiWorking = true });
		}
	}
}
