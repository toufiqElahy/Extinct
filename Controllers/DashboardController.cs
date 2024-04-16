using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace interwebz.Controllers
{
	[Authorize]
	public class DashboardController : Controller
    {
        public IActionResult Purchase_cheats()
        {
            return View();
        }
        public IActionResult Whats_New()
        {
            return View();
        }
        public IActionResult Cheat_Status()
        {
            return View();
        }
        public IActionResult Your_Orders()
        {
            return View();
        }
        public IActionResult Purchase_Codes()
        {
            return View();
        }
        public IActionResult Redeem_Code()
        {
            return View();
        }
    }
}
