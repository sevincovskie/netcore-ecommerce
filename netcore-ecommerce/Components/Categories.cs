using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using netcore_ecommerce.Data;

namespace netcore_ecommerce.Components;

public class Categories: ViewComponent {
    private readonly ApplicationDbContext _context;

    public Categories(ApplicationDbContext context) {
        _context = context;
    }

    public IViewComponentResult Invoke() {
        var result = _context.Categories.Include(c => c.Products).ToList();
        return View(result);
    }
}