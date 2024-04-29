using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using interwebz.Data;
using interwebz.Models;
using Microsoft.AspNetCore.Authorization;

namespace interwebz.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IWebHostEnvironment Environment;

        public ProductsController(ApplicationDbContext context, IWebHostEnvironment _environment)
        {
            _context = context;
            Environment = _environment; 
        }
        private void CheckAdmin()
        {
            if (User.Identity.Name != "admin")
            {
                throw new Exception();
            }
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            CheckAdmin();
            return View(await _context.Product.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            CheckAdmin();
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
        private async Task upload([FromForm] IFormFileCollection files, string targetFolder)
        {
            foreach (IFormFile file in files)
            {
                if (file.Length <= 0) continue;

                //fileName is the the fileName including the relative path
                //var filePath = file.FileName.Substring(file.FileName.IndexOf('/') + 1);
                string path = Path.Combine(targetFolder, file.FileName);

                //check if folder exists, create if not
                var fi = new FileInfo(path);
                fi.Directory?.Create();

                //copy to target
                using var fileStream = new FileStream(path, FileMode.Create);
                await file.CopyToAsync(fileStream);
            }
        }
        // GET: Products/Create
        public IActionResult Create()
        {
            CheckAdmin();
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] IFormFileCollection Files, [Bind("ProductId,Name,Title,Description,Status,Image,CreationDate,RedeemtionCode")] Product product)
        {
            string folder=  Guid.NewGuid().ToString() + "/";
            string targetFolder= this.Environment.WebRootPath + "/Images/"+ folder;
            if (Files.Count > 0)
            {
                await upload(Files, targetFolder);
                product.Image = folder + Files[0].FileName;
            }

            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            CheckAdmin();
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromForm] IFormFileCollection Files, int id, [Bind("ProductId,Name,Title,Description,Status,Image,CreationDate,RedeemtionCode")] Product product)
        {
            string folder = product.Image.Split('/')[0]+"/";
            string targetFolder = this.Environment.WebRootPath + "/Images/" + folder;
            if (Files.Count > 0)
            {
                await upload(Files, targetFolder);
                product.Image = folder + Files[0].FileName;
            }

            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            CheckAdmin();
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Product.FindAsync(id);
            if (product != null)
            {
                _context.Product.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.ProductId == id);
        }
    }
}
