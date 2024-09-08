using Microsoft.AspNetCore.Mvc;
using netcore_ecommerce.Data;

namespace netcore_ecommerce.Components;

public class Trends: ViewComponent {
    private readonly ApplicationDbContext _context;

    public Trends(ApplicationDbContext context) {
        _context = context;
    }

    public IViewComponentResult Invoke() {
        var result = _context.Products.ToList();
        return View(result);
    }
}