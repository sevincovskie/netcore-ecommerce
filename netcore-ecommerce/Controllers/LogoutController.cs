using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using netcore_ecommerce.Models;

namespace netcore_ecommerce.Controllers {
    [Authorize]
    public class LogoutController: Controller {
        private readonly SignInManager<AppUser> _signInManager;

        public LogoutController(SignInManager<AppUser> signInManager) {
            _signInManager = signInManager;
        }

        // GET: LogoutController
        public async Task<ActionResult> Index() {
            await _signInManager.SignOutAsync();
            ViewBag.Message = "Logout Successful";
            return RedirectToAction("Index", "Home");
        }
    }
}