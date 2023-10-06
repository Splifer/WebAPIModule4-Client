using Microsoft.AspNetCore.Mvc;

namespace WebAPIModule4_Client.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Route("Admin")]
	public class AdminController : Controller
	{
		public IActionResult Admin()
		{
			return View();
		}
	}
}
