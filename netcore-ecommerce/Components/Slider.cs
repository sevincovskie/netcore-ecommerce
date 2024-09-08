using Microsoft.AspNetCore.Mvc;
using netcore_ecommerce.Data;

namespace netcore_ecommerce.Components;

public class Slider: ViewComponent {
    private readonly ApplicationDbContext _context;

    public Slider(ApplicationDbContext context) {
        _context = context;
    }

    public IViewComponentResult Invoke() {
        var result = _context.Sliders.ToList();
        return View(result);
    }
}