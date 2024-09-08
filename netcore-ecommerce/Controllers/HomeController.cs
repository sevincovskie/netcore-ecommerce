using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using netcore_ecommerce.Data;
using netcore_ecommerce.Models;

namespace netcore_ecommerce.Controllers;

public class HomeController: Controller {
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context) {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index() {
        return View();
    }

    public IActionResult Details(int id) {
        var result = _context.Products.Find(id);
        ViewBag.category = _context.Categories.Find(result.CategoryId).Name;
        return View(result);
    }

    public IActionResult Shop() {
        ViewBag.categories = _context.Categories.ToList();
        ViewBag.products = _context.Products.ToList();
        return View();
    }

    public IActionResult Filter(int maxPrice, string searchName, int? categoryId) {
        int minPrice = 0;
        ViewBag.categories = _context.Categories.ToList();
        var products = _context.Products.Where(p => p.Price >= minPrice && p.Price <= maxPrice);
        if(!string.IsNullOrEmpty(searchName)) {
            products = products.Where(p => p.Name.Contains(searchName));
        }

        if(categoryId.HasValue && categoryId > 0) {
            products = products.Where(p => p.CategoryId == categoryId.Value);
        }

        ViewBag.products = products.ToList();
        return View("Shop");
    }

    public IActionResult Category(int id) {
        ViewBag.products = _context.Products.Where(p => p.CategoryId == id).ToList();
        ViewBag.categories = _context.Categories.ToList();
        ViewBag.category = _context.Categories.Find(id).Name;
        return View("Shop");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() {
        return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }
}