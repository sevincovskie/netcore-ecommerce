using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using netcore_ecommerce.Data;
using netcore_ecommerce.Models;

namespace netcore_ecommerce.Controllers {
    [Authorize]
    public class SliderController: Controller {
        private readonly ApplicationDbContext _context;

        public SliderController(ApplicationDbContext context) {
            _context = context;
        }

        // GET: Slider
        public async Task<IActionResult> Index() {
            return View(await _context.Sliders.ToListAsync());
        }

        // GET: Slider/Create
        public IActionResult Create() {
            return View();
        }

        // POST: Slider/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Header1,Header2,Context,Image")] Slider slider,
            IFormFile ImageUpload) {
            if(ModelState.IsValid) {
                if(ImageUpload != null) {
                    var ext = Path.GetExtension(ImageUpload.FileName);
                    string newName = Guid.NewGuid().ToString() + ext;
                    var path = Path.Combine(Directory.GetCurrentDirectory() + "/wwwroot/slider/" + newName);
                    using(var stream = new FileStream(path, FileMode.Create)) {
                        await ImageUpload.CopyToAsync(stream);
                    }

                    slider.Image = newName;
                }

                _context.Add(slider);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(slider);
        }

        // GET: Slider/Edit/5
        public async Task<IActionResult> Edit(int? id) {
            if(id == null) {
                return NotFound();
            }

            var slider = await _context.Sliders.FindAsync(id);
            if(slider == null) {
                return NotFound();
            }

            return View(slider);
        }

        // POST: Slider/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Header1,Header2,Context,Image")] Slider slider,
            IFormFile? ImageUpload) {
            var existing = await _context.Sliders.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
            if(id != slider.Id) {
                return NotFound();
            }

            if(ModelState.IsValid) {
                try {
                    if(ImageUpload != null) {
                        var ext = Path.GetExtension(ImageUpload.FileName);
                        string newName = Guid.NewGuid().ToString() + ext;
                        var path = Path.Combine(Directory.GetCurrentDirectory() + "/wwwroot/slider/" + newName);
                        using(var stream = new FileStream(path, FileMode.Create)) {
                            await ImageUpload.CopyToAsync(stream);
                        }

                        slider.Image = newName;
                    } else {
                        slider.Image = existing.Image;
                    }

                    _context.Update(slider);
                    await _context.SaveChangesAsync();
                } catch(DbUpdateConcurrencyException) {
                    if(!SliderExists(slider.Id)) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return View(slider);
        }

        // GET: Slider/Delete/5
        public async Task<IActionResult> Delete(int? id) {
            if(id == null) {
                return NotFound();
            }

            var slider = await _context.Sliders
                .FirstOrDefaultAsync(m => m.Id == id);
            if(slider == null) {
                return NotFound();
            }

            return View(slider);
        }

        // POST: Slider/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            var slider = await _context.Sliders.FindAsync(id);
            if(slider != null) {
                string path = Path.Combine(Directory.GetCurrentDirectory() + "/wwwroot/slider/" + slider.Image);
                FileInfo pathFile = new FileInfo(path);
                if(pathFile.Exists) {
                    System.IO.File.Delete(pathFile.FullName);
                    pathFile.Delete();
                }

                _context.Sliders.Remove(slider);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SliderExists(int id) {
            return _context.Sliders.Any(e => e.Id == id);
        }
    }
}