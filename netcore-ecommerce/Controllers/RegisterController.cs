using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using netcore_ecommerce.DTO;
using netcore_ecommerce.Models;

namespace netcore_ecommerce.Controllers {
    public class RegisterController: Controller {
        private readonly UserManager<AppUser> _userManager;

        // GET: RegisterController
        public RegisterController(UserManager<AppUser> userManager) {
            _userManager = userManager;
        }

        public ActionResult Index() {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(AppUserRegister appUserRegister)
        {
            Random random = new Random();
            int code = random.Next(100000, 1000000);
            AppUser appUser = new AppUser()
            {
                FirstName = appUserRegister.FirstName,
                LastName = appUserRegister.LastName,
                City = appUserRegister.City,
                UserName = appUserRegister.UserName,
                Email = appUserRegister.Email,
                ConfirmCode = code.ToString()
            };

            var result = await _userManager.CreateAsync(appUser, appUserRegister.Password);
            if (result.Succeeded)
            {
                // Skip email confirmation by setting EmailConfirmed to true
                appUser.EmailConfirmed = true;
                await _userManager.UpdateAsync(appUser);
                return RedirectToAction("Index", "Login");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }

            return View();
        }

    }
}