using Microsoft.AspNetCore.Mvc;

namespace CinemaGO.Controllers
{
	public class ErrorController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult NotFound()
		{
			return View();
		}

		public IActionResult Commingsoon()
		{
			return View();
		}
	}
}
