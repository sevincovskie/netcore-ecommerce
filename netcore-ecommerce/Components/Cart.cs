using Microsoft.AspNetCore.Mvc;
using netcore_ecommerce.Data;
using netcore_ecommerce.DTO;
using netcore_ecommerce.Models;
using netcore_ecommerce.Session;

namespace netcore_ecommerce.Components;

public class Cart: ViewComponent {
    private readonly ApplicationDbContext _context;

    public Cart(ApplicationDbContext context) {
        _context = context;
    }

    public IViewComponentResult Invoke() {
        List<CartItem> items = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();
        CartViewModel cartVM = new() {Items = items, GrandTotal = items.Sum(x => x.Quantity * x.Price)};
        return View(cartVM);
    }
}