using Microsoft.AspNetCore.Mvc;
using netcore_ecommerce.Data;
using netcore_ecommerce.DTO;
using netcore_ecommerce.Models;
using netcore_ecommerce.Session;

namespace netcore_ecommerce.Controllers {
    public class CartController: Controller {
        private readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext context) {
            _context = context;
        }

        // GET: CartController
        public ActionResult Index() {
            List<CartItem> items = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            CartViewModel cartVM = new() {Items = items, GrandTotal = items.Sum(x => x.Quantity * x.Price)};
            return View(cartVM);
        }

        [HttpPost]
        public async Task<IActionResult> Add(int id) {
            Product product = await _context.Products.FindAsync(id);
            List<CartItem> items = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            CartItem cartItem = items.FirstOrDefault(x => x.ProductId == id);
            if(cartItem == null) {
                items.Add(new CartItem(product));
            } else {
                cartItem.Quantity++;
            }

            HttpContext.Session.SetJson("Cart", items);
            TempData["message"] = "Product added to cart";
            return Redirect(Request.Headers["Referer"].ToString());
        }

        public async Task<IActionResult> Decrease(int id) {
            List<CartItem> items = HttpContext.Session.GetJson<List<CartItem>>("Cart");
            CartItem cartItem = items.Where(x => x.ProductId == id).FirstOrDefault();
            if(cartItem.Quantity > 1) {
                cartItem.Quantity--;
            } else {
                items.RemoveAll(x => x.ProductId == id);
            }

            HttpContext.Session.SetJson("Cart", items);
            TempData["message"] = "Product removed from cart";
            return Redirect(Request.Headers["Referer"].ToString());
        }

        public IActionResult Remove(int id) {
            List<CartItem> items = HttpContext.Session.GetJson<List<CartItem>>("Cart");
            items.RemoveAll(x => x.ProductId == id);
            if(items.Count == 0) {
                HttpContext.Session.Remove("Cart");
            } else {
                HttpContext.Session.SetJson("Cart", items);
            }

            TempData["message"] = "Product removed from cart";
            return Redirect(Request.Headers["Referer"].ToString());
        }

        public async Task<IActionResult> Clear() {
            HttpContext.Session.Remove("Cart");
            TempData["message"] = "Cart cleared";
            return Redirect(Request.Headers["Referer"].ToString());
        }

        [HttpPost]
        public async Task<IActionResult> Checkout() {
            List<CartItem> items = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            if(items.Count == 0) {
                TempData["message"] = "Cart is empty";
                return RedirectToAction("Index", "Home");
            }

            var cart = HttpContext.Session.GetJson<List<CartItem>>("Cart");
            var order = new Order();
            order.Id = Guid.NewGuid().ToString();
            order.ProductId = cart.Select(x => x.ProductId).ToArray();
            order.Quantity = cart.Select(x => x.Quantity).ToArray();
            order.Name = Request.Form["name"];
            order.Email = Request.Form["email"];
            order.Phone = Request.Form["phone"];
            order.Address = Request.Form["address"];
            //decrease stock by quantity
            foreach(var item in cart) {
                var product = await _context.Products.FindAsync(Convert.ToInt32(item.ProductId));
                product.Stock -= item.Quantity;
                _context.Update(product);
            }

            order.GrandTotal = Convert.ToDouble(cart.Sum(x => x.Quantity * x.Price));
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            HttpContext.Session.Remove("Cart");
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Increase(int id) {
            List<CartItem> items = HttpContext.Session.GetJson<List<CartItem>>("Cart");
            CartItem cartItem = items.Where(x => x.ProductId == id).FirstOrDefault();
            cartItem.Quantity++;
            HttpContext.Session.SetJson("Cart", items);
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}