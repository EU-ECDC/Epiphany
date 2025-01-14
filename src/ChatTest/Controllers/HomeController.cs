using ChatTest.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ChatTest.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
        private readonly AIModel _aiModel;

        public HomeController(ILogger<HomeController> logger, AIModel aiModel)
		{
			_logger = logger;
			_aiModel = aiModel;
        }

		public IActionResult Index()
		{
            return View(_aiModel);
        }

        public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}