using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using netcore_ecommerce.Data;

namespace netcore_ecommerce.Controllers {
    [Authorize]
    public class OrderController: Controller {
        private readonly ApplicationDbContext _context;

        public OrderController(ApplicationDbContext context) {
            _context = context;
        }

        // GET: OrderController
        public ActionResult Index() {
            ViewBag.Orders = _context.Orders;
            ViewBag.Products = _context.Products;
            return View();
        }

        public IActionResult Delete(string id) {
            var order = _context.Orders.Find(id);
            _context.Orders.Remove(order);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}