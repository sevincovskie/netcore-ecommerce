using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using netcore_ecommerce.Models;

namespace netcore_ecommerce.Controllers {
    public class LoginController: Controller {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public LoginController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager) {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        // GET: LoginController
        public ActionResult Index() {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel login) {
            var result = await _signInManager.PasswordSignInAsync(login.UserName, login.Password, false, true);
            if(result.Succeeded) {
                var user = await _userManager.FindByNameAsync(login.UserName);
                if(user.EmailConfirmed) {
                    return RedirectToAction("Index", "Account");
                }
            }

            return View();
        }
    }
}