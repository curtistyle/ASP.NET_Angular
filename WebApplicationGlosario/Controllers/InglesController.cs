using Microsoft.AspNetCore.Mvc;

namespace WebApplicationGlosario.Controllers
{
	public class InglesController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
