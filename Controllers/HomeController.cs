using interwebz.Data;
using interwebz.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace interwebz.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public async Task<IActionResult> Products()
        {
            return View(await _context.Product.ToListAsync());
        }
        public async Task<IActionResult> Product(int? productId)
        {
            if (productId == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.ProductId == productId);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
        public IActionResult Call_of_duty()
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
