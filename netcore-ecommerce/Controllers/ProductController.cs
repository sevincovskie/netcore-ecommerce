using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using netcore_ecommerce.Data;
using netcore_ecommerce.Models;

namespace netcore_ecommerce.Controllers {
    [Authorize]
    public class ProductController: Controller {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context) {
            _context = context;
        }

        // GET: Product
        public async Task<IActionResult> Index() {
            var applicationDbContext = _context.Products.Include(p => p.Category);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Product/Create
        public IActionResult Create() {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }

        // POST: Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("ProductId,Name,Code,Stock,Description,Picture,Price,CategoryId")] Product product,
            IFormFile? ImageUpload) {
            if(ModelState.IsValid) {
                if(ImageUpload != null) {
                    var ext = Path.GetExtension(ImageUpload.FileName);
                    string newName = Guid.NewGuid().ToString() + ext;
                    var path = Path.Combine(Directory.GetCurrentDirectory() + "/wwwroot/products/" + newName);
                    using(var stream = new FileStream(path, FileMode.Create)) {
                        await ImageUpload.CopyToAsync(stream);
                    }

                    product.Picture = newName;
                }

                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
            return View(product);
        }

        // GET: Product/Edit/5
        public async Task<IActionResult> Edit(int? id) {
            if(id == null) {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if(product == null) {
                return NotFound();
            }

            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
            return View(product);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind("ProductId,Name,Code,Stock,Description,Picture,Price,CategoryId")]
            Product product, IFormFile? ImageUpload) {
            var existing = await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.ProductId == id);
            if(id != product.ProductId) {
                return NotFound();
            }

            if(ModelState.IsValid) {
                try {
                    if(ImageUpload != null) {
                        var ext = Path.GetExtension(ImageUpload.FileName);
                        string newName = Guid.NewGuid().ToString() + ext;
                        var path = Path.Combine(Directory.GetCurrentDirectory() + "/wwwroot/products/" + newName);
                        using(var stream = new FileStream(path, FileMode.Create)) {
                            await ImageUpload.CopyToAsync(stream);
                        }

                        product.Picture = newName;
                    } else {
                        product.Picture = existing.Picture;
                    }

                    _context.Update(product);
                    await _context.SaveChangesAsync();
                } catch(DbUpdateConcurrencyException) {
                    if(!ProductExists(product.ProductId)) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
            return View(product);
        }

        // GET: Product/Delete/5
        public async Task<IActionResult> Delete(int? id) {
            if(id == null) {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if(product == null) {
                return NotFound();
            }

            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            var product = await _context.Products.FindAsync(id);
            if(product != null) {
                string path = Path.Combine(Directory.GetCurrentDirectory() + "/wwwroot/products/" + product.Picture);
                FileInfo pathFile = new FileInfo(path);
                if(pathFile.Exists) {
                    System.IO.File.Delete(pathFile.FullName);
                    pathFile.Delete();
                }

                _context.Products.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id) {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}