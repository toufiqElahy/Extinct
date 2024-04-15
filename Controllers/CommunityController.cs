using interwebz.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace interwebz.Controllers
{
    public class CommunityController : Controller
    {
        private readonly ILogger<CommunityController> _logger;

        public CommunityController(ILogger<CommunityController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        
		public IActionResult Forums()
		{
			return View();
		}
		public IActionResult Announcement()
		{
			return View();
		}
        public IActionResult Single()
        {
            return View();
        }
        public IActionResult Knowledge_base()
        {
            return View();
        }
        public IActionResult Tickets()
        {
            return View();
        }
        public IActionResult Account()
        {
            return View();
        }
        public IActionResult Security()
        {
            return View();
        }
        public IActionResult Profile()
        {
            return View();
        }

		public IActionResult Error()
		{
			return View();
		}
        public IActionResult Register()
        {
            return View();
        }


        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //      public IActionResult Error()
        //      {
        //          return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //      }
    }
}
